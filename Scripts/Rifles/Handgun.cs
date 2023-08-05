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

    // Update Function
    private void Update(){
        // Update function is called every second, but we don't want to shoot every second
        // So whenever a player presses fire button, we shoot(left button of the mouse/cursor)
        if(Input.GetButtonDown("Fire1")){
            Shoot();
        }
    }

    // Function for shooting
    // Shoot using ray-cast system in unity
    // Basically we will shoot a ray cast from our main camera that is following the player
    // in the forward direction and if that ray cast hits some character, then it will cause damage to it
    void Shoot(){
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
            }
        }
    }
}
