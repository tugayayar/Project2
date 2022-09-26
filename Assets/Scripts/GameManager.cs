using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Singleton
    public static GameManager Instance;

    private void Awake()
    {
        Instance = this;
    }
    #endregion

    [Header("Player Variables")]
    [HideInInspector] public bool isPlayable;

    [Header("Stack Information Variables")]
    public StackController currentStack;
    public StackController nextStack;
    
    [Header("Knifes")]
    [SerializeField] Knife leftKnife;
    [SerializeField] Knife rightKnife;
    private float knifeForwardLocalPos;

    private void Start()
    {
        isPlayable = true;
        knifeForwardLocalPos = CalcKnifeForwardLocalPosValue();
    }

    public void CurrentStackAdjuster(StackController stack)
    {
        currentStack = stack;
    }

    public void KnivesActivator(bool isActive)
    {
        leftKnife.enabled = isActive; //leftKnife.gameObject.SetActive(isActive);
        rightKnife.enabled = isActive; //rightKnife.gameObject.SetActive(isActive);
    }

    private float CalcKnifeForwardLocalPosValue()
    {
        return StackCreator.Instance.GetStackForwardSize() * .5f;
    }

    public void KnifeLocalPosAdjuster(float xLocalPos)
    {
        Vector3 rightDesiredPos = new Vector3(xLocalPos, 0f, knifeForwardLocalPos);
        Vector3 leftDesiredPos = new Vector3(-xLocalPos, 0f, knifeForwardLocalPos);
        
        leftKnife.transform.localPosition = leftDesiredPos;
        rightKnife.transform.localPosition = rightDesiredPos;
    }
}