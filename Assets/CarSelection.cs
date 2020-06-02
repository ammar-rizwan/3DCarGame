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
    
    private void Awake()
    {
        SelectCar(0);
        PlayerPrefs.SetInt("Car2 (UnityEngine.Transform)", 1);
        PlayerPrefs.SetInt("SportCar2 (UnityEngine.Transform)", 1);//unlocked 
        PlayerPrefs.SetInt("Model_Cars_SUV (UnityEngine.Transform)", 0);//locked
        PlayerPrefs.SetInt("Sedan1 (UnityEngine.Transform)", 1);

    }
    private void SelectCar(int _index)
    {

        previous.interactable = (_index != 0);
        next.interactable = (_index != transform.childCount-1);
        Debug.Log(transform.GetChild(_index));
        string nme = (transform.GetChild(_index)).ToString();
        if (PlayerPrefs.GetInt(nme,0)==1)
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

        }
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
        //PlayerPrefs.SetString("currentCar", transform.GetChild(currentCar).ToString());
        PlayerPrefs.SetInt("currentCar", currentCar);
        Debug.Log("car selected==>" + PlayerPrefs.GetInt("currentCar"));
        SceneManager.LoadScene(1);

    }
    public void ClickedBuy()
    {

    }
}
