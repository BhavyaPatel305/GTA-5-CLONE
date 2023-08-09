using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    

    [Header("Item Slots")]
    // Reference to each rifle UI
    public GameObject Weapon1;
    // Bool to check if player has picked up Weapon1  or not
    public bool isWeapon1Picked = false;
    // Check if the weapon is active or not
    public bool isWeapon1Active = false;

    // For Weapon 2
    // Reference to each rifle UI
    public GameObject Weapon2;
    // Bool to check if player has picked up Weapon2  or not
    public bool isWeapon2Picked = false;
    // Check if the weapon is active or not
    public bool isWeapon2Active = false;

    // For Weapon 3
    // Reference to each rifle UI
    public GameObject Weapon3;
    // Bool to check if player has picked up Weapon3  or not
    public bool isWeapon3Picked = false;
    // Check if the weapon is active or not
    public bool isWeapon3Active = false;

    // For Weapon 4
    // Reference to each rifle UI
    public GameObject Weapon4;
    // Bool to check if player has picked up Weapon4  or not
    public bool isWeapon4Picked = false;
    // Check if the weapon is active or not
    public bool isWeapon4Active = false;


   // Reference to all the weapons inside the players left and right hand
   // In Unity -> Player -> Left Hand -> Left Hand Thumb -> Rifle Holder -> We have 4 weapons(We need reference of these 4 weapons) 
   // Similarly for right hand

   // Using Weapon UI, as soon as player picks up a weapon, show Weapon UI in Inventory
   // Using Weapon references in Hand, as soon as player picks up a weapon, show that weapon in players hands
   [Header("Weapons to Use")]
   // We have 2 hand guns(left hand & right hand)
   public GameObject HandGun1;
   public GameObject HandGun2; 

   // Shot gun
   public GameObject ShotGun;

   // UZI
   // We have 2 UZI's (left hand & right hand)
   public GameObject UZI1;
   public GameObject UZI2;

   // Bazooka
   public GameObject Bazooka;
}
    