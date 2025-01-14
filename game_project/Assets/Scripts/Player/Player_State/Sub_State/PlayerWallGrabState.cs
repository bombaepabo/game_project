using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallGrabState : PlayerTouchingWallState
{
    private Vector2 holdPosition;
    public bool IsWallGrab = false  ;

    public PlayerWallGrabState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName){
     
 } 
 public override void AnimationFinishTrigger(){
        base.AnimationFinishTrigger();
    }
    public override void AnimationTrigger(){
        base.AnimationTrigger();
    }
    public override void DoChecks(){
        base.DoChecks();
    }
    public override void Enter(){
        base.Enter();
        holdPosition = player.transform.position;
        HoldPosition();
    }
    public override void Exit(){
        base.Exit();
        IsWallGrab = false ;


    }
    public override void LogicUpdate(){
        base.LogicUpdate();
        holdPosition.x = player.transform.position.x; 

        if(!isExitingState){
        HoldPosition();
        playerData.PlayerCurrentClimbStamina -= playerData.ClimbStaminaDrainRate *Time.deltaTime ; 

         if(yinput > 0 && playerData.PlayerCurrentClimbStamina >30){
            stateMachine.ChangeState(player.wallClimbState);
        }
        else if(yinput <0 ||!GrabInput){
            stateMachine.ChangeState(player.wallSlideState);
        }
        else if(playerData.PlayerCurrentClimbStamina <= 30){
            stateMachine.ChangeState(player.wallSlideState);
        }
        else if(playerData.PlayerCurrentClimbStamina <= 0){
                Exit();
        }
       

        }
    }
    private void HoldPosition(){
        if(player.isOnPlatform){

            IsWallGrab = true ;
            player.transform.position = holdPosition;
            player.SetVelocityX(0f);
            player.SetVelocityY(0f);
        }
        else {

        player.transform.position = holdPosition;
        player.SetVelocityX(0f);
        player.SetVelocityY(0f);
        }
    }
    public override void PhysicsUpdate(){
        base.PhysicsUpdate();
    }
}
