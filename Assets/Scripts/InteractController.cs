using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InteractController : MonoBehaviour
{
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
        bool hit = Physics.Raycast(ray,out var hitInfo, Mathf.Infinity);
        
        if(hit)
        {
            if(hitInfo.collider.TryGetComponent(out Interactable interactable))
            {
                Debug.Log("Hovering over interactable");
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
