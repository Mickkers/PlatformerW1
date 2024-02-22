using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]

public class DamagePlayer : MonoBehaviour
{
    private PlayerHealth pHealth;

    public bool canDamage;
    [SerializeField] private int damage;

    private void Update()
    {
        if (canDamage && pHealth != null)
        {
            pHealth.TakeDamage(damage);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            pHealth = collision.gameObject.GetComponent<PlayerHealth>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            pHealth = null;
        }
    }
}
