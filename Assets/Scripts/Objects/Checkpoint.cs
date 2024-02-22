using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private Animator animator;

    public bool activated;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !activated)
        {
            activated = true;
            animator.SetTrigger(AnimationStrings.isActive);
            FindObjectOfType<GameManager>().NewCheckpoint(gameObject.transform);
        }
    }
}
