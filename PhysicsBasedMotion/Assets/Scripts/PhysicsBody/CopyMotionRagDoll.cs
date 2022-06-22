using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyMotionRagDoll : MonoBehaviour
{
    [SerializeField] private RagDollManager _ragDollManager;
    private float positionSpringX;
    private float positionSpringYZ;
    public Transform targetLimb;
    public ConfigurableJoint cj;
    public bool mirror;
    // Start is called before the first frame update
    void Start()
    {
        cj = GetComponent<ConfigurableJoint>();
        positionSpringX = cj.angularXDrive.positionSpring;
        positionSpringYZ = cj.angularYZDrive.positionSpring;
    }

    // Update is called once per frame
    void Update()
    {
        if (!mirror)
        {
            cj.targetRotation = targetLimb.localRotation;
        }
        else
        {
            cj.targetRotation = Quaternion.Inverse(targetLimb.localRotation);
        }

        if (!_ragDollManager.jointsActive)
        {
            JointDrive jointDriveX = cj.angularXDrive;
            jointDriveX.positionSpring = 0;
            cj.angularXDrive = jointDriveX;
            
            JointDrive jointDriveYZ = cj.angularYZDrive;
            jointDriveYZ.positionSpring = 0;
            cj.angularYZDrive = jointDriveYZ;
        }
        else
        {
            JointDrive jointDriveX = cj.angularXDrive;
            jointDriveX.positionSpring = positionSpringX;
            cj.angularXDrive = jointDriveX;
            
            JointDrive jointDriveYZ = cj.angularYZDrive;
            jointDriveYZ.positionSpring = positionSpringYZ;
            cj.angularYZDrive = jointDriveYZ;
        }
    }
}
