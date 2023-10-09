using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamController : MonoBehaviour
{
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float minRotaionY;
    [SerializeField] private float maxRotationY;
    private float mouseY;
    private float mouseX;

    private void Start()
    {
        Cursor.visible = false;
    }
    private void LateUpdate()
    {  
        Rotate();
    }

    private void Rotate()
    {
        mouseY += -Input.GetAxis("Mouse Y") * rotationSpeed;
        mouseX += Input.GetAxis("Mouse X") * rotationSpeed;
        mouseY = Mathf.Clamp(mouseY, minRotaionY, maxRotationY);

        Vector3 rotation = Vector3.zero;
        rotation.y = mouseX;
        rotation.x = mouseY;
        Quaternion targetRotation = Quaternion.Euler(rotation);
        transform.rotation = targetRotation;
    }
}
