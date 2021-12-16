using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonController : MonoBehaviour
{
    public GameObject pivotPoint;
    private int rotateSpeed = -50;
    public float horizontalInput;


    // want the rotation of the pivot to be limited to (0.5 xpos, -0.5 ypos, -60 zrot) and opposite

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
            horizontalInput = Input.GetAxis("Horizontal");
            transform.RotateAround(pivotPoint.transform.position, Vector3.forward, rotateSpeed * horizontalInput * Time.deltaTime);
    }
}
