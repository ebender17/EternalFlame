using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class AudioManager : MonoBehaviour
{
    [Header("Listening on channels")]
    [SerializeField] private AudioSoundEventChannelSO _playMusicEvent = default;
    [SerializeField] private AudioSoundEventChannelSO _playSFXEvent = default;
    //[SerializeField] private AudioSoundsEventChannelSO _playSFXRandomEvent = default;
    //TODO: Issue here. Cannot end music with Unity's pool class as we cannot get a reference to the name on the audioclip for each AudioSource in the pool. 
    //May have to go to old script unless we can just kill all sounds.
    //[SerializeField] private AudioSoundEventChannelSO _endMusicEvent = default;

    private ObjectPool<AudioSource> _pool;

    private void Start()
    {
        //Creating pool of audio sources for sounds to play from
        _pool = new ObjectPool<AudioSource>(CreatedPooledObject, OnTakeFromPool, OnReturnedToPool, OnDestroyPoolObject, false, 10);
    }

    private void OnEnable()
    {
        _playMusicEvent.OnEventRaised += PlaySound;
        _playSFXEvent.OnEventRaised += PlaySound;
        //_playSFXRandomEvent.OnEventRaised += SelectSound;
        //_endMusicEvent.OnEventRaised += FindSoundToRelease;
    }
    private void OnDisable()
    {
        _playMusicEvent.OnEventRaised -= PlaySound;
        _playSFXEvent.OnEventRaised -= PlaySound;
        //_playSFXRandomEvent.OnEventRaised -= SelectSound;
        //_endMusicEvent.OnEventRaised -= FindSoundToRelease;
    }

    private AudioSource CreatedPooledObject()
    {
        AudioSource audioSource = new AudioSource();
        audioSource.name = "Audio Source";
        audioSource.gameObject.SetActive(false);

        return audioSource;
    }

    //Call when an item is taken from the pool using Get
    void OnTakeFromPool(AudioSource audioSource)
    {
        audioSource.gameObject.SetActive(true);
    }

    private IEnumerator ReturnToPool(AudioSource obj, float delay)
    {
        yield return new WaitForSeconds(delay);
        _pool.Release(obj);
    }

    //Called when an item is taken from the pool using Release
    void OnReturnedToPool(AudioSource audioSource)
    {
        audioSource.gameObject.SetActive(false);
    }

    //If the pool capacity is reached then any items returned will be destroyed.
    //Here we destroy the GameObject.
    private void OnDestroyPoolObject(AudioSource audioSource)
    {
        Destroy(audioSource.gameObject);
    }

    private void SetSource(AudioSource source, Sound audio)
    {
        source.clip = audio.clip;
        source.pitch = audio.pitch;
        source.volume = audio.volume;
        source.loop = audio.loop;
        source.outputAudioMixerGroup = audio.audioMixerGroup;
    }

    private void PlaySound(Sound audio)
    {
        AudioSource audioSource = _pool.Get();

        SetSource(audioSource, audio);
        audioSource.transform.SetParent(transform);
        audioSource.name = audio.name;
        audioSource.Play();

        if(!audioSource.loop)
        {
            StartCoroutine(ReturnToPool(audioSource, audio.clip.length));
        }
    }

    
}
