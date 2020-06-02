using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{

    public CarController RR;
    public GameObject neeedle;
    public Text TimeText;
    public Text ScoreText;
    public Text SpeedText;
    public Text DistanceText;
    //public Text ScoreText;
    public Sprite PauseSprite;
    public Sprite PlaySprite;
   
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
        TimeText.text = currentTime + " Sec.";
        SpeedText.text = vehicleSpeed.ToString("f1")+"KM/H";
        DistanceText.text = (RR.distanceTravelled/1000).ToString("f2") + "Kms";
    }
    public void updateNeedle()
    {
        desiredPosition = startPosiziton - endPosition;
        float temp = vehicleSpeed/ 180;
        neeedle.transform.eulerAngles = new Vector3(0, 0, (startPosiziton - temp * desiredPosition));
        //Debug.Log((startPosiziton - temp * desiredPosition));
    }

    public void PToggle(Button button)
    {
        if (Time.timeScale != 0)
        {
            button.GetComponent<Image>().sprite = PlaySprite;
            Time.timeScale = 0;
            return;
        }
        else
        {
            button.GetComponent<Image>().sprite = PauseSprite;
            Time.timeScale = 1;
        }
    }
    public void OnRestarButtonClick()
    {

        SceneManager.LoadScene(Application.loadedLevel);

    }

}
