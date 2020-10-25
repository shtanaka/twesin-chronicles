using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockController : MonoBehaviour
{

    private void OnCollisionEnter2D(Collision2D collision)
    {
        LevelManager.instance.CountBreakableBlocks();
        Destroy(gameObject);
    }
}
