using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object : MonoBehaviour
{
    // Giving Damage to any random object
    // Health of any object to which this script is attached
    public float objectHealth = 120f;

    // Method to give damage to any object to which this script is attached
    // This amount will come from Handgun, which has variable named giveDamage
    public void objectHitDamage(float amount){
        // Reduce the objects health
        objectHealth -= amount;

        // If the object is dead
        if(objectHealth <= 0){
            // Call a function named Die()
            Die();
        }
    }

    void Die(){
        // Destroy that game object to which this script is attached
        Destroy(gameObject);
    }
}
