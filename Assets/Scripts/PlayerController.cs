using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    //Weapon Ranges
    public float gunRange = 100f;
    public float meleeRange = 10f;

    Rigidbody rb;
    [SerializeField] GroundCheck groundCheck;

    [SerializeField] private int moveSpeed = 1;
    [SerializeField] int jumpForce = 300;

    [SerializeField] Transform orientationCam;

    [SerializeField] private GameObject QuadVideoPlayer;
    [SerializeField] private GameObject FadePlayer;

    private int counter = 0;

    [SerializeField] private float DeathWaitTime = 1.2f;
    private float currentDeathWaitTime = 0f;
    private bool isDead = false;

    public GameObject Pistol;
    public GameObject Crowbar;

    private void OnEnable()
    {
        InputManager.OnJumpInput += () => Jump();
        InputManager.OnShootInput += () => Attack();
        InputManager.OnScrollInput += () => Scroll();
        InputManager.OnHealInput += () => Heal();
        InputManager.OnSpeedInput += () => Speed();
    }

    // Start is called before the first frame update
    void Start()
    {
        //caching
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 movement;
        movement = orientationCam.transform.forward * InputManager.movementInput.y * moveSpeed + orientationCam.transform.right * InputManager.movementInput.x * moveSpeed;
        movement.y = rb.velocity.y;
        rb.velocity = movement;

        if (HealthBar.health == 0 && isDead == false)
        {
            Death();
        }
    }

    private void Jump()
    {
        //Burst jump up
        if (groundCheck.isGrounded == true)
        {
            rb.AddForce(rb.transform.up * jumpForce);
        }
        
    }
    
    private void Attack()
    {
        if (Pistol.activeSelf)
        {
            if (Pistol != null)
            {
                AudioManager.Singleton.PlaySoundEffect("Pistol Shot");
                RaycastHit hit;
                if (Physics.Raycast(orientationCam.transform.position, orientationCam.transform.forward, out hit, gunRange))
                {
                    Enemy enemy = hit.transform.GetComponent<Enemy>();
                    if (enemy != null)
                    {
                        Debug.Log("Yay");
                        enemy.TakeDamage(hit, orientationCam,50);
                    }
                }
            }
        }
        else
        {
            if (Crowbar.activeSelf)
            {
                Crowbar.GetComponent<Animator>().Play("Crowbar-Attack");
                AudioManager.Singleton.PlaySoundEffect("Crowbar");
                Debug.Log("Crowbar");
                RaycastHit hit;
                if (Physics.Raycast(orientationCam.transform.position, orientationCam.transform.forward, out hit, meleeRange))
                {
                    Enemy enemy = hit.transform.GetComponent<Enemy>();
                    if (enemy != null)
                    {
                        enemy.TakeDamage(hit, orientationCam,35);
                    }
                }
            }
            
        }
       
    }
    private void Scroll()
    {
        if (InputManager.controls.Player.Scroll.ReadValue<float>() >= 1)
        {
            Pistol.SetActive(true);
            Crowbar.SetActive(false);
        }
        else if (InputManager.controls.Player.Scroll.ReadValue<float>() <= -1)
        {
            Pistol.SetActive(false);
            Crowbar.SetActive(true); ;
        }
    }

    private void Heal()
    {
        if (PlayerInventory.singleton.allPotions[0].count > 0)
        {
            HealthBar.health += 25;

            if (HealthBar.health >= 100f)
            {
                HealthBar.health = 100f;
            }

            PlayerInventory.singleton.allPotions[0].count--;
        }
    }

    private void Speed()
    {
        if (PlayerInventory.singleton.allPotions[1].count > 0)
        {
            PlayerInventory.singleton.allPotions[1].count--;
            StartCoroutine(SpeedBoostCoroutine());
        }
    }

    public void Death()
    {
        if (Time.timeScale == 1)
        {
            Time.timeScale = 0.25f;
        }
        QuadVideoPlayer.SetActive(true);
        
        currentDeathWaitTime += Time.deltaTime;
        if(currentDeathWaitTime >= DeathWaitTime / 2)
        {
            FadePlayer.SetActive(true);
        }
        if (currentDeathWaitTime >= DeathWaitTime)
        {
            isDead = true;
            currentDeathWaitTime = 0f;
            Time.timeScale = 1f;
            QuadVideoPlayer.SetActive(false);
            FadePlayer.SetActive(false);
            SceneManager.LoadSceneAsync(1);
        }
        
        
    }

    IEnumerator SpeedBoostCoroutine()
    {
        moveSpeed = 12;
        yield return new WaitForSeconds(5f);
        moveSpeed = 5;
    }

}
