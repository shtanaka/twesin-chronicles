using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameStatusController : MonoBehaviour
{
    [SerializeField] private int score = 0;
    [SerializeField] private TextMeshProUGUI scoreText;
    [Range(0.5f, 4f)][SerializeField] private float timeScale = 1f;
    
    public static GameStatusController instance = null;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance.gameObject);
        }
        else if (instance != this)
        {
            DestroyGameStatusController();
        }
    }

    void Update()
    {
        Time.timeScale = timeScale;
        scoreText.SetText(score.ToString());
    }

    public void IncreaseScore(int increaseValue)
    {
        score += increaseValue;
    }

    public void DecreaseScore(int decreaseValue)
    {
        score -= decreaseValue;
    }

    public void DestroyGameStatusController()
    {
        gameObject.SetActive(false);
        Destroy(gameObject);
    }

    public int GetScore()
    {
        return score;
    }
}
