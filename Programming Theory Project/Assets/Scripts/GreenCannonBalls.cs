using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenCannonBalls : ShootCannon//INHERITANCE
{
    [SerializeField] AudioSource sound;
    [SerializeField] GameObject cam;

    public override void MatchedCollision()
    {
        //POLYMORPHISM
        cam = GameObject.Find("Main Camera");
        sound = cam.GetComponent<AudioSource>();
        sound.Play();
        base.MatchedCollision();
    }

}
