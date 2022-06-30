using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLedgeClimbState : PlayerState
{
    private Vector2 detectedPos ; 
    private Vector2 cornerPos ; 
    private Vector2 startPos ;
    private Vector2 stopPos ; 
    private bool isHanging ; 
    private int xinput ; 
    private int yinput ;
    private bool isClimbing ;
    private bool JumpInput ;
    public PlayerLedgeClimbState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
        
    }
    public override void AnimationFinishTrigger(){
        base.AnimationFinishTrigger();
        player.Anim.SetBool("climbLedge",false);
    }
    public override void AnimationTrigger(){
        base.AnimationTrigger();
        isHanging = true ;
    }
    public override void Enter(){
        base.Enter();
        player.SetVelocityZero();
        player.transform.position =detectedPos;
        cornerPos = player.DetermineCornerPosition();
        startPos.Set(cornerPos.x -(player.FacingDirection *playerData.StartOffset.x),cornerPos.y -(playerData.StartOffset.y));
        stopPos.Set(cornerPos.x + (player.FacingDirection * playerData.StopOffset.x),cornerPos.y +(playerData.StopOffset.y));
        player.transform.position = startPos ; 

    }
    public override void Exit(){
        base.Exit();
        isHanging = false ;

        if(isClimbing){
            player.transform.position = stopPos ; 
            isClimbing = false ;
        }
    }
    public override void LogicUpdate(){
        base.LogicUpdate();
        if(isAnimationFinished){
            stateMachine.ChangeState(player.IdleState);
        }
        else{
        xinput = player.inputhandler.NormInputX;
        yinput =player.inputhandler.NormInputY;
        JumpInput = player.inputhandler.JumpInput;
        player.SetVelocityZero();
        player.transform.position = startPos ;

       
         if(yinput == -1 &&isHanging && !isClimbing){
            stateMachine.ChangeState(player.InAirState);
        }
         else if(xinput == player.FacingDirection && isHanging &&!isClimbing){
           isClimbing = true ;
           player.Anim.SetBool("climbLedge",true);
        }
        else if(JumpInput &&!isClimbing){
            player.wallJumpState.DetermineWallJumpDirection(true);
            stateMachine.ChangeState(player.wallJumpState);
        }
        }

       
    }
    public void SetDetectedPosition(Vector2 pos ){
        detectedPos = pos ;
    }
}
