using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchingDirections : MonoBehaviour
{
    [SerializeField] private ContactFilter2D castFilter;
    [SerializeField] float groundDistance;
    [SerializeField] float wallDistance;

    CapsuleCollider2D capsuleCollider;
    Animator animator;
    PlayerMovement player;

    RaycastHit2D[] groundHits = new RaycastHit2D[10];
    RaycastHit2D[] wallHits = new RaycastHit2D[10];

    private bool _isGrounded;
    public bool IsGrounded {
        get
        {
            return _isGrounded;
        }
        private set
        {
            _isGrounded = value;
            animator.SetBool(AnimationStrings.isGrounded, value);
        } 
    }

    private bool _isOnWall;
    public bool IsOnWall
    {
        get
        {
            return _isOnWall;
        }
        private set
        {
            _isOnWall = value;
            animator.SetBool(AnimationStrings.isOnWall, value);
        }
    }

    private Vector2 wallCheckDirection => transform.localScale.x > 0 ? Vector2.right : Vector2.left;

    // Start is called before the first frame update
    void Start()
    {
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        animator = GetComponent<Animator>();
        player = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        IsGrounded = capsuleCollider.Cast(Vector2.down, castFilter, groundHits, groundDistance) > 0;
        if (IsGrounded)
        {
            IsOnWall = false;
        }
        else
        {
        IsOnWall = capsuleCollider.Cast(wallCheckDirection, castFilter, wallHits, wallDistance) > 0;
        }
    }
}
