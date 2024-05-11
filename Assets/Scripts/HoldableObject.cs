/*  Project: Capstone 3D Escape Room Challenge - Alchemy Room
    Author: Theresa Quach
    Description: HoldableObjects is responsible for controlling the physics, transformation and movement of the objects with the characteristic of being
                    able to be picked up and dropped. This script is put on all objects that the player will be able to pick up and drop. Objects must also be a part of 
                    the holdableObjects layer.
    Citations:  Code was written following the tutorial by CodeMonkey on Youtube (https://www.youtube.com/watch?v=2IhzPTS4av4&list=PLvw5MDqEo9YLISdYRQmGy9fYN6EtjTtZG&index=5).
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldableObject : MonoBehaviour
{
    public GameObject item;
    private Rigidbody objectRigidbody;
    private Transform heldObjectPosTransform;
    

    private void Awake() 
    {
        item = this.gameObject;
        objectRigidbody = GetComponent<Rigidbody>();
    }

    // Pick up the rigidbody object and turn off gravity so item stays up
    public void PickUp(Transform heldObjectPosTransform) 
    {
        this.heldObjectPosTransform = heldObjectPosTransform;
        objectRigidbody.useGravity = false;
    }

    // Drop the rigidbody object and turn back on gravity
    public void Drop() 
    {
        this.heldObjectPosTransform = null;
        objectRigidbody.useGravity = true;
    }

    // Object movement updated while being held by the player.
    private void FixedUpdate() 
    {
        if (heldObjectPosTransform != null) 
        {
            float lerpSpeed = 10f;
            Vector3 newPosition = Vector3.Lerp(transform.position, heldObjectPosTransform.position, Time.deltaTime * lerpSpeed);
            objectRigidbody.MovePosition(newPosition);
        }
    }

}
