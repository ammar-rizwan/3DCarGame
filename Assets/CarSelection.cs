using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CarSelection : MonoBehaviour
{
    [SerializeField] Button next;
    [SerializeField] Button previous;
    public GameObject buyB;
    public GameObject selectB;
    public GameObject lockImg;
    private int currentCar;
    public Text MoneyTxt;
    public Text price;

    public Text Selected;
    public int SelectedCarInt;
    private void Awake()
    {
        MoneyTxt.text=(PlayerPrefs.GetInt("coins")).ToString();

        if (PlayerPrefs.HasKey("currentCar"))
            SelectedCarInt = PlayerPrefs.GetInt("currentCar", currentCar);
        else
            SelectedCarInt = 0;

        print(SelectedCarInt);
        SelectCar(0);
        /*PlayerPrefs.SetInt("Car2 (UnityEngine.Transform)", 1);
        PlayerPrefs.SetInt("SportCar2 (UnityEngine.Transform)", 1);//unlocked 
        PlayerPrefs.SetInt("Model_Cars_SUV (UnityEngine.Transform)", 0);//locked
        PlayerPrefs.SetInt("Sedan1 (UnityEngine.Transform)", 1);
        */

    }
    private void SelectCar(int _index)
    {

        previous.interactable = (_index != 0);
        next.interactable = (_index != transform.childCount-1);
        Debug.Log(transform.GetChild(_index));
        string nme = (transform.GetChild(_index)).ToString();
        string p = PlayerPrefs.GetString(nme);
        if (p != null && p.Length > 0)
        {
            carClass obj = JsonUtility.FromJson<carClass>(p);
            price.text = (obj.price).ToString();
            if (obj.isLocked == false)
            {

                lockImg.SetActive(false);
                buyB.SetActive(false);

                if (obj.CarID == SelectedCarInt)
                {
                    Selected.gameObject.SetActive(true);
                    selectB.SetActive(false);
                }
                else
                {
                selectB.SetActive(true);
                    Selected.gameObject.SetActive(false);
                }
            }
            else
            {
                Selected.gameObject.SetActive(false);
                lockImg.SetActive(true);
                if (PlayerPrefs.GetInt("coins") >= obj.price)
                {
                    buyB.GetComponent<Button>().interactable = true;
                }
                else
                {
                    buyB.GetComponent<Button>().interactable = false;
                }

                selectB.SetActive(false);
                buyB.SetActive(true);

            }
        }
/*        if (PlayerPrefs.GetInt(nme,0)==1)
        {
            lockImg.SetActive(false);
            selectB.SetActive(true);
            buyB.SetActive(false);
        }
        else
        {
            lockImg.SetActive(true);
            selectB.SetActive(false);
            buyB.SetActive(true);
            
        }*/
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(i ==_index);
            transform.GetChild(i).gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
        }
        
    }
    public void ChangeCar(int _change)
    {
        currentCar += _change;
        SelectCar(currentCar);
        
    }
    public void ClickedSelect()
    {
        PlayerPrefs.SetInt("currentCar", currentCar);
        SelectedCarInt = currentCar;
        SelectCar(SelectedCarInt);
    }

    public void ClickedBuy()
    {
        int coins=PlayerPrefs.GetInt("coins");
        int price=0;
        string nme= (transform.GetChild(currentCar)).ToString();
        string p = PlayerPrefs.GetString(nme);
        if (p != null && p.Length > 0)
        {
            carClass obj = JsonUtility.FromJson<carClass>(p);

            price = obj.price;
            obj.isLocked = false;
            string json = JsonUtility.ToJson(obj);
            PlayerPrefs.SetString(nme, json);
        }

        PlayerPrefs.SetInt("coins",coins-price);
        MoneyTxt.text = (PlayerPrefs.GetInt("coins")).ToString();
        lockImg.SetActive(false);
        selectB.SetActive(true);
        Debug.Log(PlayerPrefs.GetInt("coins"));
        buyB.SetActive(false);

    }

    public void PlayButtonClick()
    {
        SceneManager.LoadScene(1);
    }
}
