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
}
