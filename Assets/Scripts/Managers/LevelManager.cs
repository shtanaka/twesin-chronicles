using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private int numOfBlocks = 0;
    [SerializeField] private int numOfBalls = 1;
    [SerializeField] private int numOfBrokenBlocks;
    [SerializeField] private int multiplyBallFactor = 4;
    [SerializeField] private int numOfMultiplyBallUsed = 0;
    [SerializeField] private int possibleNumOfMultiplyBall = 1;
    [SerializeField] private bool isLevelToEndTheGame = false;

    public int MultiplyBallFactor { get { return multiplyBallFactor; } }

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

    public void ApplyMultiplyBalls()
    {
        numOfMultiplyBallUsed += 1;
        numOfBalls += multiplyBallFactor;
    }

    public bool CanMultiplyBall()
    {
        return numOfMultiplyBallUsed < possibleNumOfMultiplyBall;
    }

    public void RemoveBallFromGame()
    {
        numOfBalls--;
    }

    public int GetNumOfBalls()
    {
        return numOfBalls;
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
