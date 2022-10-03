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
    public Transform finish;

    [Header("Stack Information Variables")]
    public StackController currentStack;
    public StackController nextStack;

    [Header("Knifes")]
    [SerializeField] Knife leftKnife;
    [SerializeField] Knife rightKnife;
    private float knifeForwardLocalPos;

    [Header("Skill Check")]
    public int skillCheckCount;

    private void Start()
    {
        isPlayable = true;
        knifeForwardLocalPos = CalcKnifeForwardLocalPosValue();
    }

    public void CurrentStackAdjuster(StackController stack)
    {
        currentStack = stack;
    }

    public IEnumerator KnivesActivator()
    {
        SkillCheckActivator(true);

        yield return new WaitForSeconds(.02f);

        leftKnife.cutCollider.enabled = true;
        rightKnife.cutCollider.enabled = true;
    }

    public void KnivesDeActivator()
    {
        SkillCheckActivator(false);
        leftKnife.cutCollider.enabled = false;
        rightKnife.cutCollider.enabled = false;
    }

    public void SkillCheckActivator(bool isActive)
    {
        leftKnife.skillCollider.enabled = isActive;
        rightKnife.skillCollider.enabled = isActive;
    }

    private float CalcKnifeForwardLocalPosValue()
    {
        return StackCreator.Instance.GetStackForwardSize() * .5f;
    }

    public void KnifeLocalPosAdjuster(float xLocalPos)
    {
        Vector3 rightDesiredPos = new Vector3(xLocalPos * .5f, 0f, knifeForwardLocalPos);
        Vector3 leftDesiredPos = new Vector3(-xLocalPos * .5f, 0f, knifeForwardLocalPos);
        
        leftKnife.transform.localPosition = leftDesiredPos;
        rightKnife.transform.localPosition = rightDesiredPos;
    }

    private float CalculateDistance()
    {
        return (finish.position - PlayerController.Instance.transform.position).sqrMagnitude;
    }

    public bool IsPlayerGoingFinish()
    {
        if (CalculateDistance() < 31f)
            return true;
        return false;
    }
}