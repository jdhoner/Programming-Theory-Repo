using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenCannonBalls : ShootCannon
{
    public AudioSource sound;
    public GameObject cam;

    public override void MatchedCollision()
    {
        cam = GameObject.Find("Main Camera");
        sound = cam.GetComponent<AudioSource>();
        sound.Play();
        base.MatchedCollision();
    }

}
