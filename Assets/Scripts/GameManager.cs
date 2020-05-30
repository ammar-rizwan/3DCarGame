using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public CarController RR;
    public GameObject neeedle;

    private float startPosiziton = 220f, endPosition = -40f;
    private float desiredPosition;
    public float vehicleSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        vehicleSpeed = RR.KPH;
        updateNeedle();
    }
    public void updateNeedle()
    {
        desiredPosition = startPosiziton - endPosition;
        float temp = RR.engineRPM / 1000;
        neeedle.transform.eulerAngles = new Vector3(0, 0, (startPosiziton - temp * desiredPosition));

    }
}
