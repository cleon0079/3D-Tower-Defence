using UnityEngine;

public class CameraMove : MonoBehaviour
{
    // Mouse moving horizontal and vertical speed
    [SerializeField] float speed = 60;

    // Mouse scrollwheel moving speed
    [SerializeField] float mouseSpeed = 500;

    void Update()
    {
        // Get the control
        float hori = Input.GetAxis("Horizontal");
        float verti = Input.GetAxis("Vertical");
        float scrollWheel = Input.GetAxis("Mouse ScrollWheel");

        // Moving the camera around
        transform.Translate(new Vector3(hori * speed, scrollWheel * mouseSpeed, verti * speed) * Time.deltaTime, Space.World);
    }
}