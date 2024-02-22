using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private PlayerProjectile[] fireballs; 

    private AudioController audioController;
    private PlayerMovement playerMovement;

    private bool canAttack;


    void Awake()
    {
        canAttack = true;
        audioController = AudioController.Instance;
        playerMovement = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Attack()
    {
        StartCoroutine(AttackAction());
    }

    private IEnumerator AttackAction()
    {
        if (canAttack)
        {
            //audioController.AttackSFX();
            canAttack = false;

            LaunchFireball();

            yield return new WaitForSeconds(attackCooldown);
            canAttack = true;
        }
    }

    private void LaunchFireball()
    {
        int proj = GetFireball();
        fireballs[proj].gameObject.SetActive(true);
        fireballs[proj].ResetBall(playerMovement.IsFacingRight);
        audioController.AttackSFX();
    }

    private int GetFireball()
    {
        for (int i = 0; i < fireballs.Length; i++)
        {
            if (!fireballs[i].gameObject.activeInHierarchy)
            {
                return i;
            }
        }
        return 0;
    }
}
