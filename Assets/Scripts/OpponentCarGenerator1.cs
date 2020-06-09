using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpponentCarGenerator1 : MonoBehaviour
{
     float[] Lanes = new float[4];

    public GameObject PlayerCar;
    public CarController RR;
    public GameObject [] opponentCar;

    public Transform objectToFollow;
    public float number;
    public Vector3 offset = new Vector3(0f, 0f, 30f);
    int gameMode;
     public float speedF=3;

    void Start()
    {
        PlayerCar = GameObject.FindGameObjectWithTag("Player");
        RR = PlayerCar.GetComponent<CarController>();

        objectToFollow = PlayerCar.transform;

        gameMode = PlayerPrefs.GetInt("level");
        Lanes[0] = -1.8f;
        Lanes[1] = -0.6f;
        Lanes[2] = 0.6f;
        Lanes[3] = 1.8f;

        StartCoroutine(TrafficDensityCheck());

        // if (gameMode == 1)
        // {
        //     InvokeRepeating("GenerateTraffic", 2f, 1.3f);
        //     InvokeRepeating("GenerateTraffic", 6f, 2f);

        // }
        // else if (gameMode == 2)
        // {
        //     InvokeRepeating("GenerateTraffic", 2f, 1.2f);
        //     InvokeRepeating("GenerateWrongWayTraffic", 2f, 1.5f);
        // }
    }


    IEnumerator TrafficDensityCheck()
    {
        while (true)
        {
            if (RR.KPH > 20)
            {
            if (gameMode == 1)
            {    
                GenerateTraffic();
            }else if(gameMode ==2){
                GenerateTraffic();
                GenerateWrongWayTraffic();

            }
            }
            yield return new WaitForSeconds(speedF);
            
        }

    }
    

    private void GenerateTraffic()
    {
        int number =0;
        if (gameMode == 1)
            number = Random.Range(0, 4);
        else if (gameMode == 2)
            number = Random.Range(2, 4);

    int ramdomcar = Random.Range(0,4);
        Debug.Log("Number "+number);
        GameObject oppo =  Instantiate(opponentCar[ramdomcar], new Vector3(Lanes[number], 0f, transform.position.z), Quaternion.Euler(0f, 0f, 0f)) ;
        EnemyCarDriver CD = oppo.GetComponent<EnemyCarDriver>();
    }
    private void GenerateWrongWayTraffic()
    {
        int number1 = Random.Range(0,2);
        int ramdomcar = Random.Range(0,4);

        Instantiate(opponentCar[ramdomcar], new Vector3(Lanes[number1], 0f, transform.position.z), Quaternion.Euler(0f, 180f, 0f));

    }
    void FixedUpdate()
    {
        transform.position = new Vector3(transform.position.x, 0f, objectToFollow.position.z + offset.z);
    }
}
