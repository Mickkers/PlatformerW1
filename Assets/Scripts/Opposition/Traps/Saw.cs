using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class Saw : MonoBehaviour
{
    private Rigidbody2D rbody;

    [SerializeField] private float moveSpeed;
    [SerializeField] private float leftAmount;
    [SerializeField] private float rightAmount;


    private float direction;
    private float leftEdge;
    private float rightEdge;

    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        leftEdge = transform.position.x - leftAmount;
        rightEdge = transform.position.x + rightAmount;
        direction = 1;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    private void Movement()
    {
        rbody.velocity = new Vector2(moveSpeed * direction, 0);

        if(transform.position.x <= leftEdge)
        {
            direction = 1;
        }
        else if(transform.position.x >= rightEdge)
        {
            direction = -1;
        }
    }
}
