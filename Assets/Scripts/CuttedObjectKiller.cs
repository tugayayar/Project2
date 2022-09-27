using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttedObjectKiller : MonoBehaviour
{
    [SerializeField] private float lifeTime = 3f;

    private void OnEnable()
    {
        StartCoroutine(KillThisObject(lifeTime));
    }

    IEnumerator KillThisObject(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}