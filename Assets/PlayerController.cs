using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Setup")] [SerializeField] private Rigidbody2D rigidBody;

    [Header("Config")] [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float jumpForce = 10f;

    private void OnMove(InputValue value)
    {
        var move = value.Get<Vector2>();
        rigidBody.velocity = new Vector2(move.x * moveSpeed, rigidBody.velocity.y);
    }

    private void OnJump(InputValue value)
    {
        rigidBody.AddForce(jumpForce * Vector2.up, ForceMode2D.Impulse);
    }

}