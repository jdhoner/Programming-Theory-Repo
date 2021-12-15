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
    public GameObject collidedBall;
    private Rigidbody rb;
    private SpawnManager spawnManager;
    public float cannonPower = 10f;

    // Start is called before the first frame update
    public virtual void  Awake()
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
            collidedBall = collision.gameObject;

            // if this object hits another object with the same colour tag AND this object is not frozen
            if (collision.collider.tag == this.tag&&!Frozen)
            {
                MatchedCollision();
            }
            else if(!Frozen) // if this object hits another object with a different colour tag AND this object is not frozen
            {
                UnmatchedCollision();
            }
        }
       
        if(collision.gameObject.CompareTag("Bottom Ammo Border"))
        {
            nextInLine = true;
        }
    }

    public virtual void Fire()
    {
        inCannon = false;
        rb.velocity = new Vector3(0, 0, 0);

        rb.AddForce(cannonballSpawnLoc.transform.up * cannonPower, ForceMode.Impulse);
        transform.localScale = Vector3.one;
        Debug.Log(rb.velocity);

    }

    public virtual void MatchedCollision()
    {
        Destroy(gameObject);
        Destroy(collidedBall);
        spawnManager.RefreshCannonball();
        Debug.Log("Refreshing Cannonball due to correct colour match");
    }

    public virtual void UnmatchedCollision()
    {
        Frozen = true;
        transform.position = new Vector3(transform.position.x, transform.position.y, -0.5f);
        rb.constraints = RigidbodyConstraints.FreezePosition;


        spawnManager.RefreshCannonball();
        Debug.Log("Refreshing Cannonball due to INCORRECT colour match");
    }
}
