using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Handgun : MonoBehaviour
{
    [Header("Rifle Things")]
    // reference to the main camera
    public Camera cam;
    // How much damage will this hand gun cause to different object
    public float giveDamage = 10f;
    // This is the range that one bullet or a ray cast of the hand gun will reach
    // Basically restrict shooting as we don't want to shoot to infinity distance
    public float shootingRange = 100f;

    // Adding FireCharge
    // It means that out player will shoot quickly
    // If fireCharge = 1, means that every 1 second we fire 1 bullet, handgun/player shoots
    // 2 means we fire 2 bullets in 1 second
    public float fireCharge = 10f;
    private float nextTimeToShoot = 0f;

    // Adding Muzzle Flash Particle System
    [Header("Rifle Effects")]
    // Reference to muzzle spark
    public ParticleSystem muzzleSpark;

    // Adding Impact Particle Effect
    // Adding bullet mark on the object, hit by the bullet
    // If the object being hit is a metal object than we have metalEffect
    public GameObject metalEffect;

    // Also whenever we play the game, we want the cursor to be locked
    private void Awake(){
        Cursor.lockState = CursorLockMode.Locked;
    }


    // Update Function
    private void Update(){
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

    // Function for shooting
    // Shoot using ray-cast system in unity
    // Basically we will shoot a ray cast from our main camera that is following the player
    // in the forward direction and if that ray cast hits some character, then it will cause damage to it
    void Shoot(){
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
}
