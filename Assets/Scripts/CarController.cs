using System;
using TMPro;
using Unity.Mathematics;
using UnityEngine;

public class CarController : MonoBehaviour
{
    [SerializeField] private Transform centerOfMass;
    [SerializeField] private TextMeshProUGUI carSpeedText;
    public float motorTorque = 2000;
    public float brakeTorque = 2000;
    public float maxSpeed = 30;
    public float steeringRange = 30;
    public float steeringRangeAtMaxSpeed = 25;

    WheelControl[] wheels;
    Rigidbody rigidBody;
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        
        rigidBody.centerOfMass = centerOfMass.localPosition;
        
        wheels = GetComponentsInChildren<WheelControl>();
    }
    
    void Update()
    {
        float vInput = Input.GetAxis("Vertical");
        float hInput = Input.GetAxis("Horizontal");

        float forwardSpeed = Vector3.Dot(transform.forward, rigidBody.linearVelocity);
        carSpeedText.text = Math.Round(forwardSpeed * 2).ToString();
        float speedFactor = Mathf.InverseLerp(0, maxSpeed, forwardSpeed);
        float currentMotorTorque = Mathf.Lerp(motorTorque, 0, speedFactor);
        float currentSteerRange = Mathf.Lerp(steeringRange, steeringRangeAtMaxSpeed, speedFactor);
        bool isAccelerating = Mathf.Sign(vInput) == Mathf.Sign(forwardSpeed);

        foreach (var wheel in wheels)
        {
            if (wheel.steerable)
            {
                wheel.WheelCollider.steerAngle = hInput * currentSteerRange;
            }
            
            if (isAccelerating)
            {
                if (wheel.motorized)
                {
                    wheel.WheelCollider.motorTorque = vInput * currentMotorTorque;
                }
                wheel.WheelCollider.brakeTorque = 0;
            }
            else
            {
                wheel.WheelCollider.brakeTorque = Mathf.Abs(vInput) * brakeTorque;
                wheel.WheelCollider.motorTorque = 0;
            }
        }
    }
}