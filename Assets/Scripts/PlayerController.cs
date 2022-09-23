using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private GameManager gameManagerSC;

    private void Start()
    {
        gameManagerSC = GameManager.Instance;
    }

    void Update()
    {
        CheckPlayerTouched();
    }

    void CheckPlayerTouched()
    {
        if (Input.GetMouseButtonDown(0) && gameManagerSC.isPlayable)
        {
            gameManagerSC.isPlayable = false;
            gameManagerSC.nextStack.KillStackAnimation();
        }
    }
}
