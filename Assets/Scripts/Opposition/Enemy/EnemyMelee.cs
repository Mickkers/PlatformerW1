using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMelee : EnemyAttack
{
    // Start is called before the first frame update
    void Start()
    {
        StartUp();
    }

    // Update is called once per frame
    void Update()
    {
        CheckDirection();
        CheckAttack();
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
        animator.SetTrigger(AnimationStrings.meleeAttack);

        yield return new WaitForSeconds(attackDuration);

        audioController.EnemyMeleeSFX();
        target.TakeDamage(damage);
        enemyMovement.canMove = true;
        StartCoroutine(AttackOver());
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
    }
}
