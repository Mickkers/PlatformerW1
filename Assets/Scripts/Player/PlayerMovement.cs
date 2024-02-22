using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rbody;
    private Animator animator;
    private TrailRenderer trailRenderer;
    private TouchingDirections touchingDirections;
    public AudioController audioController;

    [Header("Movement Settings")]
    [SerializeField] private float runSpeed;
    [SerializeField] private float jumpPower;
    [SerializeField] private float wallSlideSpeed;
    [SerializeField] private float wallJumpWindow;
    [SerializeField] private float wallJumpDuration;
    [SerializeField] private Vector2 wallJumpPower;
    [Header("Dash Settings")]
    [SerializeField] private float dashPower;
    [SerializeField] private float dashDuration;
    [SerializeField] private float dashCooldown;

    private bool canDoubleJump;

    private bool canDash = true;
    private bool isDashing;

    private bool isWallSliding;
    private bool isWallJumping;
    private float wallJumpDirection;
    private float wallJumpingCounter;

    private float moveDirection;

    private bool _isMoving = false;
    public bool IsMoving {
        get
        {
            return _isMoving;
        }
        private set
        {
            _isMoving = value;
            animator.SetBool(AnimationStrings.isMoving, _isMoving);
        } 
    }

    private bool _isFacingRight = true;
    public bool IsFacingRight
    {
        get
        {
            return _isFacingRight;
        }
        private set
        {
            if (_isFacingRight != value)
            {
                transform.localScale *= new Vector2(-1, 1);
            }

            _isFacingRight = value;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        touchingDirections = GetComponent<TouchingDirections>();
        trailRenderer = GetComponent<TrailRenderer>();
        audioController = AudioController.Instance;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        Movement();
        AbilityCheck();
    }

    private void AbilityCheck()
    {
        if (touchingDirections.IsGrounded)
        {
            canDoubleJump = true;
            canDash = true;
        }
    }

    private void Movement()
    {
        if (isDashing || isWallJumping)
        {
            return;
        }

        rbody.velocity = new Vector2(moveDirection * currRunSpeed(), rbody.velocity.y);
        SetFacingDirection(moveDirection);
        animator.SetFloat(AnimationStrings.yVelocity, rbody.velocity.y);

        WallSlide();
        WallJump();
    }

    private void WallSlide()
    {
        if (touchingDirections.IsOnWall && moveDirection != 0f)
        {
            isWallSliding = true;
            animator.SetBool(AnimationStrings.isWallSliding, isWallSliding);
            rbody.velocity = new Vector2(rbody.velocity.x, Math.Clamp(rbody.velocity.y, -wallSlideSpeed, float.MaxValue));
        }
        else
        {
            isWallSliding = false;
            animator.SetBool(AnimationStrings.isWallSliding, isWallSliding);
        }
    }

    private void WallJump()
    {
        if (isWallSliding)
        {
            isWallJumping = false;
            wallJumpDirection = -transform.localScale.x;
            wallJumpingCounter = wallJumpWindow;
            CancelInvoke(nameof(StopWallJumping));
        }
        else
        {
            wallJumpingCounter -= Time.deltaTime;
        }
    }

    private void StopWallJumping()
    {
        isWallJumping = false;
    }

    private void SetFacingDirection(float moveDirection)
    {
        if (moveDirection > 0 && !IsFacingRight)
        {
            IsFacingRight = true;
        } 
        else if (moveDirection < 0 && IsFacingRight)
        {
            IsFacingRight = false;
        }
    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float originalGrav = rbody.gravityScale;
        rbody.gravityScale = 0f;
        rbody.velocity = new Vector2(transform.localScale.x * dashPower, 0f);
        trailRenderer.emitting = true;

        yield return new WaitForSeconds(dashDuration);
        trailRenderer.emitting = false;
        rbody.gravityScale = originalGrav;
        isDashing = false;

        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }

    private float currRunSpeed()
    {
        if(IsMoving && !touchingDirections.IsOnWall)
        {
            return runSpeed;
        }
        else
        {
            return 0;
        }
    }

    public void Move(float val)
    {
        if (isWallJumping)
        {
            return;
        }
        moveDirection = val;

        IsMoving = moveDirection != 0;
    }

    public void Jump()
    {

        if (isWallSliding)
        {
            if (wallJumpingCounter < 0f)
            {
                return;
            }
            audioController.JumpSFX();
            isWallJumping = true;
            rbody.velocity = new Vector2(wallJumpDirection * runSpeed, jumpPower);
            animator.SetTrigger(AnimationStrings.jump);
            wallJumpingCounter = 0f;
            if (transform.localScale.x != wallJumpDirection)
            {
                SetFacingDirection(moveDirection * -1);
            }
            Invoke(nameof(StopWallJumping), wallJumpDuration);
            return;
        }

        if (touchingDirections.IsGrounded || canDoubleJump)
        {
            audioController.JumpSFX();
            if (touchingDirections.IsGrounded)
            {
                animator.SetTrigger(AnimationStrings.jump);
            }
            else if (canDoubleJump)
            {
                animator.SetTrigger(AnimationStrings.doubleJump);
            }
            canDoubleJump = !canDoubleJump;
            rbody.velocity = new Vector2(rbody.velocity.x, jumpPower);
            return;
        }
        
    }

    public void OnDash()
    {
        if (canDash)
        {
            StartCoroutine(Dash());
        }
    }
}
