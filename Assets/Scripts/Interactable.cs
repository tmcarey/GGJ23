using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    [Header("Events")]
    public UnityEvent onInteract;

    public void Interact()
    {
        Debug.Log("Interact");
        onInteract.Invoke();        
    }
}
