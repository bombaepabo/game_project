using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scene17_18 : MonoBehaviour,IDataPersistent
{
    private Player player ;
    [Header("Ink Json")]
    [SerializeField] private TextAsset inkJson ; 
    public bool isFinished = false ;
    [SerializeField]private GameObject Enemy ; 
    private Vector2 initspawnEnemy ; 
    private SpriteRenderer visual;
    [SerializeField] GameObject Apath;

    private bool playerInRange ; 

    private void Awake(){
        playerInRange = false ;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        initspawnEnemy =  Enemy.transform.position ; 

    }
    private void Update(){
        if(playerInRange && !DialogueManager.GetInstance().dialogueIsPlaying){

            if(playerInRange && !isFinished){
            DialogueManager.GetInstance().EnterDialogueMode(inkJson);   
            }
            isFinished = true ;
        }
     if(isFinished && !DialogueManager.GetInstance().dialogueIsPlaying){
            Enemy.SetActive(false);
            Enemy.transform.position = initspawnEnemy ;
            Apath.SetActive(false);
            }
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag == "Player"){
            playerInRange = true ; 
            
        }
    }
    private void OnTriggerExit2D(Collider2D collider)
    {
        if(collider.gameObject.tag == "Player"){
            playerInRange = false ; 
        }
    }
       public void LoadData(GameData data){
        isFinished = data.scene17isFinished;
  }
  public void SaveData(GameData data){
    data.scene17isFinished = isFinished ; 
}
}