using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private Rigidbody2D rbody;
    private PlayerMovement pMovement;
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    [SerializeField] private float moveSpeed;
    [SerializeField] private float leftAmount;
    [SerializeField] private float rightAmount;

    public bool canMove;

    private float direction;
    private Vector3 directionToPlayer;
    private float leftEdge;
    private float rightEdge;

    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        pMovement = FindObjectOfType<PlayerMovement>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        leftEdge = transform.position.x - leftAmount;
        rightEdge = transform.position.x + rightAmount;
        direction = 1;
        canMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool(AnimationStrings.isMoving, false);
        if (!canMove)
        {
            return;
        }
        Vector3 playerPos = pMovement.transform.position;
        if(playerPos.x >= leftEdge && playerPos.x <= rightEdge)
        {
            ChasePlayer();
        }
        else
        {
            PatrolArea();
        }
    }

    private void ChasePlayer()
    {
        animator.SetBool(AnimationStrings.isMoving, true);
        directionToPlayer = pMovement.transform.position - transform.position;
        rbody.velocity = new Vector2(directionToPlayer.normalized.x * moveSpeed, 0);
        if(directionToPlayer.normalized.x >= 0)
        {
            spriteRenderer.flipX = false;
        }
        else
        {
            spriteRenderer.flipX = true;
        }
    }

    private void PatrolArea()
    {
        rbody.velocity = new Vector2(moveSpeed * direction, 0);
        animator.SetBool(AnimationStrings.isMoving, true);
        if (transform.position.x <= leftEdge)
        {
            direction = 1;
        }
        else if (transform.position.x >= rightEdge)
        {
            direction = -1;
        }
        if(direction == 1)
        {
            spriteRenderer.flipX = false;
        }
        else
        {
            spriteRenderer.flipX = true;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(transform.position + new Vector3(((rightAmount - leftAmount)/2), 0, 0) , new Vector3(leftAmount + rightAmount, 1, 1));
    }
}
