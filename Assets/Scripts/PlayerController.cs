using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerController : MonoBehaviour
{
    //Weapon Ranges
    public float gunRange = 100f;
    public float meleeRange = 10f;
    public Quest quest;


    Rigidbody rb;
    [SerializeField] GroundCheck groundCheck;

    [SerializeField] private int moveSpeed = 1;
    [SerializeField] int jumpForce = 300;

    [SerializeField] Transform orientationCam;
    [SerializeField] private GameObject cureSmoke;

    [SerializeField] private GameObject QuadVideoPlayer;
    [SerializeField] private GameObject FadePlayer;

    private int counter = 0;

    [SerializeField] private float DeathWaitTime = 1.2f;
    private float currentDeathWaitTime = 0f;
    private bool isDead = false;

    public GameObject Pistol;
    public GameObject Crowbar;

    [SerializeField] private AudioSource footsteps;
    private float footstepsLength = 6.217143f;
    private float footstepsWaitTime = 0f;
    private bool isWalking;

    private int currentBullets;
    [SerializeField] private TextMeshProUGUI ammoCount;

    Vector3 playerPos;
    Vector3 playerPos2;

    private void OnEnable()
    {
        InputManager.OnJumpInput += Jump;
        InputManager.OnShootInput += Attack;
        InputManager.OnScrollInput += Scroll;
        InputManager.OnHealInput += Heal;
        InputManager.OnSpeedInput += Speed;
        InputManager.OnCureInput += ActivateCure;
    }

    private void OnDisable()
    {
        InputManager.OnJumpInput -= Jump;
        InputManager.OnShootInput -= Attack;
        InputManager.OnScrollInput -= Scroll;
        InputManager.OnHealInput -= Heal;
        InputManager.OnSpeedInput -= Speed;
        InputManager.OnSpeedInput -= ActivateCure;
    }

    // Start is called before the first frame update
    void Start()
    {
        playerPos = this.transform.position;
        //caching
        rb = GetComponent<Rigidbody>();
        currentBullets = 50;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 movement;
        movement = orientationCam.transform.forward * InputManager.movementInput.y * moveSpeed + orientationCam.transform.right * InputManager.movementInput.x * moveSpeed;
        movement.y = rb.velocity.y;
        rb.velocity = movement;

        walkingSound();
        ammoCount.text = currentBullets.ToString();

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
    public void StopMoving()
    {
        rb.velocity = Vector3.zero;
    }
    
    private void Attack()
    {
        if (Pistol.activeSelf && currentBullets > 0 && Cursor.visible == false)
        {
            if (Pistol != null)
            {
                AudioManager.Singleton.PlaySoundEffect("Pistol Shot");
                RaycastHit hit;

                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit, gunRange))
                {
                    Debug.Log(hit.transform.gameObject.name);
                    currentBullets--;
                    Enemy enemy = hit.transform.GetComponent<Enemy>();
                    if (enemy != null)
                    {
                        enemy.TakeDamage(50);
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
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit, meleeRange))
                {
                    Debug.Log(hit.transform.gameObject.name);
                    Enemy enemy = hit.transform.GetComponent<Enemy>();
                    if (enemy != null)
                    {
                        enemy.TakeDamage(35);
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
            Crowbar.SetActive(true);
        }
    }

    private void walkingSound()
    {
        footstepsWaitTime += Time.deltaTime;
        if (footstepsWaitTime >= footstepsLength)
        {
            isWalking = false;
            footstepsWaitTime = 0f;
        }

        playerPos2 = this.transform.position;
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            isWalking = true;
            playerPos = this.transform.position;
            if (footsteps.enabled == false && isWalking == true)
            {
                footsteps.enabled = true;
            }
            else if(isWalking == false)
            {
                footsteps.enabled = false;
            }
            
        }
        else 
        {
            footsteps.enabled = false;
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
            StartCoroutine(SpeedBoostCoroutine(5f));
        }
    }
    private void ActivateCure()
    {
        if(NoteInventory.NumberOfNotes == 11)
        {
            cureSmoke.SetActive(true);
            EnemySpawning.cureDiscovered = true;
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

    IEnumerator SpeedBoostCoroutine(float amount)
    {
        moveSpeed = 12;
        yield return new WaitForSeconds(amount);
        moveSpeed = 5;
    }

    private void OnDrawGizmos()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(ray.origin, ray.direction * 20);
        //Gizmos.DrawLine(orientationCam.transform.position, orientationCam.transform.forward * 100);
    }

}
