using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootCannon : MonoBehaviour
{
    public bool inCannon;
    public bool inTube;
    public bool nextInLine;
    public bool inTransit;
    public bool Frozen;
    public GameObject cannonballSpawnLoc;
    private Rigidbody rb;
    private SpawnManager spawnManager;
    public float cannonPower;

    // Start is called before the first frame update
    void Awake()
    {
        cannonballSpawnLoc = GameObject.Find("Cannonball Spawn");
        rb = GetComponent<Rigidbody>();
        spawnManager = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F)&&inCannon)
        {
            Debug.Log("Firing");
            Fire();
        }

        // if the inCannon is true, then make sure it follows the empty gameobject
    }

    private void LateUpdate()
    {
        if (inCannon)
        {
            transform.position = cannonballSpawnLoc.transform.position;
            transform.rotation = cannonballSpawnLoc.transform.rotation;
            rb.useGravity = false ;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        MeshRenderer mR = GetComponent<MeshRenderer>();
        mR.enabled = true;
        inTube = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!inTube)
        {
            // if this object hits another object with the same colour tag AND this object is not frozen
            if (collision.collider.tag == this.tag&&!Frozen)
            {
                Destroy(collision.gameObject);
                Destroy(gameObject);

                // don't trigger RefreshCannonball twice

                spawnManager.RefreshCannonball();
                Debug.Log("Refreshing Cannonball due to correct colour match");
            }
            else if(!Frozen) // if this object hits another object with a different colour tag AND this object is not frozen
            {
                transform.position = new Vector3(transform.position.x,transform.position.y,-0.5f);
                rb.constraints = RigidbodyConstraints.FreezePosition;

                //if (collision.collider.GetType().FullName == "ShootCannon")
                
                    spawnManager.RefreshCannonball();
                    Debug.Log("Refreshing Cannonball due to INCORRECT colour match");

                Frozen = true;

            }
        }
       
        if(collision.gameObject.CompareTag("Bottom Ammo Border"))
        {
            nextInLine = true;
        }
    }

    void Fire()
    {
        inCannon = false;
        rb.velocity = new Vector3(0, 0, 0);

        rb.AddForce(cannonballSpawnLoc.transform.up * cannonPower, ForceMode.Impulse);
        transform.localScale = Vector3.one;
        Debug.Log(rb.velocity);

    }
}
