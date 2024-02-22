using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTrap : MonoBehaviour
{
    private DamagePlayer dmgPlayer;
    private Animator animator;

    [SerializeField] private float downTime;
    [SerializeField] private float startupTime;
    [SerializeField] private float activeTime;

    // Start is called before the first frame update
    void Start()
    {
        dmgPlayer = GetComponent<DamagePlayer>();
        animator = GetComponent<Animator>();
        StartCoroutine(TrapSequence());
    }

    private IEnumerator TrapSequence()
    {
        dmgPlayer.canDamage = false;
        yield return new WaitForSeconds(downTime);
        animator.SetTrigger(AnimationStrings.startUp);
        yield return new WaitForSeconds(startupTime);
        animator.SetBool(AnimationStrings.isActive, true);
        dmgPlayer.canDamage = true;
        yield return new WaitForSeconds(activeTime);
        animator.SetBool(AnimationStrings.isActive, false);
        dmgPlayer.canDamage = false;
        StartCoroutine(TrapSequence());
    }
}
