using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseColliderController : MonoBehaviour
{
    [SerializeField] BallController ball;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetInstanceID() == ball.gameObject.GetInstanceID())
        {
            SceneManager.LoadScene("Game Over");
        }
    }
}
