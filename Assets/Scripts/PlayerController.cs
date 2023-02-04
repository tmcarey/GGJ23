using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Setup")] [SerializeField] private Rigidbody2D rigidBody;

    [Header("Config")] [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float moveLerpSpeed = 5f;

    private Vector2 moveDir;

    private void OnMove(InputValue value)
    {
        moveDir = value.Get<Vector2>() * moveSpeed;
    }

    private void Update()
    {
        rigidBody.velocity = Vector3.Lerp(rigidBody.velocity, new Vector3(moveDir.x, moveDir.y),
            moveLerpSpeed * Time.deltaTime);
    }
}