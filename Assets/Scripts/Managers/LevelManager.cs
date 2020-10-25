using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] int brokenBlocksCount = 0;
    [SerializeField] private TextMeshProUGUI brokenBlocksCounter;
    public static LevelManager instance = null;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        brokenBlocksCounter.SetText(brokenBlocksCount.ToString());  
    }

    public void CountBreakableBlocks()
    {
        brokenBlocksCount++;
        brokenBlocksCounter.SetText(brokenBlocksCount.ToString());
    }
}
