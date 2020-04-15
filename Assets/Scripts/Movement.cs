using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Enumerator to make it easier to describe which direction
/// player is determined to be facing in code
/// </summary>
public enum Face { 
    Down,
    Left,
    Right,
    Up,
}


public class Movement : MonoBehaviour
{
    // moveSpeed, input movement
    public float moveSpeed;
    public Vector2 movement;

    // rigidBody to move player
    // Animator to control animations
    public Rigidbody2D rigidBody;
    public Animator animator;

    // facing direction in int
    private int direction;

    // Start is called before the first frame update
    void Start()
    {
        direction = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // get input from Input buffer
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        movement.Normalize();

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);

        animator.SetFloat("Speed", movement.sqrMagnitude);

        animator.SetFloat("Facing", Facing());
    }

    //Update in set timeframes player velocity
    private void FixedUpdate()
    {
        rigidBody.velocity = movement * moveSpeed;
    }

    /// <summary>
    /// Method to return current facing direction
    /// </summary>
    /// <returns>direction int that player is facing</returns>
    public int Facing()
    {
        if(movement.x > 0)
        {
            direction = (int)Face.Right;
        }
        else if (movement.y > 0)
        {
            direction = (int)Face.Up;
        }
        else if (movement.x < 0)
        {
            direction = (int)Face.Left;
        }
        else if (movement.y < 0)
        {
            direction = (int)Face.Down;
        }

        return direction;
    }
}
