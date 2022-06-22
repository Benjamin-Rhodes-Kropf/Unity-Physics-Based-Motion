using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JointRotationForCloneCharIndiv : MonoBehaviour
{

    public float RotationSpeed;

    public float Debugrot;

    //  the "target" refers to the position of the kinects players body
    

    // our red cubes(or clones of the kinects joints)
    public GameObject me;
    public Transform target;

    // values for internal use
    private Quaternion ElbowLeft_LookRotation;



    private Vector3 ElbowLeft_TargetDirection;


    private void Start()
    {
    }

    private void Update()
    {



            // create the rotation we need to be in to look at the target

            // rotate us over time according to speed until we are in the required rotation
            me.transform.rotation = Quaternion.Slerp(me.transform.rotation, target.rotation, Time.deltaTime * 10f);

    }
}