using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonController : MonoBehaviour
{
    public GameObject pivotPoint;
    private int rotateSpeed = -50;
    public float horizontalInput;

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
