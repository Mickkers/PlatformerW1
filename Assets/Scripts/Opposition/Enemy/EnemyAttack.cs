using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyAttack : MonoBehaviour
{
    [HideInInspector] public SpriteRenderer spriteRenderer;
    [HideInInspector] public EnemyMovement enemyMovement;
    [HideInInspector] public Animator animator;
    [HideInInspector] public PlayerHealth target;
    [HideInInspector] public AudioController audioController;

    [HideInInspector] public float direction;
    public LayerMask playerLayer;
    public float range;
    public int damage;
    public bool canAttack;
    public float attackDuration;
    public float attackCooldown;
    public CapsuleCollider2D collider;



    public void StartUp()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        enemyMovement = GetComponent<EnemyMovement>();
        animator = GetComponent<Animator>();
        audioController = AudioController.Instance;
        direction = 1f;
        canAttack = true;
    }

    public bool PlayerInRange()
    {
        RaycastHit2D hit = Physics2D.BoxCast(collider.bounds.center + new Vector3((range / 2 + collider.bounds.size.x / 2) * direction, 0), new Vector3(range, collider.bounds.size.y), 0, Vector2.right, playerLayer);
        if (hit.transform.gameObject.CompareTag("Player"))
        {
            target = hit.transform.gameObject.GetComponent<PlayerHealth>();
            return true;
        }
        target = null;
        return false;
    }

    public void CheckDirection()
    {
        if (spriteRenderer.flipX == true)
        {
            direction = -1f;
        }
        else
        {
            direction = 1f;
        }
    }

    public abstract IEnumerator Attack();
}
