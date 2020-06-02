using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{

    public CarController RR;
    public GameObject neeedle;
    public Text TimeText;
    public Text ScoreText;
    public Text SpeedText;
    //public Text ScoreText;

    private string currentTime;

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
        if(RR.gameObject.active == false)
        {
            return;
        }

        currentTime = Time.time.ToString("f1");
        currentTime = currentTime + " sec.";
        TimeText.text = currentTime;
        SpeedText.text = vehicleSpeed.ToString("f1")+"KM/H";
    }
    public void updateNeedle()
    {
        desiredPosition = startPosiziton - endPosition;
        float temp = vehicleSpeed/ 180;
        neeedle.transform.eulerAngles = new Vector3(0, 0, (startPosiziton - temp * desiredPosition));
        //Debug.Log((startPosiziton - temp * desiredPosition));
    }
}
