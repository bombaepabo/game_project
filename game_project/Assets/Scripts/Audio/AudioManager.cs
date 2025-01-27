using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;
public class AudioManager : MonoBehaviour
{
    [Header("Volume")]
    [Range(0,1)]
    public float masterVolume = 1 ;
    [Range(0,1)]
    public float musicVolume = 1 ;
    [Range(0,1)]
    public float ambienceVolume = 1 ;
    [Range(0,1)]
    public float SFXVolume = 1 ;
    private Bus masterBus ;
    private Bus musicBus ;
    private Bus ambienceBus ;
    private Bus sfxBus ;
    private List<StudioEventEmitter> eventEmiteers;
    private List<EventInstance> eventInstances;
    public static AudioManager instance {get; private set;}
    private EventInstance ambienceEventInstance ;
    private EventInstance MusicEventInstance ;

   private void Awake()
   {
    if(instance != null){
        Debug.Log("Found More than one Audio Manager in the scene.");
    }
    instance = this ; 
    eventInstances = new List<EventInstance>();
    eventEmiteers = new List<StudioEventEmitter>();

    masterBus = RuntimeManager.GetBus("bus:/");
    musicBus = RuntimeManager.GetBus("bus:/Music");
    ambienceBus = RuntimeManager.GetBus("bus:/Ambience");
    sfxBus = RuntimeManager.GetBus("bus:/SFX");
   }
   private void Start(){
   // InitializeAmbience(FModEvent.instance.Ambience);
   InitializeMusic(FModEvent.instance.Music);
   }
   private void Update(){
    masterBus.setVolume(masterVolume);
    musicBus.setVolume(musicVolume);
    ambienceBus.setVolume(ambienceVolume);
    sfxBus.setVolume(SFXVolume);
   }
   public void PlayOneShot(EventReference sound , Vector3 worldPos){
    RuntimeManager.PlayOneShot(sound,worldPos);
   }
   public EventInstance CreateInstance(EventReference EventReference){
    EventInstance eventInstance = RuntimeManager.CreateInstance(EventReference);
    eventInstances.Add(eventInstance);
    return  eventInstance;
   }
   public StudioEventEmitter InitializeEventEmitter(EventReference EventReference,GameObject emitterGameObject){
    StudioEventEmitter emitter = emitterGameObject.GetComponent<StudioEventEmitter>();
    emitter.EventReference = EventReference ; 
    eventEmiteers.Add(emitter);
    return emitter ;

   }
   private void InitializeAmbience(EventReference ambienceEventReference){
        ambienceEventInstance = CreateInstance(ambienceEventReference);
        ambienceEventInstance.start();
   }
    private void InitializeMusic(EventReference MusicEventReference){
        MusicEventInstance = CreateInstance(MusicEventReference);
        MusicEventInstance.start();
   }
   private void SetAmbienceParameter(string parameterName, float parameterValue){
    ambienceEventInstance.setParameterByName(parameterName,parameterValue);
   }
   public void SetMusicAreaParameter(MusicArea area){
    MusicEventInstance.setParameterByName("area",(float) area);
   }

   private void CleanUp(){
    foreach (EventInstance eventInstance in eventInstances){
        eventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        eventInstance.release();
    }
    
    foreach(StudioEventEmitter emitter in eventEmiteers){
        emitter.Stop();
    }
   }
   private void OnDestroy(){
    CleanUp();
   }
}
