using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealFruit : MonoBehaviour
{
    private Animator animator;

    [HideInInspector] public AudioController audioController;

    [SerializeField] private int healAmount;

    private bool canHeal;

    private void Start()
    {
        animator = GetComponent<Animator>();
        audioController = AudioController.Instance;
        canHeal = true;
    }

    private void OnEnable()
    {
        ResetHeal();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && canHeal)
        {
            collision.gameObject.GetComponent<PlayerHealth>().HealPlayer(healAmount);
            audioController.HealSFX();
            animator.SetBool(AnimationStrings.isCollected, true);
        }
    }

    public void Collected()
    {
        gameObject.SetActive(false);
    }

    public void ResetHeal()
    {
        canHeal = true;
    }
}
