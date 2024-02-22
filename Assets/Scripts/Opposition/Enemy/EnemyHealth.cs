using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rb;
    private EnemyMovement enemyMovement;

    [Header("Health Attributes")]
    [SerializeField] private int currHealth;
    [SerializeField] private int maxHealth;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        enemyMovement = GetComponent<EnemyMovement>();

        currHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        currHealth = Mathf.Clamp(currHealth, 0, maxHealth);
        if (currHealth == 0)
        {
            Death();
        }
    }

    private void Death()
    {
        enemyMovement.canMove = false;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        animator.SetTrigger(AnimationStrings.death);
    }

    private void DeathOver()
    {
        gameObject.SetActive(false);
    }

    public void TakeDamage(int value)
    {
        enemyMovement.canMove = false;
        currHealth -= value;
        if (currHealth > 0)
        {
            animator.SetBool(AnimationStrings.isHit, true);
        }
    }

    public void HitOver()
    {
        enemyMovement.canMove = true;
    }
}
