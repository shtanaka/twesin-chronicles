using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    [SerializeField] private PaddleController paddle1;
    [SerializeField] private float launchVelocityX = 2f;
    [SerializeField] private float launchVelocityY = 15f;
    [SerializeField] private bool isBallReleased = false;

    private Vector2 paddleToBallVector;

    void Start()
    {
        paddleToBallVector = transform.position - paddle1.transform.position;
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
    }

    void Update()
    {
        if (!isBallReleased)
        {  
            AttachBallToPaddle();
            LaunchOnMouseClick();
        }

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        GetComponent<AudioSource>().Play();    
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
            isBallReleased = true;
            var ballRigidbody = GetComponent<Rigidbody2D>();
            ballRigidbody.bodyType = RigidbodyType2D.Dynamic;
            ballRigidbody.velocity = new Vector2(launchVelocityX, launchVelocityY);
        }
    }
}
