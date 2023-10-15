using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamController : MonoBehaviour
{
    [SerializeField] private float zoomSpeed;
    [SerializeField] private float minZoomValue;
    [SerializeField] private float maxZoomValue;

    private void Start()
    {
        Cursor.visible = false;
    }
    private void LateUpdate()
    {
        var zoom = Input.GetAxis("Mouse ScrollWheel");

        Camera.main.fieldOfView += zoom * -zoomSpeed;
        var clamp = Mathf.Clamp(Camera.main.fieldOfView, minZoomValue, maxZoomValue);

        Camera.main.fieldOfView = clamp;
    }

}
