using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCarDriver : MonoBehaviour
{

 public WheelCollider frontRightW,frontLeftW;     //left is Driver  right is Passengert 
 public WheelCollider rearRightW,rearLeftW;
 public Transform frontRightT,frontLeftT;
 public Transform rearRightT,rearLeftT;
 public float motorForce = 60;
public GameObject COM ;
public Rigidbody rigidbody;

public void Start(){
        rigidbody = GetComponent<Rigidbody>();

        COM = GameObject.Find("COM");
        rigidbody.centerOfMass = COM.transform.localPosition;
    
}
public void Accelerate(){
    frontLeftW.motorTorque = 1f * motorForce;
    frontRightW.motorTorque = 1f * motorForce;
    rearRightW.motorTorque = 1f * motorForce;
    rearLeftW.motorTorque = 1f * motorForce;
}
    public void UpdateWheelPoses(){
        UpdateWheelPoses(frontLeftW,frontLeftT);
        UpdateWheelPoses(frontRightW,frontRightT);
        UpdateWheelPoses(rearLeftW,rearLeftT);
        UpdateWheelPoses(rearRightW,rearRightT);
    }
    public void UpdateWheelPoses(WheelCollider _collider, Transform _transform){
        Vector3 _pos = _transform.position;
            Quaternion _quat = _transform.rotation;

            _collider.GetWorldPose(out _pos, out _quat);

            _transform.position = _pos;
            _transform.rotation = _quat;
    }
    private void FixedUpdate()
    {
        Accelerate();
            
    }
}
