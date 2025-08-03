using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    [Header("Health Settings")]
    [SerializeField] private int playerHealth = 100;
    [SerializeField] private int damageValue = 20;
    public bool isPlayerDead = false;

    public AudioSource audioSource;
    public AudioClip[] hurtSounds;

    public HashSet<GameObject> damagedObstacles = new HashSet<GameObject>();
    //list of obstacles that have been hit by the player so you cant take multiple instances of damage from the same obstacle

    [SerializeField] private PlayerController playerController;
    [SerializeField] private GameObject youDiedScreen;


    [Header("Damage Indicator")]
    public Image damageImage;
    public float flashDuration = 0.2f;
    public float maxAlpha = 0.3f;

    Coroutine flash;

    private void Start()
    {
        SetAlpha(0f);
    }

    public void TakeDamage(GameObject obstacle)
    {
        if (!damagedObstacles.Contains(obstacle))
        {
            playerHealth -= damageValue;
            damagedObstacles.Add(obstacle);

            if (hurtSounds.Length > 0 && audioSource != null)
            {
                int index = UnityEngine.Random.Range(0, hurtSounds.Length);
                audioSource.PlayOneShot(hurtSounds[index]);
            }

            if (flash != null) StopCoroutine(flash);
            flash = StartCoroutine(Flash());

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
    IEnumerator Flash()
    {
        float t = 0f;
        while (t < flashDuration)
        {
            t += Time.deltaTime;
            float a = Mathf.Lerp(maxAlpha, 0, t / flashDuration);
            var c = damageImage.color;
            c.a = a;
            damageImage.color = c;
            yield return null;
        }
    }
    void SetAlpha(float a)
    {
        var c = damageImage.color;
        c.a = a;
        damageImage.color = c;
    }
}
