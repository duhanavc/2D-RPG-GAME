using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    private GameObject cam;

    [SerializeField] private float parallaxEffect;

    private float xPos;

    void Start()
    {
        cam = GameObject.Find("Main Camera");


        xPos = transform.position.x;

    }

    void Update()
    {
        float distanceToMove = cam.transform.position.x * parallaxEffect;
        transform.position = new Vector3(xPos + distanceToMove, transform.position.y);
        
    }
}
