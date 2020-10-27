using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockController : MonoBehaviour
{
    public enum BlockType {Breakable, NotBreakable};

    [SerializeField] private GameObject BlockSparkleVFX;

    private void Start()
    {
        if (tag == "Breakable Block")
        {
            FindObjectOfType<LevelManager>().CountNumOfBlocks();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (tag == "Breakable Block")
        {
            FindObjectOfType<LevelManager>().CountBrokenBlock();
            TriggerSparklesVFX();
            Destroy(gameObject);
        }
    }

    private void TriggerSparklesVFX()
    {
        GameObject sparkles = Instantiate(BlockSparkleVFX, transform.position, transform.rotation);
        Destroy(sparkles, 1f);
    }
}
