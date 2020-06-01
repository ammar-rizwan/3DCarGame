using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarRaycaster : MonoBehaviour
{
    float radius = 1f;
    bool DidRightHit = false, DidLeftHit = false;
    RaycastHit raycastHit;
    int score = 0;
    public Text scoreText;
    // Update is called once per frame
    void FixedUpdate()
    {
        Ray RightRay = new Ray(transform.position, transform.TransformDirection(Vector3.right));
        Ray LeftRay = new Ray(transform.position, transform.TransformDirection(Vector3.left));

        DidRightHit = Physics.SphereCast(RightRay, radius, out raycastHit, 50);
        DidLeftHit = Physics.SphereCast(LeftRay, radius, out raycastHit, 50);

        //Debug.Log(DidHit);

        if (DidRightHit || DidLeftHit)
        {
            if (raycastHit.collider.tag == "Opponent")
            {
                score++;
                scoreText.text = "Score: "+score.ToString();
            }
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.left) * 50, Color.yellow);
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.left) * 50, Color.yellow);
        }
    }
}
