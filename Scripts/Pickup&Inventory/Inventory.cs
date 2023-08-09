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

    // Fixing Issue: Camera should not move when player opens Inventory UI
    // References to Camera's
    public SwitchCamera camera;
    public GameObject AimCam;
    public GameObject ThirdPersonCam;

    // Code to draw weapons based on key press
    // If player presses 
    // ->   1 then draw hand gun
    // ->   2 then draw shot gun
    // ->   3 then draw UZI
    // ->   4 then draw Bazooka
    private void Update(){
        // If user presses tab key, then show the inventory
        if(Input.GetKeyDown("tab")){
            Debug.Log("TAB KEY IS PRESSED");
            // If isPause true meaning, game is paused 
            // then call hide Inventory method
            if(isPause == true){
                hideInventory();
            }// Else call showInventory() method
            else{
                showInventory();
            }
        }
        // For drawing hand gun -> Condition: Pressed 1 AND isWeapon1Picked = true
        if(Input.GetKeyDown("1") && isWeapon1Picked == true){
            // Set Weapon 1 as active weapon
            isWeapon1Active = true;
            // Set other weapons as inactive
            isWeapon2Active = false;
            isWeapon3Active = false;
            isWeapon4Active = false;
            isRifleActive();
        // For drawing shot gun -> Condition: Pressed 2 AND isWeapon2Picked = true
        }else if(Input.GetKeyDown("2") && isWeapon2Picked == true){
            // Set Weapon 2 as active weapon
            isWeapon2Active = true;
            // Set other weapons as inactive
            isWeapon1Active = false;
            isWeapon3Active = false;
            isWeapon4Active = false;
            isRifleActive();
        // For drawing UZI -> Condition: Pressed 3 AND isWeapon3Picked = true
        }else if(Input.GetKeyDown("3") && isWeapon3Picked == true){
            // Set Weapon 3 as active weapon
            isWeapon3Active = true;
            // Set other weapons as inactive
            isWeapon1Active = false;
            isWeapon2Active = false;
            isWeapon4Active = false;
            isRifleActive();
        // For drawing Bazooka -> Condition: Pressed 4 AND isWeapon4Picked = true
        }else if(Input.GetKeyDown("4") && isWeapon4Picked == true){
            // Set Weapon 4 as active weapon
            isWeapon4Active = true;
            // Set other weapons as inactive
            isWeapon1Active = false;
            isWeapon3Active = false;
            isWeapon2Active = false;
            isRifleActive();
        }
    }
    
    // Code to check if the player picks uo a rifle or not
    // then show the rifle in the player hand
    // according to the rifle, enable the specific script which is in relation with the rifle

    // If player picks Hand gun
    // -> Show handgun in inventory
    // -> Show it in players hands
    // -> Enable the inventory
    // -> Enable the handgun script
    void isRifleActive(){
        // If Weapon 1 is active -> HandGun
        if(isWeapon1Active){
            // Right hand
            HandGun1.SetActive(true);
            // Left hand
            HandGun2.SetActive(true);

            // Deactivate other weapons
            ShotGun.SetActive(false);
            UZI1.SetActive(false);
            UZI2.SetActive(false);
            Bazooka.SetActive(false);

            // After showing weapon in player's hands, enable the script for it as well
            // When handgun script is activated, disable all other scripts

            // Enable handgun
            playerScript.GetComponent<PlayerScript>().enabled = false;
            shotgunScript.GetComponent<Shortgun>().enabled = false;
            
            handgun1Script.GetComponent<Handgun>().enabled = true;
            handgun2Script.GetComponent<Handgun2>().enabled = true;

            uzi1Script.GetComponent<UZI>().enabled = false;
            uzi2Script.GetComponent<UZI2>().enabled = false;
            bazooka.GetComponent<Bazooka>().enabled = false;
        }
        // If Weapon 2 is active -> ShotGun
        else if(isWeapon2Active){
            // Activate Shotgun
            ShotGun.SetActive(true);

            // Deactivate other weapons
            // Right hand
            HandGun1.SetActive(false);
            // Left hand
            HandGun2.SetActive(false);
            UZI1.SetActive(false);
            UZI2.SetActive(false);
            Bazooka.SetActive(false);

            // After showing weapon in player's hands, enable the script for it as well
            // When Shotgun script is activated, disable all other scripts

            // Enable ShotGun
            playerScript.GetComponent<PlayerScript>().enabled = false;
            shotgunScript.GetComponent<Shortgun>().enabled = true;
            
            handgun1Script.GetComponent<Handgun>().enabled = false;
            handgun2Script.GetComponent<Handgun2>().enabled = false;

            uzi1Script.GetComponent<UZI>().enabled = false;
            uzi2Script.GetComponent<UZI2>().enabled = false;
            bazooka.GetComponent<Bazooka>().enabled = false;
        }
        // If Weapon 3 is active -> HandGun
        else if(isWeapon3Active){
            // Right hand
            UZI1.SetActive(true);
            // Left hand
            UZI2.SetActive(true);

            // Deactivate other weapons
            ShotGun.SetActive(false);
            HandGun1.SetActive(false);
            HandGun2.SetActive(false);
            Bazooka.SetActive(false);

            // After showing weapon in player's hands, enable the script for it as well
            // When UZI script is activated, disable all other scripts

            // Enable UZI
            playerScript.GetComponent<PlayerScript>().enabled = false;
            shotgunScript.GetComponent<Shortgun>().enabled = false;
            
            handgun1Script.GetComponent<Handgun>().enabled = false;
            handgun2Script.GetComponent<Handgun2>().enabled = false;

            uzi1Script.GetComponent<UZI>().enabled = true;
            uzi2Script.GetComponent<UZI2>().enabled = true;

            bazooka.GetComponent<Bazooka>().enabled = false;
        }
        // If Weapon 4 is active -> ShotGun
        else if(isWeapon4Active){
            // Activate Bazooka
            Bazooka.SetActive(true);

            // Deactivate other weapons
            // Right hand
            HandGun1.SetActive(false);
            // Left hand
            HandGun2.SetActive(false);
            UZI1.SetActive(false);
            UZI2.SetActive(false);
            ShotGun.SetActive(false);

            // After showing weapon in player's hands, enable the script for it as well
            // When Bazooka script is activated, disable all other scripts

            // Enable Bazooka
            playerScript.GetComponent<PlayerScript>().enabled = false;
            shotgunScript.GetComponent<Shortgun>().enabled = false;
            
            handgun1Script.GetComponent<Handgun>().enabled = false;
            handgun2Script.GetComponent<Handgun2>().enabled = false;

            uzi1Script.GetComponent<UZI>().enabled = false;
            uzi2Script.GetComponent<UZI2>().enabled = false;
            bazooka.GetComponent<Bazooka>().enabled = true;
        }
    }
    // Function to show and hide inventory
    void showInventory(){
        // When showing Inventory, Disable switch camera script
        camera.GetComponent<SwitchCamera>().enabled = false;
        // Disable 3rd person camera and Aim Cam
        ThirdPersonCam.SetActive(false);
        AimCam.SetActive(false);

        // Set inventory panel to be true
        inventoryPanel.SetActive(true);

        // When inventory is active, we want the game to pause
        Time.timeScale = 0f;

        // Bool variable to check if game is paused or not
        isPause = true;
    }

    void hideInventory(){
        // When hiding Inventory, Enable switch camera script
        camera.GetComponent<SwitchCamera>().enabled = true;
        // Enable 3rd person camera and Aim Cam
        ThirdPersonCam.SetActive(true);
        AimCam.SetActive(true);

        // Set inventory panel to be false
        inventoryPanel.SetActive(false);

        // When inventory is not active, we want the game to continue
        Time.timeScale = 1f;

        // Bool variable to check if game is paused or not
        isPause = false;
    }
}
    