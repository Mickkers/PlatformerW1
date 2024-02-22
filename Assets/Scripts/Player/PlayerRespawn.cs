using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    private Animator animator;
    private PlayerHealth pHealth;
    private Rigidbody2D rbody;
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        pHealth = GetComponent<PlayerHealth>();
        rbody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public IEnumerator Respawn()
    {
        transform.position = FindObjectOfType<GameManager>().GetCheckpoint().position;
        animator.ResetTrigger(AnimationStrings.death);
        yield return new WaitForSeconds(.3f);
        rbody.constraints = RigidbodyConstraints2D.FreezeRotation;
        spriteRenderer.color = Color.white;
        animator.SetTrigger(AnimationStrings.spawn);
        pHealth.ResetHealth();
    }
}
