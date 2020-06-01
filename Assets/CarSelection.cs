using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarSelection : MonoBehaviour
{
    [SerializeField] Button next;
    [SerializeField] Button previous;
    private int currentCar;
    private void Awake()
    {
        SelectCar(0);
    }
    private void SelectCar(int _index)
    {

        previous.interactable = (_index != 0);
        next.interactable = (_index != transform.childCount-1);
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(i ==_index);
        }
        
    }

    // Update is called once per frame
    public void ChangeCar(int _change)
    {
        Debug.Log("ammar");
        currentCar += _change;
        SelectCar(currentCar);
        
    }
}
