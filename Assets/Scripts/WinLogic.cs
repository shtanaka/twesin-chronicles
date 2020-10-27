using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinLogic : MonoBehaviour
{
    void Start()
    {
        Debug.Log(GameStatusController.instance.GetScore());
        GameStatusController.instance.DestroyGameStatusController();
    }
}
