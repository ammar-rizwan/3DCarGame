using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Initialize : MonoBehaviour
{
    void Awake()
    {
        int chk = PlayerPrefs.GetInt("chk", 0);
        if (chk == 0)
        {
            PlayerPrefs.SetInt("chk", 1);
            for (int i = 0; i < transform.childCount; i++)
            {
                string temp = (transform.GetChild(i)).ToString();
                carClass obj = new carClass();
                obj.topSpeed = 100;
                obj.price = 1000;
                obj.CarID = i;
                if (i != 0)
                {
                    obj.isLocked = true;
                }
                else
                {
                    obj.isLocked = false;
                }
                string json = JsonUtility.ToJson(obj);
                PlayerPrefs.SetString(temp, json);

            }
            PlayerPrefs.SetInt("coins",2222);

        }
    }
}
