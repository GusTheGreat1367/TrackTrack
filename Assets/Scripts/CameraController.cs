using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float XOffset = 0f;
    public float YOffset = 0f;
    public float ZOffset = 0f;
    GameObject car;
    void Awake()
    {
        transform.position += new Vector3(car.transform.position.x + XOffset, car.transform.position.y + YOffset, car.transform.position.z + ZOffset);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(car.transform.position.x + XOffset, car.transform.position.y + YOffset, car.transform.position.z + ZOffset);
    }
}
