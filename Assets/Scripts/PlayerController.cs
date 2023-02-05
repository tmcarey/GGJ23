using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Setup")] [SerializeField] private Rigidbody rigidBody;
    [SerializeField] private Camera playerCamera;

    [Header("Config")] [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float moveLerpSpeed = 5f;
    [SerializeField] private float zoomSpeed = 5f;
    [SerializeField] private float zoomLoopSpeed = 5f;
    [SerializeField] private float minZoom = 5f;
    [SerializeField] private float maxZoom = 5f;

    private Vector2 moveDir;
    private float zoom;

    private void Awake()
    {
        zoom = playerCamera.orthographicSize;
    }

    private void OnZoom(InputValue value)
    {
        zoom -= value.Get<float>() * zoomSpeed * Time.deltaTime;
    }

    private void OnMove(InputValue value)
    {
        moveDir = value.Get<Vector2>() * moveSpeed;
    }
    
    private void OnPause(InputValue value)
    {
        GameManager.Instance.TogglePause();
    }

    private void Update()
    {
        rigidBody.velocity = Vector3.Lerp(rigidBody.velocity, new Vector3(moveDir.x, 0, moveDir.y),
            moveLerpSpeed * Time.deltaTime);
        
        zoom = Mathf.Clamp(zoom, minZoom, maxZoom);
        playerCamera.orthographicSize = Mathf.Lerp(playerCamera.orthographicSize, zoom, zoomLoopSpeed * Time.deltaTime);
    }
}