using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class StackController : MonoBehaviour
{
    [Header("Stack Animation Variables")]
    [SerializeField] private float animSpeed = 1f;
    Sequence stackTween;
    private Guid stackTweenId;

    private void OnEnable()
    {
        StackAnimation();
    }

    private void StackAnimation()
    {
        if (stackTween == null)
        {
            stackTween = DOTween.Sequence();

            stackTween.Append(transform.DOMoveX(3.2f, animSpeed).SetEase(Ease.InQuart))
                .Append(transform.DOMoveX(-3f, animSpeed).SetEase(Ease.InQuart))
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
