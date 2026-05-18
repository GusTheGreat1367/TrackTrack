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
        public float speed = 0f;
        public bool ThirdPerson = true;
        public float turnSpeed = 0f; // change to the turn speed afer testing
        public InputSystem_Actions input;
        public TMP_Text speedPer;
        public TMP_Text turnPer;
        // then the timer
        public void Awake()
        {
            input = new InputSystem_Actions();
            LeftArrowOrA = input.Player.Left;
            RightArrowOrD = input.Player.Right;
            UpArrowOrW = input.Player.Accel;
            DownArrowOrS = input.Player.Deccel;
            CameraChange = input.Player.Camera;
        }

        public void OnEnable()
        {
            LeftArrowOrA.Enable();
            RightArrowOrD.Enable();
            UpArrowOrW.Enable();
            DownArrowOrS.Enable();
            CameraChange.Enable();
        }
        public void OnDisable()
        {
            LeftArrowOrA.Disable();
            RightArrowOrD.Disable();
            UpArrowOrW.Disable();
            DownArrowOrS.Disable();
            CameraChange.Disable();
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
                //speedMph = Mathf.Lerp(-50f, 275f, Mathf.InverseLerp(-10f, 100f, speed));
                speedMph = Mathf.Lerp(0f, 275f, Mathf.InverseLerp(0f, 100f, speed));
            }
            if(DownArrowOrS.IsPressed())
            {
                speed = Mathf.MoveTowards(speed, 0f, brakingForce * Time.deltaTime);
            }
            if(RightArrowOrD.IsPressed())
            {
                transform.Rotate(Vector3.right * turnSpeed * Time.deltaTime); // turn speed is calculated by the speed, the faster you go the slower you turn
            }
            if(LeftArrowOrA.IsPressed()) 
            {
                transform.Rotate(Vector3.left * turnSpeed * Time.deltaTime);
            }
            else
            {
                // slow down because you are coasting
            }
        }
    }
}
