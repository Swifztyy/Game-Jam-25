using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartScript : MonoBehaviour
{
    [SerializeField] private LevelManager levelManager;
    [SerializeField] private HealthManager healthManager;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private GameObject diedScreen;
    public void RestartGame()
    {
        Time.timeScale = 1.0f;
        if (levelManager.currentLevel < 0)
        {
            SceneManager.LoadScene("ExperimentDylan");
        }
        else
        {
            diedScreen.SetActive(false);
            healthManager.RestoreHealth();
            healthManager.isPlayerDead = false;
            playerController.enabled = true;
            healthManager.ClearDamagedObstacles();
            levelManager.playerPos.position = levelManager.levelPositions[levelManager.currentLevel].position;
        }
    }
}
