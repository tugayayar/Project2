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
    [SerializeField] private MovePointDisplacer movePointObjSC;
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

    private Vector3 GetNextPlatformPosition()
    {
        //for move point new positon
        movePointObjSC.enabled = true;

        return movePointObjSC.transform.position + (Vector3.up * .5f);
    }

    public void GoNextPlatform()
    {
        //MovePoint diye bir obje olu�tur
        //MovePoint'i, Player'�n y�r�yece�i platforma parent olarak ayarla
        //Platformun Box Collider'�n�n, Center.x de�erini, MovePoint'in LocalPos.x'ine ata
        //Sonra Player'�, MovePoint'in World Position'�n�na yolla

        

        transform.DOMove(GetNextPlatformPosition(), nextStackMovingTimer)
            .SetEase(Ease.Linear)
            .OnComplete(()=> 
            { 
                gameManagerSC.isPlayable = true;
                movePointObjSC.enabled = false;
            });
    }
}
