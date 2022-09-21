using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EzySlice;

public class Knife : MonoBehaviour
{
    [SerializeField] LayerMask layer;
    Material cuttingObjMat;
    GameObject cuttingObj;

    private void Update()
    {
        if (cuttingObj)
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
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Platform"))
        {
            Debug.Log("heehhehehe");
            cuttingObj = other.gameObject;
            cuttingObjMat = other.GetComponent<MeshRenderer>().material;
        }
    }

    SlicedHull Cut(GameObject obj, Material mat = null)
    {
        return obj.Slice(transform.position, transform.up, mat);
    }
}
