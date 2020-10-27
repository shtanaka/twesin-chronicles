using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private int numOfBlocks = 0;
    [SerializeField] private int numOfBrokenBlocks;
    [SerializeField] private bool isLevelToEndTheGame = false;

    void Update()
    {
        if (numOfBrokenBlocks == numOfBlocks)
        {
            if (isLevelToEndTheGame)
            {
                LoadYouWin();
            } 
            else
            {
                LoadNextLevel();
            }
        }
    }

    public void CountNumOfBlocks()
    {
        numOfBlocks++;
    }

    public void CountBrokenBlock()
    {
        GameStatusController.instance.IncreaseScore(10);
        numOfBrokenBlocks++;
    }

    public void LoadGameOver()
    {
        SceneManager.LoadScene("Game Over");
    }

    public void LoadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LoadYouWin()
    {
        SceneManager.LoadScene("You Win");
    }
}
