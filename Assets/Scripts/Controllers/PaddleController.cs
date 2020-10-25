using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleController : MonoBehaviour
{
    private float minX;
    private float maxX;
    private void Start()
    {
        Vector2 stageDimensions = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        float objectXRadius = GetComponent<Renderer>().bounds.size.x / 2;
        minX = stageDimensions.x * -1 + objectXRadius;
        maxX = stageDimensions.x - objectXRadius;
    }

    private void Update()
    {
        var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var newXPosition = Mathf.Clamp(mousePosition.x, minX, maxX);
        Vector2 paddlePosition = new Vector2(newXPosition, transform.position.y);
        transform.position = paddlePosition;
    }
}
