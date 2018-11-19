using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateOnAnimationEvent : MonoBehaviour {
    public Transform target;
    public GameObject[] prefab;
    public void Instantiate(int index)
    {
        if (prefab == null)
            return;
        GameObject obj = (GameObject)Instantiate(prefab[index], target.position, target.rotation);
        obj.transform.localScale = new Vector3(Mathf.Sign(transform.lossyScale.x) * Mathf.Sign(obj.transform.localScale.x), obj.transform.localScale.y, obj.transform.localScale.z);
    }

}
