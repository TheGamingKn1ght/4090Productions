using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    private Iinteractable _currentInteractable;
    [SerializeField] private float rayCastDistance = 5f;
    [SerializeField] Transform orientationCam;
    
    //public string tag;
    public static RaycastHit hit;

    private void OnEnable()
    {
        InputManager.OnInteractInput += () => Interact();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastTarget();
    }

    private void RaycastTarget()
    {
        if (Physics.Raycast(orientationCam.transform.position, orientationCam.transform.forward, out hit, rayCastDistance))
        {
            Iinteractable interactable = hit.collider.GetComponent<Iinteractable>();
            if (interactable != null)
            {
                _currentInteractable = interactable;
            }
            else
            {
                _currentInteractable = null;
            }
        }

    }
    private void Interact()
    {
        _currentInteractable?.Interact();
        _currentInteractable = null;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.forward * rayCastDistance);
    }
}
