using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupItem : MonoBehaviour
{
    // Adding Pickup System to the game

    // Here we have variables like item price, item tag,item radius etc
    // As we will have a certain radius and when player is in that radius it can pickup the item
    [Header("Item Info")]
    public int itemPrice;
    public int itemRadius;
    // Using this we can decide if player is in shortgun radius or handgun radius
    public string itemTag;
    // Reference to specific rifle
    private GameObject itemToPick;


    [Header("Player Info")]
    // Reference to player script, whenever player pick's up an item, we will deduct money from player's account according to item price
    public Player player;

    // Reference to Inventory Script, to change values of boolean variables like isWeaponPicked to true or false
    public Inventory inventory;

    // Whenever the game starts, we want to find all of the item tags
    // And assign tags to itemTag variable
    private void Start(){
        // Using itemTag we will get to know which weapon is being picked up
        itemToPick = GameObject.FindWithTag(itemTag);
    }

    private void Update(){
        // Check if player is inside the item radius or not
        // transform.position: Rifle Position
        // player.transform.position: player position
        if(Vector3.Distance(transform.position, player.transform.position) < itemRadius){
            // If player is within item radius, allow player to pickup rifle using f button
            if(Input.GetKeyDown("f")){
                // Check if player has enough money to buy rifle
                if(itemPrice > player.playerMoney){
                    // if player doesn't have enough money
                    Debug.Log("You are broke");
                    // Show UI
                }else{
                    // If player has enough money, allow player to pickup item
                    if(itemTag == "HandGunPickup"){
                        // Deduct money from player account
                        player.playerMoney -= itemPrice;
                        //Activate the Hand Gun UI in the inventory
                        inventory.Weapon1.SetActive(true);
                        // Change the boolean value of isWeapon1PickedUp to true
                        inventory.isWeapon1Picked = true;
                        // Printing weapon name on console
                        Debug.Log(itemTag);
                    }else if(itemTag == "ShortGunPickup"){
                        // Deduct money from player account
                        player.playerMoney -= itemPrice;
                        //Activate the Short Gun UI in the inventory
                        inventory.Weapon2.SetActive(true);
                        // Change the boolean value of isWeapon2PickedUp to true
                        inventory.isWeapon2Picked = true;
                        // Printing weapon name on console
                        Debug.Log(itemTag);
                    }else if(itemTag == "UziPickup"){
                        // Deduct money from player account
                        player.playerMoney -= itemPrice;
                        //Activate the UZI UI in the inventory
                        inventory.Weapon3.SetActive(true);
                        // Change the boolean value of isWeapon3PickedUp to true
                        inventory.isWeapon3Picked = true;
                        // Printing weapon name on console
                        Debug.Log(itemTag);
                    }else if(itemTag == "BazookaPickup"){
                        // Deduct money from player account
                        player.playerMoney -= itemPrice;
                        //Activate the Hand Bazooka UI in the inventory
                        inventory.Weapon4.SetActive(true);
                        // Change the boolean value of isWeapon4PickedUp to true
                        inventory.isWeapon4Picked = true;
                        // Printing weapon name on console
                        Debug.Log(itemTag);
                    }
                    // Whatever rifle player pick's up, remove that rifle from the environment
                    itemToPick.SetActive(false);
                }
            }
        }
    }
}
