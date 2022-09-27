using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EzySlice;

public class Knife : MonoBehaviour
{
    [Header("Cutting Stack Variables")]
    [SerializeField] LayerMask layer;
    Material cuttingObjMat;
    [HideInInspector] public GameObject cuttingObj;

    [Header("Knife Variables")]
    public KnifeType knifeType;
    public enum KnifeType
    {
        Left,
        Right
    }
    public BoxCollider cutCollider;

    [Header("Next Stack Components")]
    Transform currentStack;
    BoxCollider currentStackCol;

    private void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Platform"))
        {
            cuttingObj = other.gameObject;
            cuttingObjMat = other.GetComponent<MeshRenderer>().material;
            StartCutting();
            PlayerController.Instance.skillCheckSC.knifeCutCounter++;
        }
    }

    private float StackXSizeCalculator(float stackDefaultSize, float colliderXSize)
    {
        return colliderXSize * stackDefaultSize;
    }

    private void StartCutting()
    {
        switch (knifeType)
        {
            case KnifeType.Left:
                CutLeft();
                break;
            case KnifeType.Right:
                CutRight();
                break;
        }
    }

    SlicedHull Cut(GameObject obj, Material mat = null)
    {
        return obj.Slice(transform.position, transform.up, mat);
    }
    private void CutLeft()
    {
        SlicedHull cutted = Cut(cuttingObj, cuttingObjMat);

        GameObject cuttedObjUp = cutted.CreateUpperHull(cuttingObj, cuttingObjMat);
        cuttedObjUp.AddComponent<BoxCollider>().isTrigger = false;
        cuttedObjUp.AddComponent<Rigidbody>();
        StartCuttedObjectKillTimer(cuttedObjUp);
        cuttedObjUp.layer = LayerMask.NameToLayer("Platform");

        GameObject cuttedObjLow = cutted.CreateLowerHull(cuttingObj, cuttingObjMat);
        cuttedObjLow.AddComponent<BoxCollider>().isTrigger = false;
        StackController stackSC = cuttedObjLow.AddComponent<StackController>();
        stackSC.KillStackAnimation();
        Rigidbody rb = cuttedObjLow.AddComponent<Rigidbody>();
        rb.useGravity = false;
        rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePosition;
        cuttedObjLow.layer = LayerMask.NameToLayer("Default");
        GameManager.Instance.CurrentStackAdjuster(stackSC);

        AfterCutting();
    }

    private void CutRight()
    {
        SlicedHull cutted = Cut(cuttingObj, cuttingObjMat);

        GameObject cuttedObjUp = cutted.CreateUpperHull(cuttingObj, cuttingObjMat);
        cuttedObjUp.AddComponent<BoxCollider>().isTrigger = false;
        StackController stackSC = cuttedObjUp.AddComponent<StackController>();
        stackSC.KillStackAnimation();
        Rigidbody rb = cuttedObjUp.AddComponent<Rigidbody>();
        rb.useGravity = false;
        rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePosition;
        cuttedObjUp.layer = LayerMask.NameToLayer("Default");
        GameManager.Instance.CurrentStackAdjuster(stackSC);

        GameObject cuttedObjLow = cutted.CreateLowerHull(cuttingObj, cuttingObjMat);
        cuttedObjLow.AddComponent<BoxCollider>().isTrigger = false;
        cuttedObjLow.AddComponent<Rigidbody>();
        StartCuttedObjectKillTimer(cuttedObjLow);
        cuttedObjLow.layer = LayerMask.NameToLayer("Platform");

        AfterCutting();
    }

    public void AfterCutting()
    {
        if (!PlayerController.Instance.skillCheckSC.isPlayerSkillSuccessfull)
        {
            Destroy(cuttingObj);
            cuttingObj = null;
            cuttingObjMat = null;
        }
        
        //Needed Components
        currentStack = GameManager.Instance.currentStack.transform;
        currentStackCol = currentStack.GetComponent<BoxCollider>();

        float stackSize = StackXSizeCalculator(currentStack.localScale.x, currentStackCol.size.x);

        GameManager.Instance.KnifeLocalPosAdjuster(stackSize);
        GameManager.Instance.KnivesActivator(false);
        PlayerController.Instance.GoNextPlatform();
        StackCreator.Instance.CreateNewStack(stackSize);

        PlayerController.Instance.skillCheckSC.isPlayerSkillSuccessfull = false;
    }

    private void StartCuttedObjectKillTimer(GameObject obj)
    {
        obj.AddComponent<CuttedObjectKiller>();
    }
}
