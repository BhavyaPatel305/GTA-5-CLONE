using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    // Header
    [Header("Player Movement")]
    // Speed of the player movement
    public float playerSpeed = 1.1f;
    // Adding sprinting/running functionality to the player
    public float playerSprint = 5f;

    // Player Animator and Gravity
    [Header("Player Animator & Gravity")]
    // Attach player's character controller
    public CharacterController cC;

    // Adding Gravity to the player
    public float gravity = -9.81f;

    // Adding the animation to the player
    public Animator animator;

    // Moving player in the direction where the camera points, and camera moves using the cursor movement
    [Header("Player Script Camera")]
    // Reference to main camera
    public Transform playerCamera;

    [Header("Player Jumping & Velocity")]
    // Adding player jump: On pressing space bar, the player should jump
    public float jumpRange = 1f;

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
        // Call the Jump function
        Jump();
        // Call the Sprint function
        Sprint();
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

            // If the player is moving then play some animations 
            // "Walk", these names should be same as parameters of the animator controller(For Basic Locomotion)
            animator.SetBool("Walk", true);
            // If player is walking, we make the running false as we don't want player to run when it is walking
            animator.SetBool("Running", false);

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

            // If the player is moving, set jumpRange to 0f
            // As jumping animation is getting cut-off
            jumpRange = 0f;
        }else{
            // If the player is not moving, then make both walking and running false
            animator.SetBool("Walk", false);
            animator.SetBool("Running", false);

            // When player is not moving, we will set the jump range back to 2f
            // As jumping animation is getting cut-off
            jumpRange = 1f; 
        }
    }
    
    // Adding Jump to the player
    void Jump(){
        // In Unity -> Edit -> Project Settings -> Input Manager -> Axes -> Jump
        // Here "Jump" is this jump, say in there if instead of space key we change some other key for "Jump" then we would not have to do any changes in this code
        // If jump key is pressed and player is on surface then jump
        if(Input.GetButtonDown("Jump") && onSurface){
            // Adding Jump animation
            // If we press jump button, then set Idle to false
            animator.SetBool("Idle", false);
            // Since Jump is a Trigger, Let's Trigger the jump animation
            animator.SetTrigger("Jump");

            velocity.y = Mathf.Sqrt(jumpRange * -2 * gravity);
        }else{
            // When player is done jumping, we want to reset trigger and set the Idle to true
            animator.SetBool("Idle", true);
            animator.ResetTrigger("Jump");
        }
    }

    // Adding sprinting/running functionality to the player
    void Sprint(){
        // First we need to add a new button named Sprint in Input Manager
        //In Unity -> Edit -> Project Settings -> Input Manager -> Fire3(rename it to Sprint) & mouse2 from Alt Positive Button

        // If I have pressed left shift key + ^ arrow Key or W + Player is on surface
        if(Input.GetButton("Sprint") && (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) && onSurface){
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
                // If player is sprinting, then set the sprinting/running animation to true
                // And set Walk animation to false
                animator.SetBool("Walk", false);
                animator.SetBool("Running", true);

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
                cC.Move(moveDirection.normalized * playerSprint * Time.deltaTime); 

                // If the player is moving, set jumpRange to 0f
                // As jumping animation is getting cut-off
                jumpRange = 0f;           
            }else{
                // If the player is not sprinting then set Walking animation to true and Running animation to false
                animator.SetBool("Walk", true);
                animator.SetBool("Running", false);

                // When player is not moving, we will set the jump range back to 2f
                // As jumping animation is getting cut-off
                jumpRange = 1f; 
            }    
        }
    }
}
