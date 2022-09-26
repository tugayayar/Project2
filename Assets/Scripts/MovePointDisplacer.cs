using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePointDisplacer : MonoBehaviour
{
    private Transform parent;
    BoxCollider parentCollider;

    private void OnEnable()
    {
        GetAllComponents();
        Displacer();
    }

    private void GetAllComponents()
    {
        parent = GameManager.Instance.currentStack.transform;
        parentCollider = parent.GetComponent<BoxCollider>();
    }

    private void Displacer()
    {
        transform.parent = parent;

        Vector3 desiredLocalPos = new Vector3(parentCollider.center.x, 0f, 0f);
        transform.localPosition = desiredLocalPos;
    }
}
