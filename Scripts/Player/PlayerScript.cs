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

    // Update Function
    private void Update(){
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
            // Now using character controller, we will move the player
            cC.Move(direction.normalized * playerSpeed * Time.deltaTime);
        }
    }
}
