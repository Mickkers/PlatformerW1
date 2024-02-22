using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class PlayerHealth : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rb;
    private AudioController audioController;
    private GameManager gameManager;
    private SpriteRenderer spriteRenderer;

    [Header("Health Attributes")]
    [SerializeField] private int currHealth;
    [SerializeField] private int maxHealth;
    [SerializeField] private float iFrameDuration;

    private bool canTakeDamage;
    private bool isAlive;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        audioController = AudioController.Instance;
        gameManager = FindObjectOfType<GameManager>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        ResetHealth();
    }

    public void ResetHealth()
    {

        currHealth = maxHealth;
        isAlive = true;
        canTakeDamage = true;
    }

    // Update is called once per frame
    void Update()
    {
        currHealth = Mathf.Clamp(currHealth, 0, maxHealth);
        if (currHealth == 0 && isAlive)
        {
            Death();
        }
    }

    private void Death()
    {
        isAlive = false;
        audioController.DeathSFX();
        animator.SetTrigger(AnimationStrings.death);
    }

    public void DeathOver()
    {
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        spriteRenderer.color = Color.clear;
        gameManager.RespawnPlayer();
    }

    public void TakeDamage(int value)
    {
        if (!canTakeDamage) return;
        canTakeDamage = false;
        audioController.DamageSFX();
        currHealth -= value;
        if(currHealth > 0)
        {
            animator.SetBool(AnimationStrings.isHit, true);
            StartCoroutine(IFrames());
        }
    }

    public void HealPlayer(int value)
    {
        currHealth += value;
        currHealth = Mathf.Clamp(currHealth, 0, maxHealth);
        audioController.HealSFX();
    }

    private IEnumerator IFrames()
    {
        yield return new WaitForSeconds(iFrameDuration);
        canTakeDamage = true;
        animator.SetBool(AnimationStrings.isHit, false);
    }

    public float GetHealthPercentage()
    {
        return (float) currHealth / (float) maxHealth;
    }
}