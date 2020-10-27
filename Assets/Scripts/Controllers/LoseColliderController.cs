using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseColliderController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Contains("ball"))
        {
            LevelManager levelManager = FindObjectOfType<LevelManager>();
            Destroy(collision.gameObject);
            levelManager.RemoveBallFromGame();
            if (levelManager.GetNumOfBalls() == 0)
            {
                levelManager.LoadGameOver();
            }
        }
    }
}
