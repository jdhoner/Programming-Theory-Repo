using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnManager : MonoBehaviour
{
    private bool gameStart;
    private bool newCannonBall;
    public GameObject[] ballPrefab;
    [SerializeField]GameObject[] cannonballPrefab;
    [SerializeField] GameObject startButton;
    private Vector3 cannonballSpawn = new Vector3(0, -3.7f, -0.25f);
    private Vector3 cannonballAmmo = new Vector3(10f, -1f, 0);

    // Start is called before the first frame update
    void Start()
    {
        gameStart = false;
        newCannonBall = false;
    }

    private void FixedUpdate()
    {
        if (newCannonBall)
        {
            StartCoroutine(NewCannonBall());
        }
    }


    public void SpawnBalls() //Called using button
    {
        // For each spawn location in scene, spawn a ball
        foreach(GameObject spawnPos in GameObject.FindGameObjectsWithTag("Spawn Location"))
        {
            GameObject ball = Instantiate(ballPrefab[RandomCannonball()], spawnPos.transform.position, spawnPos.transform.rotation);
            ball.transform.parent = spawnPos.transform;
            // assign each ballPrefab with a number correlating to the specific spawnPos
            // assign each spawnPos as a true bool while a ballPrefab is there
        }

        // put first cannonball in cannon
        StartCannon();
        StartCoroutine(SpawnCannonballs());
        startButton.SetActive(false);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void StartCannon()
    {
        
        GameObject startingCannonball = Instantiate(cannonballPrefab[RandomCannonball()], cannonballSpawn, cannonballPrefab[RandomCannonball()].transform.rotation);
        startingCannonball.GetComponent<MeshRenderer>().enabled = true;
        startingCannonball.GetComponent<ShootCannon>().inCannon = true;
    }

    public int RandomCannonball()
    {
        int spawnedCannonball = Random.Range(0, cannonballPrefab.Length);

        return spawnedCannonball;
    }

   IEnumerator SpawnCannonballs()
    {
        for(int i=0; i< 4; i++)
        {
            yield return new WaitForSeconds(0.75f);
            Instantiate(cannonballPrefab[RandomCannonball()], cannonballAmmo, transform.rotation);
            Debug.Log("Spawning initial cannonball no." + i);
            if (i == 3)
            {
                gameStart = true;
            }
        }
    }

    public void RefreshCannonball()
    {
        StartCoroutine(FreshCannonball());
    }

    IEnumerator FreshCannonball()
    {
        yield return new WaitForSeconds(0.75f);
        foreach (ShootCannon cannonball in GameObject.FindObjectsOfType<ShootCannon>())
        {
            if (cannonball.nextInLine)
            {
                cannonball.gameObject.GetComponent<SphereCollider>().enabled = false;
                cannonball.inTransit = true;
                cannonball.nextInLine = false;
                cannonball.inTube = false;
            }
        }
        yield return new WaitForSeconds(0.75f);
        foreach (ShootCannon cannonball in GameObject.FindObjectsOfType<ShootCannon>())
        {
            if (cannonball.inTransit)
            {
                cannonball.gameObject.transform.position = cannonballSpawn;
                cannonball.gameObject.GetComponent<SphereCollider>().enabled = true;
                cannonball.inCannon = true;
                cannonball.inTransit = false;
            }
        }
        newCannonBall = true;

    }

    IEnumerator NewCannonBall()
    {
        newCannonBall = false;
        yield return new WaitForSeconds(0.75f);
        Instantiate(cannonballPrefab[RandomCannonball()], cannonballAmmo, transform.rotation);
    }

}
