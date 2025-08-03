using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollController : MonoBehaviour
{
    [SerializeField] private Rigidbody[] ragdollBodies;
    [SerializeField] private Collider[] ragdollColliders;
    [SerializeField] private Rigidbody mainBody;
    [SerializeField] private Collider mainCollider;

    private Dictionary<Transform, Pose> initialPoses = new Dictionary<Transform, Pose>();

    private bool isRagdollActive = false;

    void Start()
    {
        CacheInitialPoses();
        DisableRagdoll();
    }

    void CacheInitialPoses()
    {
        foreach (var body in ragdollBodies)
        {
            Transform t = body.transform;
            initialPoses[t] = new Pose(t.localPosition, t.localRotation);
        }
    }

    public void EnableRagdoll()
    {
        if (isRagdollActive) return;
        isRagdollActive = true;

        foreach (Rigidbody body in ragdollBodies)
        {
            body.isKinematic = false;
            body.detectCollisions = true;
        }

        foreach (Collider col in ragdollColliders)
            col.enabled = true;

        if (mainBody != null)
            mainBody.isKinematic = true;

        if (mainCollider != null)
            mainCollider.enabled = false;
    }

    public void DisableRagdoll()
    {
        if (!isRagdollActive) return;
        isRagdollActive = false;

        foreach (Rigidbody body in ragdollBodies)
        {
            body.isKinematic = true;
            body.velocity = Vector3.zero;
            body.angularVelocity = Vector3.zero;
        }

        foreach (Collider col in ragdollColliders)
            col.enabled = false;

        if (mainBody != null)
            mainBody.isKinematic = false;

        if (mainCollider != null)
            mainCollider.enabled = true;

        // Reset transforms
        foreach (var pair in initialPoses)
        {
            pair.Key.localPosition = pair.Value.position;
            pair.Key.localRotation = pair.Value.rotation;
        }
    }
}

/*
 * 
 * public void DisableRagdoll()
    {
        isRagdollActive = false;

        foreach (Rigidbody body in ragdollBodies)
            body.isKinematic = true;

        foreach (Collider col in ragdollColliders)
            col.enabled = false;

        if (mainBody != null)
            mainBody.isKinematic = false;

        if (mainCollider != null)
            mainCollider.enabled = true;
    }
 * 
*/