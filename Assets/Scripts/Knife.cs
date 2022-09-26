using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EzySlice;

public class Knife : MonoBehaviour
{
    [Header("Cutting Stack Variables")]
    [SerializeField] LayerMask layer;
    Material cuttingObjMat;
    GameObject cuttingObj;

    [Header("Knife Types")]
    public KnifeType knifeType;
    public enum KnifeType
    {
        Left,
        Right
    }

    [Header("Next Stack Components")]
    Transform nextStack;
    BoxCollider nextStackCol;

    //[Header("Knives Position Variables")]
    //float cuttedKnifeLocalXPos;
    //float nonCuttedKinefeLocalXPos;

    //private void Start()
    //{
    //    //KnifeParentAdjuster(GameManager.Instance.currentStack.transform);

    //}

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
        }
    }

    //public void KnifeParentAdjuster(Transform parent)
    //{
    //    transform.parent = parent;

    //    //Needed Components
    //    nextStack = GameManager.Instance.nextStack.transform;
    //    nextStackCol = nextStack.GetComponent<BoxCollider>();

    //    //Change Local Positions

    //    //if (knifeType == KnifeType.Left)
    //    //    transform.localPosition = LocalPosCalculater(parent.localScale, -parentCollider.size.x);
    //    //else
    //    //    transform.localPosition = LocalPosCalculater(parent.localScale, parentCollider.size.x);

    //    GameManager.Instance.KnivesActivator(false);
    //}

    //private Vector3 LocalPosCalculater(Vector3 parentLocalScale, float colliderXPos)
    //{
    //    Vector3 desiredLocalPos = new Vector3(parentLocalScale.x * colliderXPos, 0f, parentLocalScale.z);
    //    return desiredLocalPos;
    //}

    //private void KnifeLocalPosAdjuster(Transform knife, float xLocalPos)
    //{
    //    Vector3 desiredPos = new Vector3(xLocalPos, 0f, stackForwardSize);
    //    knife.localPosition = desiredPos;
    //}

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
        cuttedObjLow.layer = LayerMask.NameToLayer("Platform");

        AfterCutting();
    }

    private void AfterCutting()
    {
        Destroy(cuttingObj);
        cuttingObj = null;
        cuttingObjMat = null;

        //KnifeParentAdjuster(nextStack);

        //Needed Components
        nextStack = GameManager.Instance.nextStack.transform;
        nextStackCol = nextStack.GetComponent<BoxCollider>();

        float stackSize = StackXSizeCalculator(nextStack.localScale.x, nextStackCol.size.x);

        GameManager.Instance.KnifeLocalPosAdjuster(stackSize);
        GameManager.Instance.KnivesActivator(false);
        PlayerController.Instance.GoNextPlatform();
        StackCreator.Instance.CreateNewStack(stackSize);
    }
}
