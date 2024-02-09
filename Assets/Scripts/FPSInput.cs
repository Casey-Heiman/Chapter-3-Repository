using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// FPSInput script for basic WASD-style movement control with collision detection

// Ensure that this script is attached to a GameObject with a CharacterController component
[RequireComponent(typeof(CharacterController))]
[AddComponentMenu("Control Script/FPS Input")]
public class FPSInput : MonoBehaviour
{
    // Public variables for movement speed and gravity
    public float speed = 6.0f;
    public float gravity = -9.8f;

    // Private reference to the CharacterController component
    private CharacterController charController;

    // Called once when the script is first loaded
    void Start()
    {
        // Get the CharacterController component attached to the GameObject
        charController = GetComponent<CharacterController>();
    }

    // Called every frame
    void Update()
    {
        // Uncommented line for basic movement without collision detection
        // transform.Translate(Input.GetAxis("Horizontal") * speed * Time.deltaTime, 0, Input.GetAxis("Vertical") * speed * Time.deltaTime);

        // Get input values for horizontal and vertical axes
        float deltaX = Input.GetAxis("Horizontal") * speed;
        float deltaZ = Input.GetAxis("Vertical") * speed;

        // Create a movement vector based on input values
        Vector3 movement = new Vector3(deltaX, 0, deltaZ);

        // Limit the magnitude of the movement vector to the defined speed
        movement = Vector3.ClampMagnitude(movement, speed);

        // Apply gravity to the y component of the movement vector
        movement.y = gravity;

        // Scale the movement vector by deltaTime to make it frame-rate independent
        movement *= Time.deltaTime;

        // Transform the movement vector from local space to world space
        movement = transform.TransformDirection(movement);

        // Move the CharacterController based on the final movement vector
        charController.Move(movement);
    }
}

