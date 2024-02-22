using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    [HideInInspector] public Rigidbody2D rbody;
    [HideInInspector] public Animator animator;
    [HideInInspector] public AudioController audioController;

    public int damage;
    public Transform startPos;
    public float projectileSpeed;
    public float killTime;

    [HideInInspector] public float lifetime;
    [HideInInspector] public float direction;
    [HideInInspector] public bool canMove;

    public void StartUp()
    {

        rbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioController = AudioController.Instance;
    }

    public void Movement()
    {
        if (canMove)
        {
            rbody.velocity = new Vector2(projectileSpeed * direction, 0);
        }
        else
        {
            rbody.velocity = new Vector2(0, 0);
        }
    }

    public void CheckLifetime()
    {
        lifetime += Time.deltaTime;
        if (lifetime >= killTime)
        {
            canMove = false;
            animator.SetBool(AnimationStrings.explode, !canMove);
        }
    }

    public void ResetBall(bool isFacingRight)
    {
        lifetime = 0;
        canMove = true;
        transform.position = startPos.position;
        if (isFacingRight)
        {
            transform.localScale = new Vector2(1, 1);
            direction = 1;
        }
        else
        {
            transform.localScale = new Vector2(-1, 1);
            direction = -1;
        }
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
        canMove = true;
        animator.SetBool(AnimationStrings.explode, !canMove);
    }
}
