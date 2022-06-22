using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Windows.Kinect;
using UnityEngine;

public class HandAnchor : MonoBehaviour
{
    [SerializeField] private BodySourceManager _bodyManager;
    
    [Header("attributes")]
    [SerializeField] private bool isHandLeft;
    [SerializeField] private bool handClosed;
    [SerializeField] private MeshRenderer _meshRenderer;
    [SerializeField] private Material closed;
    [SerializeField] private Material open;
    
    [Header("Joint")]
    [SerializeField] private Rigidbody _anchorRb;
    [SerializeField] private Transform _anchorTransform;
    [SerializeField] private Vector3 anchorOffset;
    private ConfigurableJoint _anchorJoint;
    [SerializeField] private float breakForce;
    [SerializeField] private float breakTorque;

    private void Update()
    {
        var data = _bodyManager.GetData();
        foreach(var body in data)
        {
            if (body == null)
            {
                continue;
            }
                
            if(body.IsTracked)
            {
                if (isHandLeft)
                {
                    if (body.HandLeftState.ToString() == "Closed")
                    {
                        handClosed = true;
                        _meshRenderer.material = closed;
                    } else if(body.HandLeftState.ToString() == "Open")
                    {
                        handClosed = false;
                        _meshRenderer.material = open;
                        _anchorJoint.breakForce = 0;
                    }
                }else
                {
                    if (body.HandRightState.ToString() == "Closed")
                    {
                        handClosed = true;
                        _meshRenderer.material = closed;
                    } else if(body.HandRightState.ToString() == "Open")
                    {
                        handClosed = false;
                        _meshRenderer.material = open;
                        _anchorJoint.breakForce = 0;
                    }
                }
                // Debug.LogWarning("size:" + data.Length);
                // Debug.LogWarning(  "handLeft state: "+ body.HandLeftState +" HandLeftConfidence:" + body.HandLeftConfidence + "body_id:" + body.TrackingId);
                // Debug.LogWarning(  "handRight state: "+ body.HandRightState +" HandLeftConfidence:" + body.HandRightConfidence + "body_id:" + body.TrackingId);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Wall" || collision.gameObject.tag == "Terrain"   && handClosed)
        {
            if (gameObject.GetComponents(typeof(ConfigurableJoint)).Length < 2 && handClosed)
            {
                Vector3 tpos = transform.position;
                _anchorTransform.position = new Vector3(tpos.x+anchorOffset.x, tpos.y+anchorOffset.y, tpos.z+anchorOffset.z);
                _anchorJoint = gameObject.AddComponent<ConfigurableJoint>();
                _anchorJoint.xMotion = ConfigurableJointMotion.Locked;
                _anchorJoint.yMotion = ConfigurableJointMotion.Locked;
                _anchorJoint.zMotion = ConfigurableJointMotion.Locked;
                
                    // AnchorJoint.autoConfigureConnectedAnchor = false;
                    // AnchorJoint.anchor = new Vector3(0, 0, 0);
                    // AnchorJoint.connectedAnchor = new Vector3(0, 0, 0);
                
                _anchorJoint.connectedBody = _anchorRb;
                _anchorJoint.breakForce = breakForce;
                _anchorJoint.breakTorque = breakTorque;
            }
        }
    }
}