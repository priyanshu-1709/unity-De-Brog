using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGrabber : MonoBehaviour
{
    [SerializeField] protected float throwForce = 500f;

    public void GrabObject(GameObject objectToMove)
    {
        Rigidbody rigidbody = objectToMove.GetComponent<Rigidbody>();
        rigidbody.isKinematic = true;
        rigidbody.useGravity = false;
        rigidbody.linearVelocity = Vector3.zero;
        rigidbody.angularVelocity = Vector3.zero;
        
        objectToMove.transform.position = this.transform.position;
        objectToMove.transform.rotation = this.transform.rotation;
        objectToMove.transform.parent = this.transform;
    }

    public void DropObject(GameObject objectToMove)
    {
        Rigidbody rigidbody = objectToMove.GetComponent<Rigidbody>();
        rigidbody.isKinematic = false;
        rigidbody.useGravity = true;
        objectToMove.transform.parent = null;
    }

    public void ThrowObject(GameObject objectToThrow)
    {
        DropObject(objectToThrow);
        Rigidbody rigidbody = objectToThrow.GetComponent<Rigidbody>();
        rigidbody.AddForce(this.transform.forward * throwForce);
    }

    public void SetThrowForce(float newThrowForce)
    {
        throwForce = newThrowForce;
    }
}
