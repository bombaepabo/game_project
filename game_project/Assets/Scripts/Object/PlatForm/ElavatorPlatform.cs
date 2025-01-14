using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElavatorPlatform : MonoBehaviour
{
    public Transform posA,posB ;
    public float speed ;
    Vector3 targetPos; 
    Player player ; 
    Rigidbody2D rb ; 
    Vector3 moveDirection;
    // Start is called before the first frame update
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        rb = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        targetPos = posB.position ; 
        DirectionCalculate();
    }

    // Update is called once per frame
    private void Update()
    {
        if(player.isOnPlatform){
            if(player.wallGrabState.IsWallGrab |player.wallClimbState.IsWallClimb|player.wallSlideState.IsWallSlide)
                {
                transform.position = Vector2.MoveTowards (transform.position,posB.transform.position , 15f * Time.deltaTime);
                StartCoroutine("DelayAction",2f);
                }    
            else{
                transform.position = Vector2.MoveTowards (transform.position,posA.transform.position , 2f * Time.deltaTime);
                }
        }
        else{
            transform.position = Vector2.MoveTowards (transform.position,posA.transform.position , 2f * Time.deltaTime);
        }
    }
    
    private void FixedUpdate(){
         
    }
    void DirectionCalculate(){
        moveDirection = (targetPos - transform.position).normalized;
    }
    void OnTriggerEnter2D(Collider2D collision){
        if(collision.gameObject.name.Equals("Player")){
            player.isOnPlatform = true ;
            player.platformRb = rb;
                if(player.wallGrabState.IsWallGrab |player.wallClimbState.IsWallClimb|player.wallSlideState.IsWallSlide ){
                    collision.gameObject.transform.parent =this.transform ; 
                }
            
          
        }
    }
    void OnTriggerExit2D(Collider2D collision){
        if(collision.gameObject.name.Equals("Player")){
            if(player.JumpState.isJumping){
                Debug.Log("is Jumping out movingplatforms");
            }
            player.isOnPlatform = false ;
            collision.transform.parent =null ; 
            
        }

    }
    IEnumerator PlatformBack(float delayTime){
        yield return new WaitForSeconds(delayTime);

        transform.position = Vector2.MoveTowards (transform.position,posA.transform.position , 2f * Time.deltaTime);

    }
    IEnumerator DelayAction(float delayTime){
        yield return new WaitForSeconds(delayTime);
    }
   
    
}
