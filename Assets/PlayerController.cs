using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Setup")] 
    [SerializeField] private Rigidbody2D rigidBody;
    
    [Header("Config")]
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float jumpForce = 10f;

}
