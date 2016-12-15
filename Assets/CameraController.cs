using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float Rotation = 0;
    public float RotationSpeed = 10;
    public float MoveSpeed = 20;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        var leftRight = Input.GetAxis("Horizontal");
        var upDown = Input.GetAxis("Vertical");

        transform.position += new Vector3(upDown, 0, leftRight).normalized * Time.deltaTime * MoveSpeed;
    }
}