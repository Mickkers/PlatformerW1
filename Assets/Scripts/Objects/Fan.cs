using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fan : MonoBehaviour
{
    [SerializeField] private float upwardsStrength;

    private Rigidbody2D target;

    private void Update()
    {
        if(target != null)
        {
            Vector2 newV = target.velocity;
            newV.y = upwardsStrength;
            target.velocity = newV;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            target = collision.gameObject.GetComponent<Rigidbody2D>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        target = null;
    }
}
