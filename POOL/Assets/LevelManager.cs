using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public int currentLevel = -1;
    public List<Transform> levelPositions = new List<Transform>();
    public Transform playerPos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pool"))
        {
            NextLevel();
        }
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
