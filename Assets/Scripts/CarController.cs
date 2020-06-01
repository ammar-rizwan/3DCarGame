using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.UI;
//Video 8 ko Ignore kia tha

public class CarController : MonoBehaviour
{

    public WheelCollider[] wheels= new WheelCollider[4];
    public GameObject[] wheelMesh = new GameObject[4];
    //public GameObject WheelCollider, WheelMeshes;
    private InputManager IM;
    private Rigidbody rigidbody;
    public AnimationCurve enginePower;
    public GameObject CenterofMass;
    public float KPH;
    public float MotorSpeed = 100;
    public float DownForceValue = 50;
    public float radius = 6;
    public float brakePower = 100;
    //public float totalPower;
    public float wheelsRPM;
    //public float engineRPM;
    public float[] gears;
    public float smoothTime=0.09f;
    public int gearNum = 1;
    public GameObject gameUI;

    //public void GetInput(){
    //    m_horizontalInput = CrossPlatformInputManager.GetAxis("Horizontal");
    //    m_verticalInput= CrossPlatformInputManager.GetAxis("Vertical");
    //}

    private void Start()
    {
        getObject();
    }

    private void FixedUpdate()
{
        addDownForce();
        moveVehicle();
        steerVehicle();
        //animatedWheels();

    }
    private void moveVehicle()
    {
        for (int i = 0; i < wheels.Length; i++)
        {
            wheels[i].motorTorque = IM.vertical *  MotorSpeed;
        }
        KPH = rigidbody.velocity.magnitude * 3.6f;
        if (IM.Brake)
        {
            wheels[2].brakeTorque = wheels[3].brakeTorque = brakePower;
        }
        else
        {
            wheels[2].brakeTorque = wheels[3].brakeTorque = 0;
        }
    }
    private void steerVehicle()
    {


        //acerman steering formula
        //steerAngle = Mathf.Rad2Deg * Mathf.Atan(2.55f / (radius + (1.5f / 2))) * horizontalInput;

        if (IM.horizontal > 0)
        {
            //rear tracks size is set to 1.5f       wheel base has been set to 2.55f
            wheels[0].steerAngle = Mathf.Rad2Deg * Mathf.Atan(2.55f / (radius + (1.5f / 2))) * IM.horizontal;
            wheels[1].steerAngle = Mathf.Rad2Deg * Mathf.Atan(2.55f / (radius - (1.5f / 2))) * IM.horizontal;
        }
        else if (IM.horizontal < 0)
        {
            wheels[0].steerAngle = Mathf.Rad2Deg * Mathf.Atan(2.55f / (radius - (1.5f / 2))) * IM.horizontal;
            wheels[1].steerAngle = Mathf.Rad2Deg * Mathf.Atan(2.55f / (radius + (1.5f / 2))) * IM.horizontal;
            //transform.Rotate(Vector3.up * steerHelping);

        }
        else
        {
            wheels[0].steerAngle = 0;
            wheels[1].steerAngle = 0;
        }

    }
    void animatedWheels()
    {
        Vector3 wheelsPosition = Vector3.zero;
        Quaternion wheelRotation = Quaternion.identity;

        for (int i = 0; i < 4; i++)
        {
            wheels[i].GetWorldPose(out wheelsPosition, out wheelRotation);
            wheelMesh[i].transform.position = wheelsPosition;
            wheelMesh[i].transform.rotation = wheelRotation;
        }
    }
    private void addDownForce()
    {

        rigidbody.AddForce(-transform.up * DownForceValue * rigidbody.velocity.magnitude);

    }
    //private void calculateEnginePower()
    //{

    //    wheelRPM();

    //    totalPower = 3.6f * enginePower.Evaluate(engineRPM) * (gears[gearNum]) *(IM.vertical);




    //    float velocity = 0.0f;

    //    engineRPM = Mathf.SmoothDamp(engineRPM, 1000 + (Mathf.Abs(wheelsRPM) * 3.6f * (gears[gearNum])), ref velocity, smoothTime);

    //}

    private void wheelRPM()
    {
        float sum = 0;
        int R = 0;
        for (int i = 0; i < 4; i++)
        {
            sum += wheels[i].rpm;
            R++;
        }
        wheelsRPM = (R != 0) ? sum / R : 0;


    }
    private void getObject()
    {
        IM = GetComponent<InputManager>();
        rigidbody = GetComponent<Rigidbody>();
        CenterofMass = GameObject.Find("mass");
        //WheelCollider = GameObject.Find("WheelsColliders");
        //WheelMeshes = GameObject.Find("Wheels");
        rigidbody.centerOfMass = CenterofMass.transform.localPosition;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "Opponent")
        {
            gameUI.gameObject.SetActive(true);
            gameObject.SetActive(false);
        }
    }

}
