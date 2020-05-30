using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager:MonoBehaviour
{
    public float vertical;
    public float horizontal;
    public bool Brake;

    private void FixedUpdate()
    {
        vertical = Input.GetAxis("Vertical");
        horizontal = Input.GetAxis("Horizontal");
        Brake = (Input.GetAxis("Jump")!=0) ? true : false;
    }

}
