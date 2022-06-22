using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float groundedForce; 
    [SerializeField] private float natrualforce;

    public float speed;
    public float strafeSpeed;
    public float jumpForce;
    public GameObject me;
    public Rigidbody hips;
    public ConfigurableJoint joint;
    public bool isGrounded;
    [SerializeField] private StaticFootData _staticBodyFoot;

    void Start()
    {
        hips = GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
        if (_staticBodyFoot.leftFootLocked || _staticBodyFoot.rightFootLocked)
        {
            hips.AddForce(hips.transform.forward * groundedForce);
        }
        hips.AddForce(hips.transform.forward * natrualforce);
        
        if (Input.GetKey(KeyCode.W))
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                hips.AddForce(hips.transform.forward * speed * 1.5f);
            }
            else
            {
                hips.AddForce(hips.transform.forward * speed);
            }
        }
        if (Input.GetKey(KeyCode.A))
        {
            hips.AddForce(hips.transform.right * strafeSpeed);
        }
        if (Input.GetKey(KeyCode.S))
        {
            hips.AddForce(-hips.transform.forward * speed * 1.5f);
        }
        if (Input.GetKey(KeyCode.D))
        {
            hips.AddForce(-hips.transform.right * strafeSpeed);
        }

        if (isGrounded)
        {
            JointDrive drive = joint.angularXDrive; // get a copy of the drive 
            drive.maximumForce = 10; // set the value that you want to change
            joint.angularXDrive = drive; // Set the joint's drive to our edited drive

            drive = joint.angularYZDrive; // get a copy of the drive 
            drive.maximumForce = 10; // set the value that you want to change
            joint.angularYZDrive = drive; // Set the joint's drive to our edited drive
        }
        else
        {
            JointDrive drive = joint.angularXDrive; // get a copy of the drive 
            drive.maximumForce = 3; // set the value that you want to change
            joint.angularXDrive = drive; // Set the joint's drive to our edited drive

            drive = joint.angularYZDrive; // get a copy of the drive 
            drive.maximumForce = 3; // set the value that you want to change
            joint.angularYZDrive = drive; // Set the joint's drive to our edited drive
        }
        
        ConfigurableJoint anchorJoint = joint;
        anchorJoint.connectedAnchor = Vector3.MoveTowards(anchorJoint.connectedAnchor, new Vector3(me.transform.position.x,1,me.transform.position.z), 5 * Time.deltaTime);
        joint.anchor = anchorJoint.anchor;

        if (Input.GetAxis("Jump") > 0)
        {
            if (isGrounded)
            {
                hips.AddForce(new Vector3(0, jumpForce, 0));
                isGrounded = false;
            }
        }
    }
}
