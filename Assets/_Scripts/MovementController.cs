using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{

    [SerializeField]
    float speed = 5f;

    [SerializeField]
    float lookSensitivity = 3f;

    public GameObject fpsCamera;

    private Vector3 velocity = Vector3.zero;
    private Vector3 rotation = Vector3.zero;

    private float cameraUpDownRotation = 0f;
    private float currentCameraUpDownRotation = 0f;


    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        /*
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
        */
    }

    void Update()
    {
        // 水平移動
        float xm = Input.GetAxis("Horizontal");
        float ym = Input.GetAxis("Vertical");

        Vector3 moveH = transform.right * xm;
        Vector3 moveV = transform.forward * ym;

        Vector3 moveVel = (moveH + moveV).normalized * speed;

        Move(moveVel);

        // 水平旋轉
        float yRot = Input.GetAxis("Mouse X");
        Vector3 rotVec = new Vector3(0, yRot, 0) * lookSensitivity;

        Rotate(rotVec);

        // 攝影機上下Look
        float cameraUpDownRotation = Input.GetAxis("Mouse Y") * lookSensitivity;
        RotateCamera(cameraUpDownRotation);

    }

    private void FixedUpdate()
    {
        if (velocity != Vector3.zero)
        {
            rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
        }
        rb.MoveRotation(rb.rotation * Quaternion.Euler(rotation));

        if (fpsCamera != null)
        {
            currentCameraUpDownRotation -= cameraUpDownRotation;
            // 避免轉到背後
            currentCameraUpDownRotation = Mathf.Clamp(currentCameraUpDownRotation, -85, 85);

            fpsCamera.transform.localEulerAngles = new Vector3(currentCameraUpDownRotation, 0, 0);
        }

    }

    private void Move(Vector3 vec)
    {
        velocity = vec;
    }

    private void Rotate(Vector3 rot)
    {
        rotation = rot;
    }

    private void RotateCamera(float upDownRotation)
    {
        cameraUpDownRotation = upDownRotation;
    }



}