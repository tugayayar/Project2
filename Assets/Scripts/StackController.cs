using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class StackController : MonoBehaviour
{
    [Header("Stack Animation Variables")]
    [SerializeField] private float animTime = 2f;
    [SerializeField] private float stackPositionLimit = 3.2f;
    Tweener stackTween;//Sequence stackTween;
    private Guid stackTweenId;
    //private float animStartPoint;
    //private float animEndPoint;

    private void OnEnable()
    {
        //AnimationPointAdjuster();
        StackAnimation();
    }

    //private void AnimationPointAdjuster()
    //{
    //    Debug.Log("StackPos: " + transform.position.x);
    //    if (transform.position.x < 0)
    //    {
    //        animStartPoint = stackPositionLimit;
    //        animEndPoint = -stackPositionLimit;
    //    }
    //    else
    //    {
    //        animStartPoint = -stackPositionLimit;
    //        animEndPoint = stackPositionLimit;
    //    }

    //    Debug.Log("animStartPos: " + animStartPoint + " <=> animEndPos: " + animEndPoint);
    //}

    private float AnimEndPoint()
    {
        Debug.Log("Hesapladým kitapladým: " + (-1 * transform.position.x));
        return (-1 * transform.position.x);//(-1 * transform.position.x);
    }

    private void StackAnimation()
    {
        if (stackTween == null)
        {
            //stackTween = DOTween.Sequence();

            //Ease.InQuart
            //stackTween.Append(transform.DOMoveX(AnimEndPoint(), animTime).SetEase(Ease.OutSine).OnComplete(() => { Debug.Log("Bitti!"); }))
            //.SetLoops(-1);

            stackTween = transform.DOMoveX(AnimEndPoint(), animTime).SetEase(Ease.Linear);
            //stackTween.OnComplete(() => {/* print("deðiþti "); stackTween.ChangeEndValue(AnimEndPoint());*/ stackTween.Restart(); });
            stackTween.SetLoops(-1, LoopType.Yoyo);//.SetLoops((int)LoopType.Restart);

            stackTweenId = Guid.NewGuid();
            stackTween.id = stackTweenId;
        }
    }

    public void KillStackAnimation()
    {
        DOTween.Kill(stackTweenId);
        stackTween = null;
    }
}