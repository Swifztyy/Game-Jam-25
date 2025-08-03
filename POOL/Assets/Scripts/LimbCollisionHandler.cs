using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimbCollisionHandler : MonoBehaviour
{
    private HealthManager healthManager;
    // Start is called before the first frame update
    void Awake()
    {
        healthManager = GetComponentInParent<HealthManager>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Obstacle"))
        {
            healthManager?.TakeDamage(collision.collider.gameObject);
        }
        else if (collision.collider.CompareTag("DeathPlatform"))
        {
            healthManager?.Die();
        }
    }
}
