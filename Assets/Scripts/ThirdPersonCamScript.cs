using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamScript : MonoBehaviour
{
    [SerializeField] private GameObject target;

    private float horizontalSpeed = 20.0f;
    private float verticalSpeed = 20.0f;
    public float axisY, axisX = 0.0f;

    public float axisXMax = 360.0f;
    public float axisXMin = -360.0f;

    public float axisYMax = 360.0f;
    public float axisYMin = -360.0f;


    public float height = 1.0f;
    public float distance = 7.0f;
    private Vector3 camPosition = Vector3.zero; // eigenes Cordianten System

    public Vector3 offset = new Vector3(0.0f, 2.0f, 0.0f);

    public float scrollScalle = 0.5f;
    public float distanceMax = 0.0f;
    public float distanceMin = 0.0f;
    public float changeDistanceStart;
    private float distanceStart;
    // Start is called before the first frame update
    void Start()
    {
        distanceStart = distance;
        distance = changeDistanceStart;
    }

    void LateUpdate()
    {

        if (changeDistanceStart != distanceStart) distanceStart = changeDistanceStart;

        // ############## Scroll distance ##############
        distance += -(Input.mouseScrollDelta.y * scrollScalle);

        float distMaxTmp = distanceStart + distanceMax;
        if (distance > distMaxTmp) distance = distMaxTmp;

        float distMinTmp = distanceStart - distanceMin;
        if (distance < distMinTmp) distance = distMinTmp;


        //############## berechnungen der Kamera ##############
        camPosition.y = height;
        camPosition.z = distance;

        //rotation
        if (Input.GetMouseButton(0)){
            axisY = (Input.GetAxis("Mouse X") * verticalSpeed + axisY) % 360.0f;
            axisX = (Input.GetAxis("Mouse Y") * horizontalSpeed + axisX) % 360.0f;
        }
        if (axisX > axisXMax) axisX = axisXMax;
        if (axisX < axisXMin) axisX = axisXMin;

        if (axisY > axisYMax) axisY = axisYMax;
        if (axisY < axisYMin) axisY = axisYMin;

        Quaternion rotation = Quaternion.Euler(axisX, axisY, 0.0f);

        Vector3 rotationPosition = rotation * camPosition; 


        //update camera
        Vector3 focus = target.transform.position + offset;

        transform.position = focus + rotationPosition;
        transform.LookAt(focus);
    }
}
