using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    [Header("Events")]
    public UnityEvent onInteract;

    public void Interact()
    {
        onInteract.Invoke();        
    }
}
