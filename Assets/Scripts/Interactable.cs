using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    [Header("Events")]
    public UnityEvent onInteract;
    public UnityEvent<bool> onHover;

    public void Interact()
    {
        Debug.Log("Interact");
        onInteract.Invoke();        
    }

    public void OnHover(bool isHovering)
    {
        Debug.Log("OnHover");
        onHover.Invoke(isHovering);
    }

}
