using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackCannonBalls : ShootCannon
{
    public GameObject playboxBackground;
    private Material playboxMat;
    public Color colourOne;
    public Color colourTwo;

    public override void Awake()
    {
        base.Awake();
        playboxBackground = GameObject.Find("Playbox Back");
        playboxMat = playboxBackground.GetComponent<MeshRenderer>().material;
        colourOne = RandomColour();
        colourTwo = RandomColour();

    }
    public override void UnmatchedCollision()
    {
        float startTime = 0;
        float t = startTime + Time.deltaTime;
        playboxMat.color = Color.Lerp(colourOne, colourTwo,t);
        base.UnmatchedCollision();
    }

    Color RandomColour()
    {
       Color col = new Color(Random.Range(0.1f, 1.0f), Random.Range(0.1f, 1.0f), Random.Range(0.1f, 1.0f), Random.Range(0.1f, 1.0f));
        return col;
    }
}
