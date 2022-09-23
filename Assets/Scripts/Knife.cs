using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EzySlice;

public class Knife : MonoBehaviour
{
    [Header("Cutting Object Variables")]
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

    private void Start()
    {
        KnifeParentAdjuster(GameManager.Instance.currentStack.transform);
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

    public void KnifeParentAdjuster(Transform parent)
    {
        transform.parent = parent;

        //Needed Components
        BoxCollider parentCollider = parent.GetComponent<BoxCollider>();

        //Change Local Positions
        if (knifeType == KnifeType.Left)
            transform.localPosition = LocalPosCalculater(parent.localScale, -parentCollider.size.x);
        else
            transform.localPosition = LocalPosCalculater(parent.localScale, parentCollider.size.x);
    }

    private Vector3 LocalPosCalculater(Vector3 parentLocalScale, float colliderXPos)
    {
        Vector3 desiredLocalPos = new Vector3(parentLocalScale.x * colliderXPos, 0f, parentLocalScale.z);
        return desiredLocalPos;
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
        Rigidbody rb = cuttedObjLow.AddComponent<Rigidbody>();
        rb.useGravity = false;
        rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePosition;
        cuttedObjLow.layer = LayerMask.NameToLayer("Platform");

        Destroy(cuttingObj);
    }

    private void CutRight()
    {
        SlicedHull cutted = Cut(cuttingObj, cuttingObjMat);

        GameObject cuttedObjUp = cutted.CreateUpperHull(cuttingObj, cuttingObjMat);
        cuttedObjUp.AddComponent<BoxCollider>().isTrigger = false;
        Rigidbody rb = cuttedObjUp.AddComponent<Rigidbody>();
        rb.useGravity = false;
        rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePosition;
        cuttedObjUp.layer = LayerMask.NameToLayer("Platform");

        GameObject cuttedObjLow = cutted.CreateLowerHull(cuttingObj, cuttingObjMat);
        cuttedObjLow.AddComponent<BoxCollider>().isTrigger = false;
        cuttedObjLow.AddComponent<Rigidbody>();
        cuttedObjLow.layer = LayerMask.NameToLayer("Platform");

        Destroy(cuttingObj);
    }
}
