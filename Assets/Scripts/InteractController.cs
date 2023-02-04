using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InteractController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnInteract(InputValue val)
    { 
        if(_interactable)
            _interactable.Interact();
    }

    private Interactable _interactable;

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.GetRayIntersection(ray,Mathf.Infinity);
        
        if(hit)
            Debug.Log(hit.collider);
        
        if(hit.collider != null)
        {
            if(hit.collider.TryGetComponent(out Interactable interactable))
            {
                _interactable = interactable;
            }
            else
            {
                _interactable = null;
            }
        }
        else
        {
                _interactable = null;
        }
        
    }
}
