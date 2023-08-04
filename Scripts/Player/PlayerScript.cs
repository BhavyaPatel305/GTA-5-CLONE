using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    // Header
    [Header("Player Movement")]
    // Speed of the player movement
    public float playerSpeed = 1.1f;

    // Player Animator and Gravity
    [Header("Player Animator & Gravity")]
    // Attach player's character controller
    public CharacterController cC;

    // Adding Gravity to the player
    public float gravity = -9.81f;

    // Moving player in the direction where the camera points, and camera moves using the cursor movement
    [Header("Player Script Camera")]
    // Reference to main camera
    public Transform playerCamera;

    [Header("Player Jumping & Velocity")]
    // To make the player rotation smooth
    public float smoothTurnTime = 0.1f;
    private float smoothTurnVelocity;

    Vector3 velocity;
    public Transform surfaceCheck;
    bool onSurface;
    public float surfaceDistance = 0.4f;
    public LayerMask surfaceMask;

    // Update Function
    private void Update(){
        // Check if the player is on the surface or not
        onSurface = Physics.CheckSphere(surfaceCheck.position, surfaceDistance, surfaceMask);

        // If player is on ground
        if(onSurface && velocity.y < 0f){
            // Reset the velocity
            velocity.y = -2f;
        }

        // Gravity
        velocity.y += gravity * Time.deltaTime;
        cC.Move(velocity * Time.deltaTime);
        
        // Call the playerMove function
        playerMove();
    }
    // Method to move the player
    void playerMove(){
        // Get the input from the player
        // If player press, < arrow key or A key then value = 1
        // If player press, > arrow key or D key then value = -1
        float horizontal_axis = Input.GetAxisRaw("Horizontal");
        // If player press, ^ arrow key or W key then value = 1
        // If player press, v arrow key or S key then value = -1
        float vertical_axis = Input.GetAxisRaw("Vertical");

        // Provide direction to the player
        // X-axis: horizontal_axis, Y-axis: 0f(no movement), Z-axis: vertical_axis
        Vector3 direction = new Vector3(horizontal_axis, 0f, vertical_axis).normalized;

        // direction.magnitude >= 0.1f meaning player is moving
        if(direction.magnitude >= 0.1f){
            // Adding rotation to the player
            // Atan2() function will convert angle into radians
            // Rad2Deg will convert radians to degrees

            // In Unity, playerCamera.eulerAngles.y refers to the rotation angle around the vertical (Y) axis of a GameObject's transform. The eulerAngles property of a transform provides the rotation of the GameObject in terms of three Euler angles: one for each axis (X, Y, and Z).
            // Specifically, playerCamera.eulerAngles.y gives you the rotation angle in degrees around the Y-axis of the playerCamera GameObject. The Y-axis typically represents the vertical axis in Unity's coordinate system. 
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + playerCamera.eulerAngles.y;

            // Get the new angle value using SmoothDampAngle() method
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref smoothTurnVelocity, smoothTurnTime);

            // Rotate the player using targetAngle
            // What this Quaternion.Euler() does is that it returns a rotation that rotates z degrees around z-axis
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            
            // Get the new move direction using new targetAngle which includes angles from playerCamera
            Vector3 moveDirection = Quaternion.Euler(0f,targetAngle,0f) * Vector3.forward;

            // Now using character controller, we will move the player
            cC.Move(moveDirection.normalized * playerSpeed * Time.deltaTime);
        }
    }
}
