using System;
using Player;
using UniRx;
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
    private IDisposable _movementSubHorizontal = null;
    private IDisposable _movementSubVertical = null;
    [SerializeField] private float playerScale;
    

    private void OnEnable()
    {
        rb.gravityScale = gravity;
         _movementSubHorizontal = MessageBroker.Default.Receive<HorizontalPlayerMoveEventArgs>().ObserveOnMainThread().Subscribe(SetMovement);
       _movementSubVertical =  MessageBroker.Default.Receive<VerticalPlayerMoveEventArgs>().ObserveOnMainThread().Subscribe(SetJump);
       transform.localScale = new Vector3(playerScale, playerScale, playerScale);
    }

    private void SetJump(VerticalPlayerMoveEventArgs obj)
    {
        movement.Set(movement.x,obj.AxisValue);
    }

    private void SetMovement(HorizontalPlayerMoveEventArgs obj)
    {
        movement.Set(obj.AxisValue, movement.y);
        if (movement.x > 0)
        {
          transform.localScale = new Vector3(playerScale, playerScale, playerScale);
        }
        else if (movement.x < 0)
        {
            transform.localScale = new Vector3(-(playerScale), playerScale, playerScale);
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

    private void OnDisable()
    {
        _movementSubHorizontal?.Dispose();
        _movementSubVertical?.Dispose();
    }
}