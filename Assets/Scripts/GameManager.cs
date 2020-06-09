using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{

    public CarController RR;
    public CarRaycaster CR;
    public GameObject[] Cars;
    private string currentTime;
    public float vehicleSpeed;


    public GameObject GamePlayUI;
    public Text TimeText;
    public Text ScoreText;
    public Text SpeedText;
    public Text DistanceText;


    public GameObject GameOverUI;
    public Text TotalDistanceText;
    public Text TotalDistanceMoney;
    public Text NearMissText;
    public Text NearMissMoney;
    public Text CashCollectedText;
    public Text CashCollectedMoney;



    public Sprite PauseSprite;
    public Sprite PlaySprite;    

    public Material Day;
    public Material Night;

    public Text TotalScore;
    private bool isDaySelected;

    // private float startPosiziton = 220f, endPosition = -40f;
    // private float desiredPosition;

    // Start is called before the first frame update
    void Start()
    {
        isDaySelected =(PlayerPrefs.GetInt("daySelected") == 1) ? true:false; 
        if(isDaySelected){
            RenderSettings.skybox = Day;
        }else{
            RenderSettings.skybox = Night;
    }


        int temp = PlayerPrefs.GetInt("currentCar");
        // int temp = 3;
        Instantiate(Cars[temp], new Vector3(0f, 0.5f, -23f), Quaternion.Euler(0f, 0f, 0f));
        RR = GameObject.FindGameObjectWithTag("Player").GetComponent<CarController>();
        CR = GameObject.FindGameObjectWithTag("Player").GetComponent<CarRaycaster>();

        // GamePlayUI = GameObject.FindGameObjectsWithTag("UITag")[0];
        // GameOverUI = GameObject.FindGameObjectsWithTag("UITag")[1];
         

        GameOverUI.gameObject.SetActive(false);
        // gameOverText = gameUI.gameObject.GetComponent<Text>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        vehicleSpeed = RR.KPH;
        if(RR.gameObject.active == false)
        {
            GameOverUI.gameObject.SetActive(true);
            GamePlayUI.gameObject.SetActive(false);

            TotalDistanceText.text = (RR.distanceTravelled/1000).ToString("f2");
            TotalDistanceMoney.text = "$"+(RR.distanceTravelled/1000)*10;
            NearMissText.text = CR.nearMisses.ToString();
            NearMissMoney.text = (CR.nearMisses*10).ToString();
            TotalScore.text = CR.score.ToString("f1");


            return;
        }

        currentTime = Time.time.ToString("f1");
        TimeText.text = currentTime + " Sec.";
        SpeedText.text = vehicleSpeed.ToString("f1")+"KM/H";
        DistanceText.text = (RR.distanceTravelled/1000).ToString("f2") + "Kms";
    }
    // public void updateNeedle()
    // {
    //     desiredPosition = startPosiziton - endPosition;
    //     float temp = vehicleSpeed/ 180;
    //     neeedle.transform.eulerAngles = new Vector3(0, 0, (startPosiziton - temp * desiredPosition));
    //     //Debug.Log((startPosiziton - temp * desiredPosition));
    // }

    public void PToggle(Button button)
    {
        if (Time.timeScale != 0)
        {
            button.GetComponent<Image>().sprite = PlaySprite;
            Time.timeScale = 0;

            // gameUI.transform.GetChild(0).gameObject.SetActive(true);
            // gameUI.transform.GetChild(1).gameObject.SetActive(true);
            return;
        }
        else
        {
            button.GetComponent<Image>().sprite = PauseSprite;
            // gameUI.transform.GetChild(0).gameObject.SetActive(false);
            // gameUI.transform.GetChild(1).gameObject.SetActive(false);

            Time.timeScale = 1;

        }
    }
    public void OnRestarButtonClick()
    {
        SceneManager.LoadScene(2);
    }
    public void OnMainMenuButton()
    {
        SceneManager.LoadScene(0);
    }

    public void onChnageController(){
        if(PlayerPrefs.GetInt("tilt")==1){
            PlayerPrefs.SetInt("tilt",0);

        }else{
            PlayerPrefs.SetInt("tilt",1);

        }
    }
/*    void Update()
    {
        // currentSpeed = transform.GetComponent<Rigidbody>().velocity.magnitude * 3.6f;
        pitch = vehicleSpeed / topSpeed;

        audioSource =transform.GetComponents<AudioSource>();
        foreach (AudioSource item in audioSource)
        {
            item.pitch = pitch;
        }
        
    }*/



}
