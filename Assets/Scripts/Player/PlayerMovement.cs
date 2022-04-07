using System;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerMovement : MonoBehaviour
{
    //player movement variables
    public float speed = 15f;
    public float jumpSpeed = 8f;
    public float gravity = 8f;
    public bool canJump = true;
    public bool grounded = false;
    public Rigidbody2D rb;
    [SerializeField] private Vector2 movement = Vector2.zero;
    [SerializeField] private SpriteRenderer spriteRenderer;

    private void OnEnable()
    {
        rb.gravityScale = gravity;
    }

    void Update()
    {
        //listen for input
        movement.Set(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        //when moving, flip the sprite
        if (movement.x > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (movement.x < 0)
        {
            spriteRenderer.flipX = true;
        }
    }

    private void FixedUpdate()
    {
        //move the player
        var velocity = rb.velocity;
        Vector3 targetVelocity = new Vector2(movement.x * speed, velocity.y);
        rb.velocity = Vector3.SmoothDamp(velocity, targetVelocity, ref targetVelocity, Time.fixedTime);
        //jump
        if (Input.GetButtonDown("Jump") && canJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
        }

        //check if grounded
        grounded = rb.velocity.y == 0;
    }
}