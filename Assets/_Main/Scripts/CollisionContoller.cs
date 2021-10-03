using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionContoller : MonoBehaviour
{
    public float pushForce = 2.0f;

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody rigidbody = hit.collider.attachedRigidbody;

        if (rigidbody == null || rigidbody.isKinematic)
            return;

        if (hit.moveDirection.y < -0.3)
            return;

        Vector3 pushDirection = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);

        rigidbody.velocity = pushDirection * pushForce;
    }
}
