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

    private void Start()
    {
        isPlayable = true;
    }

    public void CurrentStackAdjuster()
    {
        currentStack = nextStack;
    }

    public void KnivesActivator(bool isActive)
    {
        leftKnife.gameObject.SetActive(isActive);
        rightKnife.gameObject.SetActive(isActive);
    }
}