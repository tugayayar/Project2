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
    Sequence stackTween;
    private Guid stackTweenId;
    private float animStartPoint;
    private float animEndPoint;

    private void OnEnable()
    {
        AnimationPointAdjuster();
        StackAnimation();
    }

    private void AnimationPointAdjuster()
    {
        if (transform.position.x < 0)
        {
            animStartPoint = stackPositionLimit;
            animEndPoint = -stackPositionLimit;
        }
        else
        {
            animStartPoint = -stackPositionLimit;
            animEndPoint = stackPositionLimit;
        }
    }

    private void StackAnimation()
    {
        if (stackTween == null)
        {
            stackTween = DOTween.Sequence();

            stackTween.Append(transform.DOMoveX(animStartPoint, animTime).SetEase(Ease.InQuart))
                .Append(transform.DOMoveX(animEndPoint, animTime).SetEase(Ease.InQuart))
                .SetLoops(-1);

            stackTweenId = Guid.NewGuid();
            stackTween.id = stackTweenId;
        }
    }

    public void KillStackAnimation()
    {
        stackTween.Kill();
        stackTween = null;
    }
}
