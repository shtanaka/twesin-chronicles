using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverLogic : MonoBehaviour
{
    void Start()
    {
        Debug.Log(GameStatusController.instance.GetScore());
        GameStatusController.instance.DestroyGameStatusController();
    }
}
