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
        
    }

}
