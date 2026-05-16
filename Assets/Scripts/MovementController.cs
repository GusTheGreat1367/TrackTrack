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
        public float speed = 0f;
        public float turnSpeed = 0f; // change to the turn speed afer testing
        public InputSystem_Actions input;
        public void Awake()
        {
            input = new InputSystem_Actions();
            LeftArrowOrA = input.Player.Left;
            RightArrowOrD = input.Player.Right;
            UpArrowOrW = input.Player.Accel;
            DownArrowOrS = input.Player.Deccel;
        }

        public void OnEnable()
        {
            LeftArrowOrA.Enable();
            RightArrowOrD.Enable();
            UpArrowOrW.Enable();
            DownArrowOrS.Enable();
        }
        public void OnDisable()
        {
            LeftArrowOrA.Disable();
            RightArrowOrD.Disable();
            UpArrowOrW.Disable();
            DownArrowOrS.Disable();
        }
        public void Update()
        {
            speed = Mathf.Clamp(speed, -10f, 100f);
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
            if(UpArrowOrW.IsPressed()) // Or WasPressedThisFrame()
            {
                speed += 1;
            }
            if(DownArrowOrS.IsPressed())
            {
                speed -= 1;
            }
            if(RightArrowOrD.IsPressed())
            {
                // Add Physics?
                transform.Rotate(Vector3.right * turnSpeed * Time.deltaTime);
            }
            if(LeftArrowOrA.IsPressed()) 
            {
                // Add Physics?
                transform.Rotate(Vector3.left * turnSpeed * Time.deltaTime);
                // This in MovementController.cs, add to the wheels, camera, and car
                // for the wheels, add this and the maximum rotation degrees this to make them move
                // Add the 2 camera angles
            }
        }
    }
}
