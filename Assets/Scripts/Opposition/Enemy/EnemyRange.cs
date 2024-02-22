using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRange : EnemyAttack
{
    [SerializeField] private EnemyProjectile[] spikeBalls;
    [SerializeField] private Transform arrowSpawn;

    // Start is called before the first frame update
    void Start()
    {
        StartUp();
    }

    // Update is called once per frame
    void Update()
    {
        CheckDirection();
        SetProjectileSpawn();
        CheckAttack();
    }

    private void SetProjectileSpawn()
    {
        Vector3 newPos = arrowSpawn.localPosition;
        if (direction == 1)
        {
            newPos.x = 1.2f;
        }
        else
        {
            newPos.x = -1.2f;
        }
        arrowSpawn.localPosition = newPos;
    }

    private void CheckAttack()
    {
        if (PlayerInRange() && canAttack)
        {
            StartCoroutine(Attack());
        }
    }

    public override IEnumerator Attack()
    {
        enemyMovement.canMove = false;
        canAttack = false;
        animator.SetTrigger(AnimationStrings.rangeAttack);

        yield return new WaitForSeconds(attackDuration);

        audioController.EnemyRangeSFX();
        LaunchSpike();
        enemyMovement.canMove = true;
        StartCoroutine(AttackOver());
    }

    private void LaunchSpike()
    {
        int spike = GetSpike();
        spikeBalls[spike].ResetBall(isFacingRight());
        spikeBalls[spike].gameObject.SetActive(true);
    }

    private bool isFacingRight()
    {
        if(direction == 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private int GetSpike()
    {
        for (int i = 0; i < spikeBalls.Length; i++)
        {
            if (!spikeBalls[i].gameObject.activeInHierarchy)
            {
                return i;
            }
        }
        return 0;
    }

    private IEnumerator AttackOver()
    {
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(collider.bounds.center + new Vector3((range / 2 + collider.bounds.size.x / 2) * direction, 0), new Vector3(range, collider.bounds.size.y));

        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(arrowSpawn.position, 0.2f);
    }
}
