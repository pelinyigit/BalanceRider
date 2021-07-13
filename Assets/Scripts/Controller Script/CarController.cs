using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;

public class CarController : MonoBehaviour
{
    private const string HORIZONTAL = "Horizontal";
    private const string VERTICAL = "Vertical";
    private float horizontalInput;
    private float verticalInput;
    private float steerAngle;
    private float currentSteerAngle;
    private float currentBreakForce;
    private bool isBreaking;
    [SerializeField] private GameObject car;
    [SerializeField] private float speed;
    [SerializeField] private float maxSpeedLimit;

    public float AntiRoll = 5000.0f;
    public float rotatez;

    [SerializeField] private float motorForce;
    [SerializeField] private float breakForce;
    [SerializeField] private float inputBreakForce;
    [SerializeField] private float maxSteerAngle;
    private Vector3 startMousePos = Vector3.zero;
    private Vector3 deltaMousePos = Vector3.zero;
    private float direction = 0f;
    [SerializeField] public WheelCollider frontLeftWheelCollider;

    [SerializeField] public WheelCollider frontRightWheelCollider;

    [SerializeField] public WheelCollider rearLeftWheelCollider;

    [SerializeField] public WheelCollider rearRightWheelCollider;

    [SerializeField] private Transform frontLeftWheelTransform;

    [SerializeField] private Transform frontRightWheelTransform;

    [SerializeField] private Transform rearLeftWheelTransform;

    [SerializeField] private Transform rearRightWheelTransform;

    private void FixedUpdate()
    {


        //rotatez = currentSteerAngle * (10/30f);

        GetInput();
        InputController();
        HorizontalInputController();
        HandleSteer();
        HandleMotor();
        HandleSteering();
        UpdateWheels();
       // AntiRollRotation();
     //  Debug.Log(transform.localEulerAngles.z);
    }

    private void GetInput()
    {
        //  horizontalInput = Input.GetAxis(HORIZONTAL);
        // isBreaking = Input.GetKey(KeyCode.Space);

        verticalInput = Input.GetAxis(VERTICAL);

    }
    private void AntiRollRotation()
    {
        WheelHit hit;
        float travelL = 1.0f;
        float travelR = 1.0f;

        bool groundedFL = frontLeftWheelCollider.GetGroundHit(out hit);

        if (groundedFL)

            travelL = (-frontLeftWheelCollider.transform.InverseTransformPoint(hit.point).y - frontLeftWheelCollider.radius) / frontLeftWheelCollider.suspensionDistance;

        bool groundedRL = rearLeftWheelCollider.GetGroundHit(out hit);

        if (groundedRL)

            travelL = (-rearLeftWheelCollider.transform.InverseTransformPoint(hit.point).y - rearLeftWheelCollider.radius) / rearLeftWheelCollider.suspensionDistance;

        bool groundedFR = frontRightWheelCollider.GetGroundHit(out hit);

        if (groundedFR)

            travelR = (-frontRightWheelCollider.transform.InverseTransformPoint(hit.point).y - frontRightWheelCollider.radius) / frontRightWheelCollider.suspensionDistance;

        bool groundedRR = rearRightWheelCollider.GetGroundHit(out hit);

        if (groundedRR)

            travelR = (-rearRightWheelCollider.transform.InverseTransformPoint(hit.point).y - rearRightWheelCollider.radius) / rearRightWheelCollider.suspensionDistance;


        float antiRollForce = (travelL - travelR) * AntiRoll;

        if (groundedFL)

            GetComponent<Rigidbody>().AddForceAtPosition(frontLeftWheelCollider.transform.up * -antiRollForce, frontLeftWheelCollider.transform.position);

        if (groundedRL)

            GetComponent<Rigidbody>().AddForceAtPosition(rearLeftWheelCollider.transform.up * -antiRollForce, rearLeftWheelCollider.transform.position);

        if (groundedFR)

            GetComponent<Rigidbody>().AddForceAtPosition(frontRightWheelCollider.transform.up * antiRollForce, frontRightWheelCollider.transform.position);

        if (groundedRR)


            GetComponent<Rigidbody>().AddForceAtPosition(rearRightWheelCollider.transform.up * antiRollForce, rearRightWheelCollider.transform.position);
    }

    private void InputController()
    {
        if (Input.GetMouseButton(0))
        {
            speed = speed < maxSpeedLimit ? speed + Mathf.Lerp(0, maxSpeedLimit, Time.deltaTime) : maxSpeedLimit;
            isBreaking = false;
        }
        else
        {
            speed = speed >= 0f ? speed -= Time.deltaTime * 15f : 0f;
            isBreaking = true;
        }
    }

    private void HandleSteer()
    {
        

        if (Mathf.Abs(direction) > Mathf.Abs(300f))
        {
            if (direction < 0)
            {
                currentSteerAngle = 30f;
                //transform.DOLocalRotate(new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, -10f), 0f, RotateMode.Fast);
            }
            else
            {
                currentSteerAngle = -30f;
               // transform.DOLocalRotate(new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 10f), 0f, RotateMode.Fast);
        
            
            }

        }
        else if (Mathf.Abs(direction) <= Mathf.Abs(300f))
        {

            if (Mathf.Abs(direction) >= Mathf.Abs(100f))
            {

                currentSteerAngle = -direction / 50f;
              
            }
        }
    }

    private void HorizontalInputController()
    {


        if (Input.GetMouseButtonDown(0))
        {
            startMousePos = Input.mousePosition;
        }
        if (Input.GetMouseButton(0))
        {
            deltaMousePos = Input.mousePosition;
            direction = startMousePos.x - deltaMousePos.x;
        }
        if (Input.GetMouseButtonUp(0))
        {
            direction = 0f;
        }
    }


    private void HandleMotor()
    {
        frontLeftWheelCollider.motorTorque = speed * motorForce;
        frontRightWheelCollider.motorTorque = speed * motorForce;
        breakForce = isBreaking ? inputBreakForce : 0f;

        frontLeftWheelCollider.brakeTorque = breakForce;
        frontRightWheelCollider.brakeTorque = breakForce;
        rearLeftWheelCollider.brakeTorque = breakForce;
        rearRightWheelCollider.brakeTorque = breakForce;

        /*  if (isBreaking)
                {
                    ApplyBreaking();
                }
            */
    }

    /*    private void ApplyBreaking()
        {
            frontLeftWheelCollider.brakeTorque = currentBreakForce;
            frontRightWheelCollider.brakeTorque = currentBreakForce;
            rearLeftWheelCollider.brakeTorque = currentBreakForce;
            rearRightWheelCollider.brakeTorque = currentBreakForce;
        }
    */
    private void HandleSteering()
    {
        // currentSteerAngle = maxSteerAngle * horizontalInput;
        frontLeftWheelCollider.steerAngle = currentSteerAngle;
        frontRightWheelCollider.steerAngle = currentSteerAngle;
    }
    private void UpdateWheels()
    {
        UpdateSingleWheel(frontLeftWheelCollider, frontLeftWheelTransform);
        UpdateSingleWheel(frontRightWheelCollider, frontRightWheelTransform);
        UpdateSingleWheel(rearLeftWheelCollider, rearLeftWheelTransform);
        UpdateSingleWheel(rearRightWheelCollider, rearRightWheelTransform);
    }
    private void UpdateSingleWheel(WheelCollider wheelCollider, Transform wheelTransform)
    {
        Vector3 pos;
        Quaternion rot;
        wheelCollider.GetWorldPose(out pos, out rot);
        wheelTransform.rotation = rot;
        wheelTransform.position = pos;
    }
}
