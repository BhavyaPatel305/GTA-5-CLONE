using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchCamera : MonoBehaviour
{
    // Adding CrossHair/Aiming functionality
    // We need this SwitchCamera script as without it if we run the game
    // we can just see cross hair in the middle of the screen and if we turn the player
    // in some other direction than the direction of the gun pointing and the cross hair would be different

    // We will be switching from 3rd person camera to aim camera, so for that we need different header names
    [Header("Camera to Assign")]
    // So we will have 2 references
    public GameObject AimCam;
    public GameObject ThirdPersonCam;
    // Reference to Animator for aiming animations(Shooting)
    public Animator animator;

    private void Update(){
        // If we press mouse right button and if we are walking
        // Meaning if we are aiming and moving at the same time than what we want to do
        if(Input.GetButton("Fire2") && (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))){
            // Start the animation for Aim Walk
            animator.SetBool("AimWalk", true);
            // When we are aiming and walking, we only want AimWalk to be true not ShootAim
            animator.SetBool("ShootAim", false);
            // We want the 3rd person camera to be disabled
            ThirdPersonCam.SetActive(false);
            // Activate the aim cam
            AimCam.SetActive(true);
        }
        // If player is just pressing mouse right button
        else if(Input.GetButton("Fire2")){
            // Start the animation for ShootAim and AimWalk when we are shooting
            // As we can both aim and shoot together
            animator.SetBool("ShootAim", true);
            animator.SetBool("AimWalk",true);
            // We want the 3rd person camera to be disabled
            ThirdPersonCam.SetActive(false);
            // Activate the aim cam
            AimCam.SetActive(true);
        }else{
            // If player is not aiming or shooting, stop aiming and shooting animations
            animator.SetBool("ShootAim", false);
            animator.SetBool("AimWalk",false);
            // If the player is not aiming, activate the 3rd person camera and aim cam to be deactivated
            ThirdPersonCam.SetActive(true);
            AimCam.SetActive(false);
        }
    }


}
