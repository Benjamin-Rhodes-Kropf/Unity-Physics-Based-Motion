using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform playerLocation;
    [SerializeField]private Vector3 offset;
    [SerializeField] private float followSpeed;

    // Update is called once per frame
    [ExecuteInEditMode]
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, new Vector3(0,0,playerLocation.position.z)+offset,Time.deltaTime*followSpeed);
    }
}
