using UnityEngine;
using Movement;

public class WheelSpin : MonoBehaviour
{
    MovementController carMove;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    float speed;
    public int wheelsped = 500;
    public bool shouldMove = false;
    void Start()
    {
        carMove = GetComponentInParent<MovementController>();
        speed = carMove.speed;
    }

    // Update is called once per frame
    void Update()
    {
        speed = carMove.speed; 
        if(speed != 0)
        {
            transform.Rotate(speed * Time.deltaTime * wheelsped, 0, 0);
        }
        if(shouldMove)
        {
            // the wheel turns
            // only front left/right wheels
        }
    }
}
