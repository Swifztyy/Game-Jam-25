using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    [Header("Health Settings")]
    [SerializeField] private int playerHealth = 100;
    [SerializeField] private int damageValue = 20;
    public bool isPlayerDead = false;

    public HashSet<GameObject> damagedObstacles = new HashSet<GameObject>();
    //list of obstacles that have been hit by the player so you cant take multiple instances of damage from the same obstacle

    [SerializeField] private PlayerController playerController;
    [SerializeField] private GameObject youDiedScreen;
    public void TakeDamage(GameObject obstacle)
    {
        if (!damagedObstacles.Contains(obstacle))
        {
            playerHealth -= damageValue;
            damagedObstacles.Add(obstacle);
            
            if (playerHealth <= 0)
            {
                Die();
            }
        }
    }
    public void ClearDamagedObstacles()
    {
        damagedObstacles.Clear();
        // this is to reset the damaged obstacles hash whenever it is called in a level transition
    }

    private void Die()
    {
        Debug.Log("player is dead");
        playerController.enabled = false;
        isPlayerDead = true;
        youDiedScreen.SetActive(true);
    }

    public void RestoreHealth()
    {
        playerHealth = 100;
    }
}
