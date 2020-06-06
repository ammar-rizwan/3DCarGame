using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpponentCarGenerator1 : MonoBehaviour
{
     float[] Lanes = new float[4];

    public GameObject oppoCar;

    public Transform objectToFollow;
    public float number;
    public Vector3 offset = new Vector3(0f, 0f, 20f);
    int gameMode;
    void Start()
    {

        objectToFollow = GameObject.FindGameObjectWithTag("Player").transform;
        gameMode = PlayerPrefs.GetInt("level");
        Lanes[0] = -2.5f;
        Lanes[1] = -1f;
        Lanes[2] = 1f;
        Lanes[3] = 2.5f;

        if (gameMode == 1)
        {
            InvokeRepeating("GenerateTraffic", 2f, 1.3f);
            InvokeRepeating("GenerateTraffic", 6f, 2f);


        }
        else if (gameMode == 2)
        {
            InvokeRepeating("GenerateTraffic", 2f, 1.2f);
            InvokeRepeating("GenerateWrongWayTraffic", 2f, 1.5f);
        }
        else
        {
            Debug.Log("Error");
        }
    }


    private void GenerateTraffic()
    {
        int number =0;
        if (gameMode == 1)
            number = Random.Range(0, 4);
        else if (gameMode == 2)
            number = Random.Range(2, 4);

        Debug.Log("Number "+number);

        Instantiate(oppoCar, new Vector3(Lanes[number], 0f, transform.position.z), Quaternion.Euler(0f, 0f, 0f));

    }
    private void GenerateWrongWayTraffic()
    {
        int number1 = Random.Range(0,2);

        Instantiate(oppoCar, new Vector3(Lanes[number1], 0f, transform.position.z), Quaternion.Euler(0f, 180f, 0f));

    }
    void FixedUpdate()
    {

        //      Vector3 _targetPos = objectToFollow.position + 
        //					 objectToFollow.forward * offset.z + 
        //					 //objectToFollow.right * offset.x + 
        //					 objectToFollow.up * offset.y;
        //transform.position = Vector3.Lerp(transform.position, _targetPos, 10f * Time.deltaTime);
        transform.position = new Vector3(transform.position.x, 0f, objectToFollow.position.z + offset.z);
    }
}
