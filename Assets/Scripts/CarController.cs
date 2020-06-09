﻿using System.Collections;
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
    public float brakePower = 500;
    //public float totalPower;
    public float wheelsRPM;
    //public float engineRPM;
    public float[] gears;
    public float smoothTime=0.09f;
    public int gearNum = 1;
    public GameObject gameUI;
    public AudioClip crash;
    public float topSpeed = 60;
    //private float currentSpeed = 0;
    private float pitch = 0;
    private AudioSource[] audioSource;
    public float distanceTravelled = 0;
    Vector3 lastPosition;

    public Text gameOverText;


    //public void GetInput(){
    //    m_horizontalInput = CrossPlatformInputManager.GetAxis("Horizontal");
    //    m_verticalInput= CrossPlatformInputManager.GetAxis("Vertical");
    //}

    private void Start()
    {
        getObject();
        lastPosition = transform.position;
        // gameUI = GameObject.FindGameObjectsWithTag("UITag")[1];

    }

    private void FixedUpdate()
{
        addDownForce();
        moveVehicle();
        steerVehicle();

        distanceTravelled += Vector3.Distance(transform.position, lastPosition);
        lastPosition = transform.position;
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

        float angle = 15 * IM.horizontal;
        wheels[0].steerAngle = angle;
        wheels[1].steerAngle = angle;


        //rigidbody.velocity = new Vector3(IM.horizontal * 5, 0f, IM.vertical*20);


        //acerman steering formula
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
           
            AudioSource.PlayClipAtPoint(crash, transform.position, 1.0F);
            //crash.Play();
            
        //    gameOverText = gameUI.gameObject.GetComponent<Text>();

        //    gameOverText.text = "Game Over!";
        //     gameUI.transform.GetChild(0).gameObject.SetActive(true);
        //     gameUI.transform.GetChild(1).gameObject.SetActive(true);
    // gameUI.gameObject.SetActive(true);
    //         gameObject.SetActive(false);
    gameObject.SetActive(false);
        }
    }
    void Update()
    {
        // currentSpeed = transform.GetComponent<Rigidbody>().velocity.magnitude * 3.6f;
        pitch = KPH / topSpeed;

        audioSource = transform.GetComponents<AudioSource>();
        foreach (AudioSource item in audioSource)
        {
            item.pitch = pitch;
        }

    }

}
