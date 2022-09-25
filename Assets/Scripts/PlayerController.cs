using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Singleton
    public static PlayerController Instance;

    private void Awake()
    {
        Instance = this;
    }
    #endregion

    [Header("Needed Components")]
    private GameManager gameManagerSC;

    [Header("Player Variables")]
    [SerializeField] private float nextStackMovingTimer = 2f;

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
            gameManagerSC.KnivesActivator(true);
        }
    }

    public void GoNextPlatform()
    {
        transform.DOMove(gameManagerSC.nextStack.transform.position + (Vector3.up * .5f), nextStackMovingTimer)
            .SetEase(Ease.Linear)
            .OnComplete(()=> { gameManagerSC.isPlayable = true; });
    }
}
