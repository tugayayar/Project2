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

    private void OnEnable()
    {
        stackPositionLimit = transform.position.x;
        StackAnimation();
    }

    private float AnimEndPoint()
    {
        stackPositionLimit = stackPositionLimit * -1f;
        return stackPositionLimit;
    }

    private void StackAnimation()
    {
        KillStackAnimation();

        if (stackTween == null)
        {
            stackTween = DOTween.Sequence();

            stackTween.Append(transform.DOMoveX(AnimEndPoint(), animTime).SetEase(Ease.OutSine))
            .SetLoops(-1, LoopType.Yoyo);

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