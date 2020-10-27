using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockController : MonoBehaviour
{
    private void Start()
    {
        FindObjectOfType<LevelManager>().CountNumOfBlocks();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        FindObjectOfType<LevelManager>().CountBrokenBlock();
        Destroy(gameObject);
    }
}
