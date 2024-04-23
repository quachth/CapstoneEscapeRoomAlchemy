using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldableObject : MonoBehaviour
{

    private Rigidbody objectRigidbody;
    private Transform heldObjectPosTransform;

    private void Awake() 
    {
        objectRigidbody = GetComponent<Rigidbody>();
    }

    // Pick up the rigidbody object and turn off gravity
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
