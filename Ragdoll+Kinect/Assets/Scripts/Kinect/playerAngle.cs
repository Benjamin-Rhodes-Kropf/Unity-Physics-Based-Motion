using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

public class playerAngle : MonoBehaviour
{

    public Transform myTarget;
    public Vector3 target;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(target);
    }
}
