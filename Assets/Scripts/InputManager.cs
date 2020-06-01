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
        //vertical = CrossPlatformInputManager.GetAxis("Vertical");
        vertical = 1f;
        horizontal = CrossPlatformInputManager.GetAxis("Horizontal");
        Brake = (Input.GetAxis("Jump")!=0) ? true : false;
    }

}
