using System;
using TMPro;
using UnityEngine;

public class CarController : MonoBehaviour
{
    [SerializeField] private Transform centerOfMass;
    [SerializeField] private TextMeshProUGUI carSpeedText;
    [SerializeField] private ParticleSystem pS;
    [SerializeField] private AudioSource driving, bumping;
    public float motorTorque, brakeTorque, maxSpeed, steeringRange, steeringRangeAtMaxSpeed;
    public Transform playerTransform;
    private GoIntoCar gic;

    WheelControl[] wheels;
    Rigidbody rigidBody;
    private void Start()
    {
        gic = FindAnyObjectByType<GoIntoCar>();
        rigidBody = GetComponent<Rigidbody>();
        
        rigidBody.centerOfMass = centerOfMass.localPosition;
        
        wheels = GetComponentsInChildren<WheelControl>();
        
        driving.volume = 0;
    }
    
    private void Update()
    {

        if (!gic._inCar)
        {
            foreach (var wheel in wheels) { wheel.WheelCollider.brakeTorque = brakeTorque; }

            return;
        }
        
        float vInput = Input.GetAxis("Vertical");
        float hInput = Input.GetAxis("Horizontal");

        float forwardSpeed = Vector3.Dot(transform.forward, rigidBody.linearVelocity);
        carSpeedText.text = Math.Round(forwardSpeed * 2).ToString();
        
        driving.volume = Mathf.InverseLerp(0, maxSpeed,  Mathf.Abs(forwardSpeed));
        if (driving.volume < 0.2f) driving.volume = 0.2f;
        bumping.volume = Mathf.InverseLerp(0.7f, maxSpeed,  Mathf.Abs(forwardSpeed))* 2;
        var m = pS.main;
        m.startLifetime = (Mathf.InverseLerp(0, maxSpeed,  Mathf.Abs(forwardSpeed))+ 1) *4;
        m.startSpeed = (Mathf.InverseLerp(0, maxSpeed,  Mathf.Abs(forwardSpeed)) + 1) * 6 ;

        float speedFactor = Mathf.InverseLerp(0, maxSpeed, forwardSpeed);
        float currentMotorTorque = Mathf.Lerp(motorTorque, 0, speedFactor);
        float currentSteerRange = Mathf.Lerp(steeringRange, steeringRangeAtMaxSpeed, speedFactor);
        bool isAccelerating = Mathf.Approximately(Mathf.Sign(vInput), Mathf.Sign(forwardSpeed));
        foreach (var wheel in wheels)
        {
            if (wheel.steerable) { wheel.WheelCollider.steerAngle = hInput * currentSteerRange; }
            
            if (isAccelerating)
            {
                if (wheel.motorized) { wheel.WheelCollider.motorTorque = vInput * currentMotorTorque; }
                wheel.WheelCollider.brakeTorque = 0;
            }
            else
            {
                wheel.WheelCollider.brakeTorque = Mathf.Abs(vInput) * brakeTorque;
                wheel.WheelCollider.motorTorque = 0;
            }
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("obsticle"))
        {
            print(other.gameObject.name);
            bumping.Play();
        }
    }
}
