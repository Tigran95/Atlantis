using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingltonesParent : MonoBehaviour
{
    void Awake()
    {
        SingltonesParent[] objs = FindObjectsOfType<SingltonesParent>();
        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }
}
