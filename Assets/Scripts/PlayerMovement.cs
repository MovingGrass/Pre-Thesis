using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
     public CharacterController controller; // Assign the CharacterController component here

    public float speed = 12f;
    public float gravity = -9.81f * 2; // Adjusted gravity as suggested
    public float jumpHeight = 3f;

    public Transform groundCheck;       // Assign the 'GroundCheck' empty GameObject here
    public float groundDistance = 0.4f; // Radius of the sphere check
    public LayerMask groundMask;        // Set this to the 'Ground' layer in the inspector

    Vector3 velocity;
    bool isGrounded;

    void Update()
    {
        // Ground Check
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        // Reset velocity if grounded
        if (isGrounded && velocity.y < 0)
        {
            // Setting to -2f instead of 0f forces the player onto the ground slightly
            // which feels better and prevents bouncing on slopes.
            velocity.y = -2f;
        }

        // --- Player Movement Input ---
        float x = Input.GetAxis("Horizontal"); // A/D or Left/Right Arrow
        float z = Input.GetAxis("Vertical");   // W/S or Up/Down Arrow

        // Calculate movement direction based on player orientation
        Vector3 move = transform.right * x + transform.forward * z;

        // Apply movement to the controller
        controller.Move(move * speed * Time.deltaTime);

        // --- Jumping ---
        if (Input.GetButtonDown("Jump") && isGrounded) // "Jump" is space bar by default
        {
            // Calculate jump velocity based on the physics equation: v = sqrt(h * -2 * g)
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // --- Gravity ---
        // Apply gravity to velocity
        velocity.y += gravity * Time.deltaTime;

        // Apply velocity (gravity pull) to the controller
        // Needs to be multiplied by Time.deltaTime again based on physics formula (d = 1/2*g*t^2)
        controller.Move(velocity * Time.deltaTime);
    }
}
