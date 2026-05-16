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
        public TMP_Text speedPercent;
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
        public void Update()
        {
            // Add the timer and restart buttons
            // Fix the turning and add physics
            // Fix Acceleration and Deceleration and add physics
            // Fix the UI for laptimes and speedometer
            transform.position += transform.forward * speed * Time.deltaTime;
            speedPercent.text = $"Throttle: {Mathf.RoundToInt(speed).ToString()}%";
            if(UpArrowOrW.IsPressed()) // Or WasPressedThisFrame()
            {
                speed += 1;
                if(speed > 100)
                {
                    speed = 100;
                }
            }
            if(DownArrowOrS.IsPressed())
            {
                speed -= 1;
                if(speed < -10)
                {
                    speed = -10;
                }
            }
            if(RightArrowOrD.IsPressed())
            {
                // Add Physics?
                transform.Rotate(Vector3.right * turnSpeed * Time.deltaTime); // turn speed is calculated by the speed, the faster you go the slower you turn
            }
            if(LeftArrowOrA.IsPressed()) 
            {
                // Add Physics?
                transform.Rotate(Vector3.left * turnSpeed * Time.deltaTime);
            }
        }
    }
}
