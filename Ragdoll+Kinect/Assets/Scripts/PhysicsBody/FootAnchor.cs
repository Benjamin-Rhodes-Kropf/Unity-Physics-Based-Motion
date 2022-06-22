using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootAnchor : MonoBehaviour
{
    [SerializeField] private bool leftFoot;
    [SerializeField] private StaticFootData _staticBodyFoot;
    [SerializeField] private bool isActive;
    [SerializeField] private bool tocuhingGround;
    [SerializeField] private Rigidbody anchorRb;
    [SerializeField] private Transform AnchorTransform;
    [SerializeField] private Vector3 anchorOffset;
    [SerializeField] private bool autoConfigure;
    [SerializeField] private ConfigurableJoint AnchorJoint;
    [SerializeField] private float breakForce;
    [SerializeField] private float breakTorque;

    private void Update()
    {
        if (!_staticBodyFoot.rightFootLocked && !leftFoot)
        {
            AnchorJoint.breakForce = 0;
        }
        else if(tocuhingGround && !leftFoot && _staticBodyFoot.rightFootLocked){
            makeJoint();
        }

        if (!_staticBodyFoot.leftFootLocked && leftFoot)
        {
            AnchorJoint.breakForce = 0;
        }else if(tocuhingGround && leftFoot && _staticBodyFoot.leftFootLocked){
            makeJoint();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Terrain" && isActive && _staticBodyFoot.rightFootLocked && !leftFoot)
        {
            tocuhingGround = true;
        }
        if (collision.gameObject.tag == "Terrain" && isActive && _staticBodyFoot.leftFootLocked && leftFoot)
        {
            tocuhingGround = true;
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.tag == "Terrain" && isActive && _staticBodyFoot.rightFootLocked && !leftFoot)
        {
            tocuhingGround = false;
        }
        if (other.gameObject.tag == "Terrain" && isActive && _staticBodyFoot.leftFootLocked && leftFoot)
        {
            tocuhingGround = false;
        }
    }

    private void makeJoint()
    {
        if (gameObject.GetComponents(typeof(ConfigurableJoint)).Length < 2)
        {
            Vector3 tpos = transform.position;
            AnchorTransform.position = new Vector3(tpos.x+anchorOffset.x, tpos.y+anchorOffset.y, tpos.z+anchorOffset.z);
            AnchorJoint = gameObject.AddComponent<ConfigurableJoint>();
            AnchorJoint.xMotion = ConfigurableJointMotion.Locked;
            AnchorJoint.yMotion = ConfigurableJointMotion.Locked;
            AnchorJoint.zMotion = ConfigurableJointMotion.Locked;
            if (!autoConfigure)
            {
                AnchorJoint.autoConfigureConnectedAnchor = false;
                AnchorJoint.anchor = new Vector3(0, 0, 0);
                AnchorJoint.connectedAnchor = new Vector3(0, 0, 0);
            }
            AnchorJoint.connectedBody = anchorRb;
            AnchorJoint.breakForce = breakForce;
            AnchorJoint.breakTorque = breakTorque;
        }
    }
}
