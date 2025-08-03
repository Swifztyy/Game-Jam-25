using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            RagdollController ragdoll = other.GetComponentInChildren<RagdollController>();
            if (ragdoll != null)
            {
                ragdoll.EnableRagdoll();
            }
        }
    }
}
