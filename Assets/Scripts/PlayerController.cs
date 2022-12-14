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
    public MovePointDisplacer movePointObjSC;
    private GameManager gameManagerSC;
    public SkillCheck skillCheckSC;
    [SerializeField] private Rigidbody playerRB;
    [SerializeField] Animator anim;
    
    [Header("Player Variables")]
    [SerializeField] private float nextStackMovingTimer = 2f;

    [Header("Animations")]
    public Animation animation;
    public enum Animation
    {
        Run,
        Dance
    }
    

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
            skillCheckSC.knifeCutCounter = 0;
            gameManagerSC.isPlayable = false;
            gameManagerSC.nextStack.KillStackAnimation();
            StartCoroutine(gameManagerSC.KnivesActivator());
            StartCoroutine(skillCheckSC.CheckIsKnivesCutted());
        }
    }

    private Vector3 GetNextPlatformPosition()
    {
        //for move point new positon
        movePointObjSC.enabled = true;

        return movePointObjSC.transform.position + (Vector3.up * .5f);
    }

    public void GoNextPlatform(bool finish)
    {
        Vector3 desiredPos = new Vector3();
        if (finish)
        {
            desiredPos = GameManager.Instance.finish.position;

        }
        else desiredPos = GetNextPlatformPosition();

        transform.DOMove(desiredPos, nextStackMovingTimer)
            .SetEase(Ease.Linear)
            .OnComplete(()=> 
            { 
                gameManagerSC.isPlayable = true;
                movePointObjSC.enabled = false;
                if (finish)
                {
                    AnimatonChanger(Animation.Dance);
                    UIManager.Instance.SuccessText(true);
                    LevelManager.Instance.SaveLevelNumber();
                }
            });
    }

    public void Fail()
    {
        float desiredZPos = transform.position.z + 2f;
        transform.DOMoveZ(desiredZPos, nextStackMovingTimer)
        .SetEase(Ease.Linear)
        .OnComplete(() =>
        {
            playerRB.constraints = RigidbodyConstraints.None;
            playerRB.isKinematic = false;

            UIManager.Instance.FailText(true);
            StartCoroutine(LevelManager.Instance.NextLevel()); //ayn? leveli bir daha y?kleyecek
        });
    }

    public void AnimatonChanger(Animation changedAnim)
    {
        switch (changedAnim)
        {
            case Animation.Run:
                anim.SetTrigger("Run");
                break;
            case Animation.Dance:
                anim.SetTrigger("Dance");
                break;
        }
    }
}
