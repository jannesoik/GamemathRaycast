using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleRotator : MonoBehaviour
{
    [SerializeField] private float xSpeed = 0f;
    [SerializeField] private float ySpeed = 0f;
    [SerializeField] private float zSpeed = 0f;

    private Vector3 currentRotation;

    void Start()
    {
        currentRotation = transform.localEulerAngles;
    }

    void Update()
    {
        currentRotation += new Vector3(xSpeed, ySpeed, zSpeed) * Time.deltaTime;
        transform.localEulerAngles = currentRotation;
    }
}
