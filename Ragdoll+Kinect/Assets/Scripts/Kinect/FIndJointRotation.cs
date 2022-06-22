using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 the goal for this script is to obtain the nessisary joint rotational values
 from Xbox Kinect so that we may make a physics based character that tries to follow the kinect.
*/

public class FIndJointRotation : MonoBehaviour
{
      //set to true
    private bool isGettingData = true;
    public float RotationSpeed;

    public float distanceToLeftShoulder;
    public float distanceToRightShoulder;

                               
     // Left
    private GameObject HandLeft;
    private GameObject ElbowLeft;
    private GameObject ShoulderLeft;
      private GameObject HipLeft;
      private GameObject KneeLeft;
      private GameObject FootLeft;

     // Right
    private GameObject HandRight;
    private GameObject ElbowRight;
    private GameObject ShoulderRight;
      private GameObject HipRight;
      private GameObject KneeRight;
      private GameObject FootRight;

     // mid
      private GameObject SpineBase;
      private GameObject SpineMid;
      private GameObject ShoulderMid;

    //up
    private GameObject Head;


    //  values that will be set in the Inspector

    //  the "target" refers to the position of the kinects players body
    private Transform ElbowLeftTarget;
    private Transform ShoulderLeftTarget;
    private Transform ElbowRightTarget;
    private Transform ShoulderRightTarget;
    private Transform HipLeftTarget;
    private Transform HipRightTarget;
    private Transform KneeRightTarget;
    private Transform KneeLeftTarget;
    private Transform SpineBaseHipTarget;
    private Transform SpineBaseTarget;
    private Transform SpineMidTarget;
    private Transform SpineShoulderMidTarget;

     // our red cubes(or clones of the kinects joints)
    public GameObject ElbowLeftClone;
    public GameObject ShoulderLeftClone;
    public GameObject ElbowRightClone;
    public GameObject ShoulderRightClone;
    public GameObject HipLeftClone;
    public GameObject HipRightClone;
    public GameObject KneeRightClone;
    public GameObject KneeLeftClone;
    public GameObject SpineBaseHipClone;
    public GameObject SpineBaseClone;
    public GameObject SpineMidClone;
    public GameObject SpineShoulderMidClone;

     // values for internal use
    private Quaternion ElbowLeft_LookRotation;
    private Quaternion ShoulderLeft_LookRotation;
    private Quaternion ElbowRight_LookRotation;
    private Quaternion ShoulderRight_LookRotation;
    private Quaternion HipLeft_LookRotation;
    private Quaternion HipRight_LookRotation;
    private Quaternion KneeRight_LookRotation;
    private Quaternion KneeLeft_LookRotation;

    //  spine
    private Quaternion SpineBaseHip_LookRotation;
    private Quaternion SpineBase_LookRotation;
    private Quaternion SpineMid_LookRotation;
    private Quaternion SpineShoulderMid_LookRotation;

    private Vector3 ElbowLeft_TargetDirection;
    private Vector3 ShoulderLeft_TargetDirection;
    private Vector3 ElbowRight_TargetDirection;
    private Vector3 ShoulderRight_TargetDirection;
    private Vector3 HipLeft_TargetDirection;
    private Vector3 HipRight_TargetDirection;
    private Vector3 KneeRight_TargetDirection;
    private Vector3 KneeLeft_TargetDirection;
  
     // spine
    private Vector3 SpineBaseHip_TargetDirection;
    private Vector3 SpineBase_TargetDirection;
    private Vector3 SpineMid_TargetDirection;
    private Vector3 SpineShoulderMid_TargetDirection;
    
    private void Start()
    {
        isGettingData = true;
    }

    private void Update()
    {
     //get the distance to left and right shoulder
     

     if (isGettingData == true)
        {
            GetData();
        }
        else
        {
         

         //    sets the clones position to the true kinects joints position
            ElbowLeftClone.transform.position = ElbowLeft.transform.position;
            ShoulderLeftClone.transform.position = ShoulderLeft.transform.position;
            ElbowRightClone.transform.position = ElbowRight.transform.position;
            ShoulderRightClone.transform.position = ShoulderRight.transform.position;
            HipLeftClone.transform.position = HipLeft.transform.position;
            HipRightClone.transform.position = HipRight.transform.position;
            KneeRightClone.transform.position = KneeRight.transform.position;
            KneeLeftClone.transform.position = KneeLeft.transform.position;

          //    spine
              SpineMidClone.transform.position = SpineMid.transform.position;
         

          //    sets the spine base to inbetween the hips
              //SpineBaseHipClone.transform.position = (HipLeft.transform.position+ HipRight.transform.position)*0.5f;
              SpineBaseClone.transform.position = (HipLeft.transform.position + HipRight.transform.position) * 0.5f;
              SpineShoulderMidClone.transform.position = (ShoulderLeft.transform.position + ShoulderRight.transform.position) * 0.5f;

             // find the vector pointing from our position (the xbox kinects joints) to the target (for example the elbows target is the hand)
            ElbowLeft_TargetDirection = (ElbowLeftTarget.position - ElbowLeftClone.transform.position).normalized;
            ShoulderLeft_TargetDirection = (ShoulderLeftTarget.position - ShoulderLeftClone.transform.position).normalized;
            ElbowRight_TargetDirection = (ElbowRightTarget.position - ElbowRightClone.transform.position).normalized;
            ShoulderRight_TargetDirection = (ShoulderRightTarget.position - ShoulderRightClone.transform.position).normalized;
              HipLeft_TargetDirection = (HipLeftTarget.position - HipLeftClone.transform.position).normalized;
              HipRight_TargetDirection = (HipRightTarget.position - HipRightClone.transform.position).normalized;
              KneeRight_TargetDirection = (KneeRightTarget.position - KneeRightClone.transform.position).normalized;
              KneeLeft_TargetDirection = (KneeLeftTarget.position - KneeLeftClone.transform.position).normalized;
            

             // spine
              //SpineBaseHip_TargetDirection = (SpineBaseHipTarget.position - SpineBaseHipClone.transform.position).normalized;
              SpineBase_TargetDirection = (SpineBaseTarget.position - SpineBaseClone.transform.position).normalized;
              SpineMid_TargetDirection = (SpineMidTarget.position - SpineMidClone.transform.position).normalized;
              SpineShoulderMid_TargetDirection = (SpineShoulderMidTarget.position - SpineShoulderMidClone.transform.position).normalized;
            

             // create the rotation we need to be in to look at the target
            ElbowLeft_LookRotation = Quaternion.LookRotation(ElbowLeft_TargetDirection);
            ShoulderLeft_LookRotation = Quaternion.LookRotation(ShoulderLeft_TargetDirection, new Vector3(0,1,0));
            ElbowRight_LookRotation = Quaternion.LookRotation(ElbowRight_TargetDirection);
            ShoulderRight_LookRotation = Quaternion.LookRotation(ShoulderRight_TargetDirection);
              HipLeft_LookRotation = Quaternion.LookRotation(HipLeft_TargetDirection);
              HipRight_LookRotation = Quaternion.LookRotation(HipRight_TargetDirection);
              KneeRight_LookRotation = Quaternion.LookRotation(KneeRight_TargetDirection);
              KneeLeft_LookRotation = Quaternion.LookRotation(KneeLeft_TargetDirection);

             // spine
              //SpineBaseHip_LookRotation = Quaternion.LookRotation(SpineBaseHip_TargetDirection);
              SpineBase_LookRotation = Quaternion.LookRotation(SpineBase_TargetDirection); 


              SpineMid_LookRotation = Quaternion.LookRotation(SpineMid_TargetDirection, new Vector3(1,0,0));

              // SpineShoulderMid_LookRotation = Quaternion.LookRotation(SpineShoulderMid_TargetDirection);

             // rotate us over time according to speed until we are in the required rotation
            ElbowLeftClone.transform.rotation = Quaternion.Slerp(ElbowLeftClone.transform.rotation, ElbowLeft_LookRotation, Time.deltaTime * 10f);
            ShoulderLeftClone.transform.rotation = Quaternion.Slerp(ShoulderLeftClone.transform.rotation, ShoulderLeft_LookRotation, Time.deltaTime * RotationSpeed);
            ElbowRightClone.transform.rotation = Quaternion.Slerp(ElbowRightClone.transform.rotation, ElbowRight_LookRotation, Time.deltaTime * RotationSpeed);
            ShoulderRightClone.transform.rotation = Quaternion.Slerp(ShoulderRightClone.transform.rotation, ShoulderRight_LookRotation, Time.deltaTime * RotationSpeed);
            HipLeftClone.transform.rotation = Quaternion.Slerp(HipLeftClone.transform.rotation, HipLeft_LookRotation, Time.deltaTime * RotationSpeed);
            HipRightClone.transform.rotation = Quaternion.Slerp(HipRightClone.transform.rotation, HipRight_LookRotation, Time.deltaTime * RotationSpeed);
            KneeRightClone.transform.rotation = Quaternion.Slerp(KneeRightClone.transform.rotation, KneeRight_LookRotation, Time.deltaTime * RotationSpeed);
            KneeLeftClone.transform.rotation = Quaternion.Slerp(KneeLeftClone.transform.rotation, KneeLeft_LookRotation, Time.deltaTime * RotationSpeed);

             // spine
              //SpineBaseHipClone.transform.rotation = Quaternion.Slerp(SpineBaseHipClone.transform.rotation, SpineBaseHip_LookRotation, Time.deltaTime * RotationSpeed);
              SpineBaseClone.transform.rotation = Quaternion.Slerp(SpineBaseClone.transform.rotation, SpineBase_LookRotation, Time.deltaTime * RotationSpeed);
             SpineMidClone.transform.rotation= Quaternion.Slerp(SpineMidClone.transform.rotation, SpineMid_LookRotation, Time.deltaTime * RotationSpeed);
              SpineShoulderMidClone.transform.rotation = Quaternion.Slerp(SpineShoulderMidClone.transform.rotation, SpineShoulderMid_LookRotation, Time.deltaTime * RotationSpeed);

            Debug.DrawRay(HipLeftClone.transform.position, KneeRightClone.transform.position);


            distanceToLeftShoulder = Vector3.Distance(new Vector3(0, 0, 0), ShoulderLeft.transform.position);
            distanceToRightShoulder = Vector3.Distance(new Vector3(0, 0, 0), ShoulderRight.transform.position);
        }

    }

    void GetData()
    {
        //  finds game objects in higherarchy and sets then equal to our game objects
        HandLeft = GameObject.Find("HandLeft");
        ElbowLeft = GameObject.Find("ElbowLeft");
        ShoulderLeft = GameObject.Find("ShoulderLeft");
        HandRight = GameObject.Find("HandRight");
        ElbowRight = GameObject.Find("ElbowRight");
        ShoulderRight = GameObject.Find("ShoulderRight");
        HipLeft = GameObject.Find("HipLeft");
        HipRight = GameObject.Find("HipRight");
        KneeRight = GameObject.Find("KneeRight");
        KneeLeft = GameObject.Find("KneeLeft");
        FootRight = GameObject.Find("FootRight");
        FootLeft = GameObject.Find("FootLeft");
        ShoulderMid = GameObject.Find("SpineShoulder");
        SpineMid = GameObject.Find("SpineMid");
        SpineBase = GameObject.Find("SpineBase");
        Head = GameObject.Find("Head");


        if (HandLeft != null)
        {
              //sets the targets (end joints for example hands dont get targets or clones)
            ElbowLeftTarget = HandLeft.transform;
            ShoulderLeftTarget = ElbowLeft.transform;
            ElbowRightTarget = HandRight.transform;
            ShoulderRightTarget = ElbowRight.transform;
            HipLeftTarget = KneeLeftClone.transform;
            HipRightTarget = KneeRightClone.transform;
            KneeRightTarget = FootRight.transform;
            KneeLeftTarget = FootLeft.transform;

            //  spine
              //SpineBaseHipTarget = HipLeft.transform;
              SpineBaseTarget = SpineMid.transform;
              SpineMidTarget = ShoulderMid.transform;
              SpineShoulderMidTarget = Head.transform;

           //   figures out if we are still serching for a player
            if (HandLeft.gameObject.transform.position != new Vector3(0, 0, 0))
            {
                isGettingData = false;
                Debug.Log("DoneSerching!");
            }
        }
    }
}