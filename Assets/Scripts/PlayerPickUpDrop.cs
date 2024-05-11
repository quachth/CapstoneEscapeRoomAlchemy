/*  Project: Capstone 3D Escape Room Challenge - Alchemy Room
    Author: Theresa Quach
    Description: PlayerPickUpDrop contains logic that allows the player to pick up objects using the left mouse button. This script is placed on the player controller's main camera and
                    has the layer of objects capable of being picked up specified.
    Citations:  Code was written following the tutorial by CodeMonkey on Youtube (https://www.youtube.com/watch?v=2IhzPTS4av4&list=PLvw5MDqEo9YLISdYRQmGy9fYN6EtjTtZG&index=5).
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpDropItem : MonoBehaviour
{

    [SerializeField] private Transform playerCameraTransform;
    [SerializeField] private LayerMask pickUpLayerMask;
    [SerializeField] private Transform heldObjectPosTransform;

    private HoldableObject holdableObject; 


    private void Update() {
        // When user hits left-click
        if (Input.GetKeyDown(KeyCode.Mouse0)) 
        {
            // If player is not currently holding an object, try to pick up an object
            if (holdableObject == null) {
                float pickupDistance = 2.5f;    // Pickup range from camera
                // If the raycast from the player camera in the specified direction with the specified length hits a collider, and the collider belongs to the specified layer mask
                if (Physics.Raycast(playerCameraTransform.position, playerCameraTransform.forward, out RaycastHit raycastHit, pickupDistance, pickUpLayerMask))     
                {
                    // if the object hit by the raycast is a HoldableObject (has HoldableObject script attached), pick it up
                    if (raycastHit.transform.TryGetComponent<HoldableObject>(out holdableObject)) 
                    {
                        holdableObject.PickUp(heldObjectPosTransform);
                    }
                }
            } else {
                // An object is currently being held, drop it
                holdableObject.Drop();
                holdableObject = null;
            }
        }
    }
}
