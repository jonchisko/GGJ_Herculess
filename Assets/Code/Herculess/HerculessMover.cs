﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class HerculessMover : MonoBehaviour
{
    public delegate void MoveFinished(HerculessMover a);

    public Animator animController;
    
    [SerializeField] 
    private float speed = 5.0f;

    private Vector3 movementDirection;

    public Vector3 MovementDirection
    {
        get => movementDirection;
        set => movementDirection = value;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        animController.SetBool("IsRunning", true);
        movementDirection = Vector3.forward;
    }

    // Update is called once per frame
    void Update()
    {
        MoveHerculess();
    }

    private void MoveHerculess()
    {
        this.transform.Translate( movementDirection * (speed * Time.deltaTime));
    }

    private void OnTriggerEnter(Collider other)
    {
        ObstacleBehaviour obstacle = other.gameObject.GetComponent<ObstacleBehaviour>();
        if (obstacle != null)
        {
            obstacle.ExecuteObstacleEffect(this);
        }
    }


}
