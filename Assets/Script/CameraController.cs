using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    public Transform[] targets;
    public Text PlanetText;
    public float rotationSpeed = 10.0f;
    public float zoomSpeed = 3.0f;
    public float zoomMin = 0.0f;
    public float zoomMax = 40.0f;

    private int currentTargetIndex = 0;
    private float horizontal = 0.0f;
    private float vertical = 0.0f;
    private float distance = 5.0f;
    private bool pause = false;

    private void OnEnable()
    {
        UIManager.Pause += OnPause;
    }
    void Update()
    {

        if (!pause)
        {
            horizontal += Input.GetAxis("Mouse X") * rotationSpeed;
            vertical -= Input.GetAxis("Mouse Y") * rotationSpeed;


            vertical = Mathf.Clamp(vertical, -80f, 80f);


            transform.rotation = Quaternion.Euler(vertical, horizontal, 0);

            if (Input.GetKey(KeyCode.LeftShift))
            {
                distance += Input.GetAxis("Mouse ScrollWheel") * zoomSpeed * 2;
            }

            distance += Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
            distance = Mathf.Clamp(distance, zoomMin, zoomMax);


            transform.position = targets[currentTargetIndex].position - transform.rotation * Vector3.forward * distance;
            PlanetText.text = targets[currentTargetIndex].name;

            if (Input.GetMouseButtonDown(0))
            {
                currentTargetIndex++;
                if (currentTargetIndex >= targets.Length)
                {
                    currentTargetIndex = 0;

                }
            }
        }
    }



    public void OnPause(bool pauses)
    {
        pause = pauses;
    }

    private void OnDisable()
    {
        UIManager.Pause -= OnPause;
    }
}
