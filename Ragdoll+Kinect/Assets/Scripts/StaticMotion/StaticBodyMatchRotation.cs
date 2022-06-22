using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticBodyMatchRotation : MonoBehaviour
{
    [SerializeField] private StaticPlayerControler _staticPlayerControler;
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset;
    void Update()
    {
        if (_staticPlayerControler.followKinectEnabled)
        {
            transform.rotation = target.rotation;
            transform.Rotate(offset.x,offset.y,offset.z);
        }
    }
}
