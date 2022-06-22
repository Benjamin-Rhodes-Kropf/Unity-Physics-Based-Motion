using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyRotation : MonoBehaviour
{
    [SerializeField] private FIndJointRotation _fIndJointRotation;
    [SerializeField] private float rotationDirection;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    { 
        rotationDirection =  _fIndJointRotation.distanceToRightShoulder - _fIndJointRotation.distanceToLeftShoulder;
        if (rotationDirection < 1 && rotationDirection> 0 || rotationDirection > -1 && rotationDirection <0)
        {
            rotationDirection = 0;
        }
        transform.Rotate(0,rotationDirection/10,0);
    }
}
