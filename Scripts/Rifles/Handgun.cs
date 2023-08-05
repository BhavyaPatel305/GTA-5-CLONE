using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Handgun : MonoBehaviour
{
    // RIFLE MOVEMENT VARIABLES: CODE COPIED FROM PLAYER SCRIPT
    // we want to have different walking speeds etc when player is using handguns

    // Now initially when the game starts playerController will be attached to player
    // then when player collects hand guns or uses it, we want to detach playerController
    // and add GunAnimator controller attached to it, along with that we want to have different walking speeds etc when player is using handguns
    // For that from PlayerScript copy these items:
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

    // END: Above Code from Player Script

    // RIFLE SHOOTING VARIABLES: Actual handgun script starts

    [Header("Rifle Things")]
    // reference to the main camera
    public Camera cam;
    // How much damage will this hand gun cause to different object
    public float giveDamage = 10f;
    // This is the range that one bullet or a ray cast of the hand gun will reach
    // Basically restrict shooting as we don't want to shoot to infinity distance
    public float shootingRange = 100f;
    // Transform for hand
    public Transform hand;
    // Solving the problem of player not turning
    public Transform PlayerTransform;

    // Adding FireCharge
    // It means that out player will shoot quickly
    // If fireCharge = 1, means that every 1 second we fire 1 bullet, handgun/player shoots
    // 2 means we fire 2 bullets in 1 second
    public float fireCharge = 10f;
    private float nextTimeToShoot = 0f;

    // Adding Ammo System, Mag System and Reloading System
    [Header("Rifle Ammunition and Reloading")]
    // Max ammo in 1 magazine will be 25
    private int maximumAmmunition = 25;
    // Number of magazine's this handgun will contain 
    private int mag = 10;
    // Ammunition that we currently have
    private int presentAmmunition = 0;
    // After every 4.3 seconds our handgun will reload one magazine
    public float reloadingTime = 4.3f;
    // By default value is false as on starting the game, handgun will not be reloading
    private bool setReloading = false;

    // Adding Muzzle Flash Particle System
    [Header("Rifle Effects")]
    // Reference to muzzle spark
    public ParticleSystem muzzleSpark;

    // Adding Impact Particle Effect
    // Adding bullet mark on the object, hit by the bullet
    // If the object being hit is a metal object than we have metalEffect
    public GameObject metalEffect;

    // Adding Ammo Out UI(Print Ammo Out Message on the screen)
    [Header("Sounds & UI")]
    // Reference to ammo out canvas
    public GameObject AmmoOutUI;


    private void Awake(){
        // Set hand as the parent of the hand gun
        // basically it will make this handgun child of this hand
        transform.SetParent(hand);
        // Whenever we play the game, we want the cursor to be locked
        Cursor.lockState = CursorLockMode.Locked;
        // Whenever the game starts, present ammo will be maximum
        presentAmmunition = maximumAmmunition;
    }


    // Update Function
    private void Update(){
        // FROM PLAYER SCRIPT

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

        // END: ABOVE CODE FROM PLAYER SCRIPT
        // If we are reloading, then do nothing
        if(setReloading){
            return;
        }
        // If presentAmmo is <= 0 then call Relod() function
        if(presentAmmunition <= 0){
            // Way to call a IEnumerator function
            StartCoroutine(Reload());
            return;
        }
        // Update function is called every second, but we don't want to shoot every second
        // So whenever a player presses fire button, we shoot(left button of the mouse/cursor)
        // if time is greater than next time to shoot
        if(Input.GetButton("Fire1") && Time.time >= nextTimeToShoot){
            // Then
            // If fireCharge = 1, means that every 1 second we fire 1 bullet, handgun/player shoots
            // 2 means we fire 2 bullets in 1 second
            nextTimeToShoot =  Time.time + 1f/fireCharge;
            Shoot();
        }
    }
    // FROM PLAYER SCRIPT
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
            // Animation for player moving in forward direction
            // When we are walking we don't want run to be tru
            animator.SetBool("WalkForward", true);
            animator.SetBool("RunForward", false);
            // Adding rotation to the player
            // Atan2() function will convert angle into radians
            // Rad2Deg will convert radians to degrees

            // In Unity, playerCamera.eulerAngles.y refers to the rotation angle around the vertical (Y) axis of a GameObject's transform. The eulerAngles property of a transform provides the rotation of the GameObject in terms of three Euler angles: one for each axis (X, Y, and Z).
            // Specifically, playerCamera.eulerAngles.y gives you the rotation angle in degrees around the Y-axis of the playerCamera GameObject. The Y-axis typically represents the vertical axis in Unity's coordinate system. 
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + playerCamera.eulerAngles.y;

            // Get the new angle value using SmoothDampAngle() method
            float angle = Mathf.SmoothDampAngle(PlayerTransform.eulerAngles.y, targetAngle, ref smoothTurnVelocity, smoothTurnTime);

            // Rotate the player using targetAngle
            // What this Quaternion.Euler() does is that it returns a rotation that rotates z degrees around z-axis
            PlayerTransform.rotation = Quaternion.Euler(0f, angle, 0f);
            
            // Get the new move direction using new targetAngle which includes angles from playerCamera
            Vector3 moveDirection = Quaternion.Euler(0f,targetAngle,0f) * Vector3.forward;

            // Now using character controller, we will move the player
            cC.Move(moveDirection.normalized * playerSpeed * Time.deltaTime);

            // If the player is moving, set jumpRange to 0f
            // As jumping animation is getting cut-off
            jumpRange = 0f;
        }else{
            // If player not moving then set walk and run animations false
            animator.SetBool("WalkForward", false);
            animator.SetBool("RunForward", false);
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
            // On Jumping set the idelAim false(means disable idle script)
            animator.SetBool("IdleAim", false);
            // Trigger jump animation
            animator.SetTrigger("Jump");

            velocity.y = Mathf.Sqrt(jumpRange * -2 * gravity);
        }else{
            // Start the Idle player animation
            animator.SetBool("IdleAim", true);
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
                // When we are running, we don't walk animation so set it to false
                animator.SetBool("WalkForward", false);
                // Start running animation
                animator.SetBool("RunForward", true);

                // Adding rotation to the player
                // Atan2() function will convert angle into radians
                // Rad2Deg will convert radians to degrees

                // In Unity, playerCamera.eulerAngles.y refers to the rotation angle around the vertical (Y) axis of a GameObject's transform. The eulerAngles property of a transform provides the rotation of the GameObject in terms of three Euler angles: one for each axis (X, Y, and Z).
                // Specifically, playerCamera.eulerAngles.y gives you the rotation angle in degrees around the Y-axis of the playerCamera GameObject. The Y-axis typically represents the vertical axis in Unity's coordinate system. 
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + playerCamera.eulerAngles.y;

                // Get the new angle value using SmoothDampAngle() method
                float angle = Mathf.SmoothDampAngle(PlayerTransform.eulerAngles.y, targetAngle, ref smoothTurnVelocity, smoothTurnTime);

                // Rotate the player using targetAngle
                // What this Quaternion.Euler() does is that it returns a rotation that rotates z degrees around z-axis
                PlayerTransform.rotation = Quaternion.Euler(0f, angle, 0f);
                
                // Get the new move direction using new targetAngle which includes angles from playerCamera
                Vector3 moveDirection = Quaternion.Euler(0f,targetAngle,0f) * Vector3.forward;

                // Now using character controller, we will move the player
                cC.Move(moveDirection.normalized * playerSprint * Time.deltaTime); 

                // If the player is moving, set jumpRange to 0f
                // As jumping animation is getting cut-off
                jumpRange = 0f;           
            }else{
                // If player is not running, start the walking animation and stop running animation
                animator.SetBool("WalkForward", true);
                animator.SetBool("RunForward", false);

                // When player is not moving, we will set the jump range back to 2f
                // As jumping animation is getting cut-off
                jumpRange = 1f; 
            }    
        }
    }
    // END: ABOVE CODE FROM PLAYER SCRIPT

    // Function for shooting
    // Shoot using ray-cast system in unity
    // Basically we will shoot a ray cast from our main camera that is following the player
    // in the forward direction and if that ray cast hits some character, then it will cause damage to it
    void Shoot(){
        // On Shooting, decrease the ammo by 1
        presentAmmunition--;
        // If presentAmmunition = 0 means 1 magazine is used, then decrement the mag by 1
        if(presentAmmunition == 0){
            mag--;
        }

        // If mag = 0 means no magazine is left, then show the ammo out text/UI
        if(mag == 0){
            // Show the ammo out text/UI
            StartCoroutine(ShowAmmoOut());
            return;
        }

        // Whenever Shoot() method is called, play the muzzle spark
        muzzleSpark.Play();

        // here we store information where our ray-cast has hit
        RaycastHit hitInfo;
        // We use Physics.Raycast() to cast a ray
        // Now we need to tell from where we want to cast a ray
        // we tell that as first argument: cam.tranform.position(Our main camera)
        // Now we tell this ray-cast in which direction we want it to go: cam.transform.forward
        // And now we want to store information about that object where the ray-cast hit: out hitInfo
        // then we want to restrict this ray: shootingRange
        if(Physics.Raycast(cam.transform.position, cam.transform.forward, out hitInfo, shootingRange)){
            // For now simply print the name of the object we hit in the console
            Debug.Log(hitInfo.transform.name);

            // Reference
            // Here Object is the Object.cs script
            // here we are simply saying that if we hit whatever object than that object has this Object.cs script(Which is used to give damage to any object)
            Object obj = hitInfo.transform.GetComponent<Object>();
            // If that object has Object script attached to itself than what we want to do
            // If object script is found
            if(obj != null){
                // We want to give damage to that object, so we call objectHitDamage() method from Object.cs file
                // and the amount of damage will be: giveDamage variable
                obj.objectHitDamage(giveDamage);
                // Here after damaging the object, show the metal effect
                // hitInfo.point means at whichever position of the object I shoot the bullet, exactly at that position draw the metal effect
                // For explanation of Quaternion.LookRotation(hitInfo.normal) watch Link
                // After few seconds we want this metal effect to be destroyed as we don't want it to be instantiated again and again in the game
                // For that we store it in a variable named metalEffectGo
                GameObject metalEffectGo = Instantiate(metalEffect, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
                // 1f to tell that after 1 second of instantiating it, destroy it
                Destroy(metalEffectGo, 1f);
            }
        }
    }
    // In unity, IEnumerator allows the program to yield things like wait for second function
    // Which lets you tell the script to wait without hoping onto the CPU
    // We can say, IEnumerator is basically used for simply pausing an iteration
    IEnumerator Reload(){
        // While reloading pause all other tasks and reload handgun first
        setReloading = true;
        // For testing purpose
        Debug.Log("Reloading...");
        // Wait for 4.3 seconds
        yield return new WaitForSeconds(reloadingTime);
        // When we are done reloading, again debug a message(for testing purpose)
        Debug.Log("Done Reloading...");
        // Again set the presentAmmunition to maxAmmunition
        presentAmmunition = maximumAmmunition;
        // When finished with reloading, set the setReloading to false
        setReloading = false;
    }
    // Function to display Ammo Out Message on the screen
    IEnumerator ShowAmmoOut(){
        // Set the Ammo out UI active
        AmmoOutUI.SetActive(true);
        // For 5 seconds
        yield return new WaitForSeconds(5f);
        // After that remove Ammo Out UI
        AmmoOutUI.SetActive(false);
    }
}
