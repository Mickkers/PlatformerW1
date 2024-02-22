using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private PlayerInput playerInput;
    public PlayerInput.GameplayActions gameplay;

    private PlayerMovement playerMovement;
    private PlayerAttack playerAttack;
    private UIManager ui;

    // Start is called before the first frame update
    private void Start()
    { 
        gameplay.Jump.performed += ctx => playerMovement.Jump();
        gameplay.Dash.started += ctx => playerMovement.OnDash();
        gameplay.Attack.performed += ctrx => playerAttack.Attack();
        gameplay.Pause.performed += cctx => ui.TogglePause();
    }

    void Awake()
    {
        playerInput = new PlayerInput();
        gameplay = playerInput.Gameplay;
        playerMovement = GetComponent<PlayerMovement>();
        playerAttack = GetComponent<PlayerAttack>();
        ui = FindObjectOfType<UIManager>();
    }

    private void OnEnable()
    {
        gameplay.Enable();
    }

    private void OnDisable()
    {
        gameplay.Disable();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        playerMovement.Move(gameplay.Move.ReadValue<float>());
    }
}