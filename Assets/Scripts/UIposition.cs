using UnityEngine;

public class UIposition : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float depth = 0f; // distance in front of camera
        Vector3 screenBottomLeft = new Vector3(0, 0, depth);
        transform.position = Camera.main.ScreenToWorldPoint(screenBottomLeft);
    }
}
