using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    public float XOffset = 0f;
    public float YOffset = 3f;
    public float ZOffset = -5f;
    public GameObject car;
    public InputAction CameraChange;
    public InputSystem_Actions input;
    bool TP = false;
    void Awake()
    {
        input = new InputSystem_Actions();
        CameraChange = input.Player.Camera;
        transform.position = new Vector3(car.transform.position.x, car.transform.position.y, car.transform.position.z);
    }
    public void OnEnable()
    {
        CameraChange.Enable();
    }
    public void OnDisable()
    {
        CameraChange.Disable();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(CameraChange.WasPressedThisFrame())
        {
            if(TP)
            {
                TP = false;
            }
            else
            {
                TP = true;
            }
        }
        if(TP)
        {
            transform.position = new Vector3(car.transform.position.x + XOffset, car.transform.position.y + YOffset, car.transform.position.z + ZOffset);
            transform.rotation = Quaternion.Euler(25, 0, 0);
        }
        else
        {
            transform.position = new Vector3(car.transform.position.x, car.transform.position.y, car.transform.position.z);
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
