using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerCam : MonoBehaviour
{
    public float sensX;
    public float sensY;

    public Transform orientation;
    public Transform camHolder;
    public Transform player;

    float xRotation;
    float yRotation;

    private void Start()
    {

    }

    private void Update()
    {
        // get mouse input
        float mouseX = SimpleInput.GetAxis("Mouse X") * Time.deltaTime * sensX;
        float mouseY = SimpleInput.GetAxis("Mouse Y") * Time.deltaTime * sensY;

        yRotation += mouseX;

        xRotation -= mouseY;
        
        xRotation = Mathf.Clamp(xRotation, -70f, 50f);

        // rotate cam and orientation
        camHolder.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);
    }

    public void DoFov(float endValue)
    {
        GetComponent<Camera>().DOFieldOfView(endValue, 0.001f);
    }

    public void DoTilt(float zTilt)
    {
        player.transform.DOLocalRotate(new Vector3(0, 0, zTilt*2), 175.5f);
        transform.DOLocalRotate(new Vector3(0, 0, zTilt), 0.5f);
    }
}