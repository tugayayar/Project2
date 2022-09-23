using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackCreator : MonoBehaviour
{
    #region Singleton
    public static StackCreator Instance;

    private void Awake()
    {
        Instance = this;
    }
    #endregion

    [SerializeField] StackController stackPrefab;

    public void CreateNewStack(float newScaleSize)
    {
        StackAdjuster(newScaleSize);
    }

    StackController CreateStack()
    {
        return Instantiate(stackPrefab, Vector3.zero, Quaternion.identity);
    }

    void StackAdjuster(float scale)
    {
        GameManager.Instance.CurrentStackAdjuster();

        StackController newStack = CreateStack();
        GameManager.Instance.nextStack = newStack;
        newStack.transform.position = NewStackPosCalc();

        Vector3 desiredScale = new Vector3(scale, 0, newStack.transform.localScale.z);
        newStack.transform.localScale = desiredScale;

        newStack.gameObject.SetActive(true);
    }

    Vector3 NewStackPosCalc()
    {
        return (GameManager.Instance.nextStack.transform.position + (Vector3.forward * 3f)) + RandomSide();
    }

    Vector3 RandomSide()
    {
        int rndNum = Random.Range(0, 2) * 2 - 1;
        return Vector3.right * ((float)rndNum * 4f);
    }
}
