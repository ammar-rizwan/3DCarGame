using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation : MonoBehaviour
{
    [SerializeField] Vector3 finalPosition;
    Vector3 initialPosition;
    void Awake()
    {
        initialPosition = transform.position;
    }
    // Start is called before the first frame update
    void OnDisable()
    {
        transform.position = initialPosition;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, finalPosition, 0.1f);
    }
}
