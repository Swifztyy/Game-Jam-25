using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public int currentLevel = -1;
    public List<Transform> levelPositions = new List<Transform>();
    public Transform playerPos;

    public DiveUIController uiController;

    private bool isTransitioning = false;

    public AudioSource audioSource;
    public AudioClip splashSound;

    public GameObject splashEffectPrefab;

    [SerializeField] private HealthManager healthManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pool") && !isTransitioning)
        {
            if (uiController != null)
            {
                uiController.PlaySplashSound();
            }

            if (splashEffectPrefab != null)
            {
                Instantiate(splashEffectPrefab, playerPos.position, Quaternion.identity);
            }

            StartCoroutine(HandleLevelTransition());
        }
    }

    private IEnumerator HandleLevelTransition()
    {
        isTransitioning = true;

        Time.timeScale = 0.2f;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;

        Rigidbody rb = playerPos.GetComponent<Rigidbody>();
        if (rb != null)
            rb.velocity = Vector3.down * 2f;

        bool teleported = false;

        float midRiseY = Screen.height * 0.6f; 

        uiController.PlayRiseUI(midRiseY, () => teleported = true);

        // Wait for teleport callback
        yield return new WaitUntil(() => teleported);

        // Teleport player
        currentLevel++;
        if (currentLevel < levelPositions.Count && healthManager.isPlayerDead == false)
        {
            playerPos.position = levelPositions[currentLevel].position;
            healthManager.ClearDamagedObstacles();
            healthManager.RestoreHealth();
        }
        else
        {
            Debug.Log("No more levels or player is dead.");
        }

        //RagdollController ragdoll = playerPos.GetComponentInChildren<RagdollController>();
        //if (ragdoll != null)
        //    ragdoll.DisableRagdoll();

        // Reset time
        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02f;

        isTransitioning = false;
    }

    void NextLevel()
    {
        currentLevel++;

        if (currentLevel <= levelPositions.Count)
        {
            playerPos.position = levelPositions[currentLevel].position;
        }
        else
        {
            Debug.Log("currentLevel count exceeds level count");
        }
    }
}
