﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float MoveSpeed = 20;
    public float RotationSpeed = 250;
    public float ZoomSpeed = 10;
    public float BorderWidth = 20;
    public bool MouseLockedWhileRotating;
    public bool RawInput = true;

    // Use this for initialization
    void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsMouseMiddlePressed())
        {
            Cursor.visible = false;
            if (MouseLockedWhileRotating && !Application.isEditor)
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
            Rotate();
        }
        else
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
        }
        Move();
        Zoom();

    }

    float GetAxis(string axisName)
    {
        return RawInput ? Input.GetAxisRaw(axisName) : Input.GetAxis(axisName);
    }

    bool IsMouseMiddlePressed()
    {
        return Input.GetAxisRaw("Fire3") > 0;
    }

    void Move()
    {
        var mousePos = Input.mousePosition;
        var x = mousePos.x;
        var y = mousePos.y;

        var dx = GetAxis("Horizontal");
        var dy = GetAxis("Vertical");

        var intention = Vector3.zero;
        if (!IsMouseMiddlePressed() && x >= 0 && x <= BorderWidth || dx < 0)
        {
            intention += -transform.right;
        }
        else if (!IsMouseMiddlePressed() && x >= Screen.width - BorderWidth && x <= Screen.width || dx > 0)
        {
            intention += transform.right;
        }

        if (!IsMouseMiddlePressed() && y >= 0 && y <= BorderWidth || dy < 0)
        {
            intention += -transform.forward;
        }
        else if (!IsMouseMiddlePressed() && y >= Screen.height - BorderWidth && y <= Screen.height || dy > 0)
        {
            intention += transform.forward;
        }

        // stay on same x-z plane
        intention.y = 0;

        transform.Translate(intention * Time.deltaTime * MoveSpeed, Space.World);
    }

    void Rotate()
    {
        var horizontal = GetAxis("Mouse X");
        var vertical = GetAxis("Mouse Y");

        gameObject.transform.RotateAround(transform.position, Vector3.up, horizontal * RotationSpeed * Time.deltaTime);
        gameObject.transform.RotateAround(transform.position, Vector3.Cross(transform.forward, Vector3.up), vertical * RotationSpeed * Time.deltaTime);
    }

    void Zoom()
    {
        var wheel = GetAxis("Mouse ScrollWheel");
        var direction = transform.forward;
        RaycastHit hit;
        if (Physics.Raycast(GetComponent<Camera>().ScreenPointToRay(Input.mousePosition), out hit))
        {
            direction = (hit.point - transform.position).normalized;
        }

        transform.Translate(direction * wheel * ZoomSpeed, Space.World);
    }
}
