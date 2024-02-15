using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Gun Stuff
    public float damage = 10f;
    public float range = 100f;
    public float impactForce = 500000;

    Rigidbody rb;
    [SerializeField] GroundCheck groundCheck;

    [SerializeField] int moveSpeed = 1;
    [SerializeField] int jumpForce = 300;

    [SerializeField] Transform orientationCam;

    public GameObject Pistol;
    public GameObject Sword;

    private void OnEnable()
    {
        InputManager.OnJumpInput += () => Jump();
        InputManager.OnShootInput += () => Shoot();
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
    }

    private void Jump()
    {
        //Burst jump up
        if (groundCheck.isGrounded == true)
        {
            rb.AddForce(rb.transform.up * jumpForce);
        }
        
    }

    private void Shoot()
    {
        
        if(Pistol != null)
        {
            Debug.Log("Shoot");
            RaycastHit hit;
            if (Physics.Raycast(orientationCam.transform.position, orientationCam.transform.forward, out hit, range))
            {
                Debug.Log(hit.transform.name);
                Target target = hit.transform.GetComponent<Target>();
                if (target != null)
                {
                    target.TakeDamage(damage);
                }
            }
        }
        
        Sword.GetComponent<Animator>().Play("Crowbar-Attack");

    
    }
    private void Scroll()
    {
        Debug.Log("Scroll" + InputManager.ScrollInput);
    }

}
