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

    // References to all scripts
    // So if player is not holding any weapon, then player script is active
    // If player is holding handgun, then handgun script is active and accordingly
    // So that whatever weapon player chooses, we play appropriate animations
    [Header("Scripts")]
    // Player Script
    public PlayerScript playerScript;
    // Short Gun Script
    public Shortgun shotgunScript;
    // Hand Gun
    // Right Hand
    public Handgun handgun1Script;
    // Left Hand
    public Handgun2 handgun2Script;
    // UZI
    // Right Hand
    public UZI uzi1Script;
    // Left Hand
    public UZI2 uzi2Script;
    // Bazooka
    public Bazooka bazooka;

    // Adding all variable in relation to the inventory
    [Header("Inventory")]
    public GameObject inventoryPanel;
    bool isPause = false;

    // Code to check if the player picks uo a rifle or not
    // then show the rifle in the player hand
    // according to the rifle, enable the specific script which is in relation with the rifle

    // If player picks Hand gun
    // -> Show handgun in inventory
    // -> Show it in players hands
    // -> Enable the inventory
    // -> Enable the handgun script
    void isRifleActive(){

    }
   

}
    