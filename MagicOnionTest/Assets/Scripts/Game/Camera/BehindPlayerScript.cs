﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehindPlayerScript : MonoBehaviour
{
    public GameObject player;
    public float distanceFromObject = 6f;

    // Use this for initialization
    void Start()
    {
        
    }

    void Update()
    {
        Vector3 lookOnObject = player.transform.position - transform.position;
        lookOnObject = player.transform.position - transform.position;
        transform.forward = lookOnObject.normalized;
        Vector3 playerLastPosition;
        playerLastPosition = player.transform.position - lookOnObject.normalized * distanceFromObject;
        playerLastPosition.y = player.transform.position.y + distanceFromObject / 2;
        transform.position = playerLastPosition;
    }
}
