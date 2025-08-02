using UnityEngine;

public class LookCameraUI : MonoBehaviour
{
    private Camera mainCamera;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (mainCamera != null)
        {
            Vector3 lookDirection = mainCamera.transform.forward;
            lookDirection.y = 0; // Keep upright, ignore vertical tilt
            if (lookDirection != Vector3.zero)
            {
                transform.forward = lookDirection;
            }
        }
    }
}
