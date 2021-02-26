﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpingState : PlayerState {
    
    public override void Enter(PlayerStateInput stateInput, CharacterStateTransitionInfo transitionInfo = null)
    {
        //stateInput.anim.Play("Player_Jump");
        stateInput.lastXDir = 0;
    }

    public override void Update(PlayerStateInput stateInput)
    {
        stateInput.playerController.isGrounded = Physics2D.OverlapCircle(stateInput.playerController.groundCheck.position, stateInput.playerController.checkRadius, stateInput.playerController.whatIsGround);

        if (stateInput.playerController.canDash() && stateInput.playerControls.InGame.Dash.WasPressedThisFrame()) {
            character.ChangeState<PlayerDashState>();
            return;
        }

        if (stateInput.playerControls.InGame.Shoot.WasPressedThisFrame()) {
            stateInput.playerController.Shoot();
        }

        if (stateInput.playerControls.InGame.Jump.WasPressedThisFrame() && stateInput.playerController.canJump())
        {
            stateInput.playerController.hasJumpedOnce = true;
            stateInput.playerController.Jump();
        }

        if (stateInput.rb.velocity.y <= 0)
        {
            character.ChangeState<PlayerFallingState>();
        }
        else if (stateInput.playerControls.InGame.Jump.WasReleasedThisFrame())
        {
            stateInput.playerController.JumpRelease();
            character.ChangeState<PlayerFallingState>();
        }
        // Movement animations and saving previous input
        int horizontalMovement = (int)Mathf.Sign(stateInput.playerControls.InGame.Move.ReadValue<Vector2>().x);
        if (stateInput.lastXDir != horizontalMovement)
        {
            if (horizontalMovement != 0)
            {
                stateInput.spriteRenderer.flipX = horizontalMovement == -1;
            }
        }
        stateInput.lastXDir = horizontalMovement;
    }


    public override void FixedUpdate(PlayerStateInput stateInput)
    {
        stateInput.playerController.HandleMovement();
    }
}
