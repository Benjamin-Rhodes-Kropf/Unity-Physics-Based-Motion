using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticBodyFoot : MonoBehaviour
{

    [SerializeField] private Transform footRight;
    [SerializeField] private Transform footRightLimit;
    [SerializeField] private Transform footLeft;
    [SerializeField] private Transform footLeftLimit;

    [SerializeField] private Material footlockedMat;
    [SerializeField] private Material footunlockedMat;
    [SerializeField] private MeshRenderer _meshRendererRight;
    [SerializeField] private MeshRenderer _meshRendererLeft;

    public bool leftFootLocked;
    public bool rightFootLocked;


    private void Update()
    {
        if (footRight.position.y < footRightLimit.position.y)
        {
            _meshRendererRight.material = footlockedMat;
            rightFootLocked = true;
        }
        else
        {
            _meshRendererRight.material = footunlockedMat;
            rightFootLocked = false;
        }
        
        if (footLeft.position.y < footLeftLimit.position.y)
        {
            _meshRendererLeft.material = footlockedMat;
            leftFootLocked = true;
        }
        else
        {
            _meshRendererLeft.material = footunlockedMat;
            leftFootLocked = false;
        }
    }
}
