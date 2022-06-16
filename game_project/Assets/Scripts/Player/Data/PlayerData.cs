using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "newPlayerData",menuName = "Data/Player Data/Base Data")]
public class PlayerData : ScriptableObject
{
 [Header("Move State")]
 public float movementVelocity = 5f ; 
 [Header("Jump State")]
 public float jumpVelocity = 15f ; 
 public int amountOfJumps = 1 ;

 [Header("InAir State")]
 public float coyoteTime  = 0.2f;
 public float JumpHeightMultiplier = 0.5f;
 [Header("Check Variable")]
 public float GroundCheckRadius = 0.3f;
 public float WallCheckDistance = 0.6f;
 public LayerMask whatisGround;
 public LayerMask whatisWall ; 
[Header("WallSlide State")]
public float WallSlideVelocity = -3f ;
[Header("WallClimb State")]
public float WallClimbVelocity = 3f ;
[Header("WallJump State")]
public float WallJumpVelocity = 10f;
public float wallJumpTime = 0.4f ; 
public Vector2 wallJumpAngle = new Vector2(1,2);

}
