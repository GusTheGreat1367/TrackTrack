using UnityEngine;
using System;
using UnityEngine.InputSystem;
using Unity.VisualScripting;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;


namespace Movement
{
    public class CarMove : MonoBehaviour
    {
        public InputAction LeftArrowOrA; // turn Left
        public InputAction RightArrowOrD; // turn Right
        public InputAction UpArrowOrW; // Acceleration
        public InputAction DownArrowOrS; // Deceleration
        public InputAction CameraChange;
        public InputAction resetLap;
        public float speed = 0f;
        public bool ThirdPerson = true;
        public float turnSpeed = 0f; // change to the turn speed afer testing
        public InputSystem_Actions input;
        public TMP_Text speedPer;
        public TMP_Text turnPer;
        Vector3 spawnPoint;
        Quaternion spawnRot;
        // lap time
        public void Awake()
        {
            input = new InputSystem_Actions();
            LeftArrowOrA = input.Player.Left;
            RightArrowOrD = input.Player.Right;
            UpArrowOrW = input.Player.Accel;
            DownArrowOrS = input.Player.Deccel;
            CameraChange = input.Player.Camera;
            resetLap = input.Player.Reset;
            spawnPoint = transform.position;
            spawnRot = transform.rotation;
        }

        public void OnEnable()
        {
            LeftArrowOrA.Enable();
            RightArrowOrD.Enable();
            UpArrowOrW.Enable();
            DownArrowOrS.Enable();
            CameraChange.Enable();
            resetLap.Enable();
        }
        public void OnDisable()
        {
            LeftArrowOrA.Disable();
            RightArrowOrD.Disable();
            UpArrowOrW.Disable();
            DownArrowOrS.Disable();
            CameraChange.Disable();
            resetLap.Disable();
        }
        public AnimationCurve accelerationCurve;
        public AnimationCurve turnCurve;
        float maxSpeed = 100f;
        float acceleration = 10f;
        public float brakingForce = 25f;  
        public float speedMph = 0f;
        float maxTurnAngle = 0f; // 65 degrees for left, 300 for the right
        public void Update()
        {
            // Add the timer and restart buttons
            // make reverse/slow down work
            // make you slow down when off track
            // make you slow down when you turn
            // make you slow down when you aren't accelerating or decelerating- slow when coasting
            // for lap time ending, make a raycast that detects when you cross the finish line, then stop timer

            speedMph = Mathf.Lerp(0f, 275f, Mathf.InverseLerp(0f, 100f, speed));
            speedPer.text = $"{speedMph:F0} MPH & {speed:F0}% throttle";
            turnPer.text = $"Turn Speed: {turnSpeed:F0}";
            transform.position += transform.forward * speed * Time.deltaTime;
            turnSpeed = Mathf.Lerp(0f, 50f, Mathf.InverseLerp(0f, 50f, speed));
            float turnSpeedValue = turnCurve.Evaluate(turnSpeed / 50f) * 50f; 
            turnSpeed = turnSpeedValue;
            if(UpArrowOrW.IsPressed()) 
            {
                float speedPercent = speed / maxSpeed; // 0 to 1
                float currentAccel = accelerationCurve.Evaluate(speedPercent) * acceleration;
                speed = Mathf.MoveTowards(speed, maxSpeed, currentAccel * Time.deltaTime);
                // timer start
            }
            if(DownArrowOrS.IsPressed())
            {
                speed = Mathf.MoveTowards(speed, 0f, brakingForce * Time.deltaTime);
            }
            if(RightArrowOrD.IsPressed())
            {
                if(turnSpeed > 0f)
                    transform.Rotate(Vector3.right * turnSpeed * Time.deltaTime); 
                    speed = Mathf.MoveTowards(speed, 0f, (acceleration / 6f) * Time.deltaTime); // make it less linear
            }
            if(LeftArrowOrA.IsPressed()) 
            {
                if(turnSpeed > 0f)
                    transform.Rotate(Vector3.left * turnSpeed * Time.deltaTime);
                    speed = Mathf.MoveTowards(speed, 0f, (acceleration / 6f) * Time.deltaTime); // make it less linear
            }
            if(resetLap.WasPressedThisFrame())
            {
                transform.position = spawnPoint;
                transform.rotation = spawnRot;
                // timer reset
                speed = 0f;
            }
            else
            {
                // slow down because you are coasting
                speed = Mathf.MoveTowards(speed, 0f, (acceleration / 4f) * Time.deltaTime); // maybe make it less linear?
            }
        }
    }
}
