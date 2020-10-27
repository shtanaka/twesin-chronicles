using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    [SerializeField] private PaddleController paddle1;
    [SerializeField] private float launchVelocityX = 2f;
    [SerializeField] private float launchVelocityY = 15f;
    [SerializeField] private float randomBallFactor = 0.2f;
    [SerializeField] private bool isBallReleased = false;

    public bool IsBallReleased { set { isBallReleased = value; } }

    private Vector2 paddleToBallVector;

    void Start()
    {
        if (!isBallReleased)
        {
            paddleToBallVector = transform.position - paddle1.transform.position;
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        }
    }

    void Update()
    {
        if (!isBallReleased)
        {  
            AttachBallToPaddle();
            LaunchOnMouseClick();
        } else
        {
            LevelManager levelManager = FindObjectOfType<LevelManager>();
            if (Input.GetKeyDown(KeyCode.Space) && levelManager.CanMultiplyBall())
            {
                levelManager.ApplyMultiplyBalls();
                for (int i = 0; i < levelManager.MultiplyBallFactor; i++)
                {
                    CloneBall();
                }
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        GetComponent<AudioSource>().Play();
        var ballRigidbody = GetComponent<Rigidbody2D>();
        float xFactor = Random.Range(-randomBallFactor, +randomBallFactor);
        float yFactor = Random.Range(-randomBallFactor, +randomBallFactor);
        ballRigidbody.velocity = ballRigidbody.velocity + new Vector2(xFactor, yFactor);
    }
    
    void CloneBall()
    {
        var newBall = Instantiate(this, transform.position, transform.rotation);
        newBall.IsBallReleased = true;
        newBall.LaunchBall(Random.Range(-5f, +5f), -15f);
    }

    private void AttachBallToPaddle()
    {
        Vector2 paddlePos = new Vector2(paddle1.transform.position.x, paddle1.transform.position.y);
        transform.position = paddlePos + paddleToBallVector;
    }

    private void LaunchOnMouseClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            LaunchBall(launchVelocityX, launchVelocityY);
        }
    }

    public void LaunchBall(float velocityX, float velocityY)
    {
        isBallReleased = true;
        var ballRigidbody = GetComponent<Rigidbody2D>();
        ballRigidbody.bodyType = RigidbodyType2D.Dynamic;
        ballRigidbody.velocity = new Vector2(velocityX, velocityY);
    }
}
