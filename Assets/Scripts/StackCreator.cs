using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackCreator : MonoBehaviour
{
    [SerializeField] Transform stackPrefab;

    private void Start()
    {
        
    }

    public Transform CreateStack()
    {
        return Instantiate(stackPrefab, Vector3.zero, Quaternion.identity);
    }


}
