using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class InputManager:MonoBehaviour
{
    public float vertical;
    public float horizontal;
    public bool Brake;

    private void FixedUpdate()
    {
        vertical = 1f;
        Brake = (Input.GetAxis("Jump")!=0) ? true : false;

        if(PlayerPrefs.GetInt("tilt")==1){
            horizontal = Input.acceleration.x;

        }else{
           horizontal = CrossPlatformInputManager.GetAxis("Horizontal");
        }
        //vertical = CrossPlatformInputManager.GetAxis("Vertical");
    }

}
