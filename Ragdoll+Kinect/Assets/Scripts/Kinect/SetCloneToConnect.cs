using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCloneToConnect : MonoBehaviour
{
    private bool isGettingData = true;
    [SerializeField]private Transform myTrans;
    [SerializeField]private GameObject targetGameObject;
    [SerializeField]private string targetGameObjectName;
    
    // Update is called once per frame
    private void Start()
    {
        
    }


    void Update()
    {
        if(isGettingData == true)
        {
            GetData();
        }
        if (targetGameObject == null)
        {
            isGettingData = true;
            Debug.Log("lost game object: resuming search");
        }
        if (isGettingData == false)
        {
            myTrans.rotation = targetGameObject.transform.rotation;
        }
    }
    void GetData()
    {
        //  finds game objects in higherarchy and sets then equal to our game objects
        targetGameObject = GameObject.Find(targetGameObjectName);
        Debug.Log(targetGameObject);
        if (targetGameObject != null)
        {
            targetGameObject = GameObject.Find(targetGameObjectName);
            isGettingData = false;
            Debug.Log("Found Game Object!");

        }
    }
}

