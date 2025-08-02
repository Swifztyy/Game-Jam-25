using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiveUIController : MonoBehaviour
{
    public RectTransform uiPanel;
    public CanvasGroup canvasGroup;

    private Vector2 startPos;
    private Vector2 endPos;

    private float screenHeight;

    public Action onRiseComplete;

    public AudioSource audioSource;
    public AudioClip splashSound;

    private void Start()
    {
        float screenHeight = Screen.height;

        startPos = new Vector2(uiPanel.anchoredPosition.x, -Screen.height);
        endPos = new Vector2(uiPanel.anchoredPosition.x, Screen.height);

        uiPanel.anchoredPosition = startPos;
        uiPanel.gameObject.SetActive(false);

        if (uiPanel != null)
        {
            uiPanel.anchoredPosition = startPos;
            uiPanel.gameObject.SetActive(false);
        }
        else
        {
            Debug.LogError("UIPanel is not assigned in DiveUIController.");
        }
    }

    public void PlayRiseUI(float midRiseY, Action onMidRiseComplete)
    {
        if (uiPanel == null) return;

        screenHeight = Screen.height;
        startPos = new Vector2(uiPanel.anchoredPosition.x, -screenHeight);
        uiPanel.anchoredPosition = startPos;

        if (canvasGroup != null)
        {
            canvasGroup.alpha = 0f;
        }

        uiPanel.gameObject.SetActive(true);
        //uiPanel.anchoredPosition = startPos;

        //onRiseComplete = onMidRiseComplete;

        StartCoroutine(RiseSequence(midRiseY, onMidRiseComplete));
    }

    //public void HideRiseUI()
    //{
    //    if (uiPanel != null)
    //    {
    //        uiPanel.gameObject.SetActive(false);
    //    }
    //}

    private IEnumerator RiseSequence(float midRiseY, Action onMidRiseComplete)
    {
        float totalDuration = 4.5f;
        float midDuration = totalDuration * 0.6f;
        float endDuration = totalDuration - midDuration;

        yield return AnimatePosition(startPos.y, midRiseY, midDuration, fadeOut: false, fadeIn: true);

        onMidRiseComplete?.Invoke();

        float endY = screenHeight + 400f;
        yield return AnimatePosition(midRiseY, endY, endDuration, fadeOut: true);

        uiPanel.gameObject.SetActive(false);
    }

    private IEnumerator AnimatePosition(float fromY, float toY, float duration, bool fadeOut = false, bool fadeIn = false)
    {
        float elapsed = 0f;
        Vector2 anchoredPos = uiPanel.anchoredPosition;

        while (elapsed < duration)
        {
            elapsed += Time.unscaledDeltaTime;
            float t = Mathf.Clamp01(elapsed / duration);
            float newY = Mathf.Lerp(fromY, toY, t);
            uiPanel.anchoredPosition = new Vector2(anchoredPos.x, newY);

            if (canvasGroup != null)
            {
                if (fadeIn)
                    canvasGroup.alpha = t; 
                else if (fadeOut)
                    canvasGroup.alpha = 1f - t; 
            }

            yield return null;
        }
    }

    public void PlaySplashSound()
    {
        if (audioSource != null && splashSound != null)
        {
            audioSource.pitch = UnityEngine.Random.Range(0.7f, 1.3f);
            audioSource.PlayOneShot(splashSound);
            audioSource.pitch = 1f;
        }
    }
}
