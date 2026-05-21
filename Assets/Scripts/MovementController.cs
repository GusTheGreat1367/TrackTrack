using UnityEngine;
using System;
using UnityEngine.InputSystem;
using Unity.VisualScripting;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;


namespace Movement
{
    public class MovementController : MonoBehaviour
    {
        public InputAction LeftArrowOrA; // turn Left
        public InputAction RightArrowOrD; // turn Right
        public InputAction UpArrowOrW; // Acceleration
        public InputAction DownArrowOrS; // Deceleration
        public InputAction CameraChange;
        public InputAction resetLap;
        public InputAction esc;
        public float speed = 0f;
        public bool ThirdPerson = true;
        public float turnSpeed = 0f; // change to the turn speed afer testing
        public InputSystem_Actions input;
        public TMP_Text speedPer;
        public TMP_Text turnPer;
        public TMP_Text timer;
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
            esc = input.Player.Esc;
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
            esc.Enable();
        }
        public void OnDisable()
        {
            LeftArrowOrA.Disable();
            RightArrowOrD.Disable();
            UpArrowOrW.Disable();
            DownArrowOrS.Disable();
            CameraChange.Disable();
            resetLap.Disable();
            esc.Disable();
        }
        public AnimationCurve accelerationCurve;
        public AnimationCurve turnCurve;
        public AnimationCurve coastCurve;
        float maxSpeed = 100f;
        float acceleration = 10f;
        public float brakingForce = 25f;  
        public float speedMph = 0f;
        public float time;
        public bool timerRunning = false;
        bool DownArrow = false;
        bool move = false;
        float maxTurnAngle = 0f; // 65 degrees for left, 300 for the right
        public void Update()
        {
            // make you slow down when off track
            // for lap time ending, make a raycast that detects when you cross the finish line, then stop timer and display best lap time
            // esc to return to track menu, then another button on that menu to retun to main menu 
            // car decels too fast
            // add main menu pics
            // fix coasting
            // make timer start when you start moving
            // fix reverse
            // reverse is at like 5% when you initially hold down arrow, then when you release it it jumps to like 35% 

            if(timerRunning)
            {
                time += Time.deltaTime; 
            }
            timer.text = $"{time:F2}";
            speedMph = Mathf.Lerp(0f, 275f, Mathf.InverseLerp(0f, 100f, speed));
            speedPer.text = $"{speedMph:F0} MPH & {speed:F0}% throttle";
            turnPer.text = $"Turn Speed: {turnSpeed:F0}";
            transform.position += transform.forward * speed * Time.deltaTime;
            turnSpeed = Mathf.Lerp(0f, 50f, Mathf.InverseLerp(0f, 50f, speed));
            float turnSpeedValue = turnCurve.Evaluate(turnSpeed / 50f) * 50f; 
            turnSpeed = turnSpeedValue;
            if(!DownArrow)
            {
                speedMph = Mathf.Lerp(0f, 275f, Mathf.InverseLerp(0f, 100f, speed));
            }
            if(UpArrowOrW.IsPressed()) 
            {
                DownArrow = false;
                move = true;
                float speedPercent = speed / maxSpeed; // 0 to 1
                float currentAccel = accelerationCurve.Evaluate(speedPercent) * acceleration;
                speed = Mathf.MoveTowards(speed, maxSpeed, currentAccel * Time.deltaTime);
                if(speed > 0 && transform.position != spawnPoint)
                { 
                    timerRunning = true; 
                }
            }
            if(DownArrowOrS.IsPressed())
            {
                DownArrow = true;
                move = true;
                speed = Mathf.MoveTowards(speed, 0f, brakingForce * Time.deltaTime);
                // fix reverse, decellerates too fast
            }
            if(speed <= 0 && DownArrow)
            {
                speed = Mathf.MoveTowards(speed, -50, brakingForce * Time.deltaTime);
                speedMph = Mathf.Lerp(-50f, 0f, Mathf.InverseLerp(-50f, 0f, speed));
            }
            if(RightArrowOrD.IsPressed())
            {
                if(turnSpeed > 0f)
                    move = true;
                    transform.Rotate(Vector3.right * turnSpeed * Time.deltaTime); 
                    float coastSpeed = Mathf.Lerp(1f, 50f, Mathf.InverseLerp(1f, 50f, speed));
                    float currentCoast = coastCurve.Evaluate(coastSpeed / 50f) * coastSpeed;
                    speed = Mathf.MoveTowards(speed, 0f, currentCoast * Time.deltaTime);
            }
            if(LeftArrowOrA.IsPressed()) 
            {
                if(turnSpeed > 0f)
                    move = true;
                    transform.Rotate(Vector3.left * turnSpeed * Time.deltaTime);
                    float coastSpeed = Mathf.Lerp(1f, 50f, Mathf.InverseLerp(1f, 50f, speed));
                    float currentCoast = coastCurve.Evaluate(coastSpeed / 50f) * coastSpeed;
                    speed = Mathf.MoveTowards(speed, 0f, currentCoast * Time.deltaTime);
            }
            if(resetLap.WasPressedThisFrame())
            {
                transform.position = spawnPoint;
                transform.rotation = spawnRot;
                time = 0f;
                speed = 0f;
                timerRunning = false;
            }
            if(esc.WasPressedThisFrame())
            {
                // UI menu for return to main menu, reset lap, return to track select, ect.
                timerRunning = false;
                //SceneManager.LoadScene("PlayMenu");
            }
            if(!move)
            {
                //float coastSpeed = Mathf.Lerp(1f, maxSpeed, Mathf.InverseLerp(1f, maxSpeed, speed));
                //float currentCoast = coastCurve.Evaluate(coastSpeed / maxSpeed) * coastSpeed;
                DownArrow = false;
                float currentCoast = (coastCurve.Evaluate(speed / maxSpeed) * speed) * 0.995f;
                if(speed > 0f)
                    speed -= currentCoast;
            }
        }
    }
}
