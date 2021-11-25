using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    [SerializeField] private Sound audioToPlay;
    //[SerializeField] private VoidEventChannelSO _onSceneReady = default;
    [SerializeField] private AudioSoundEventChannelSO _playMusicOn = default;
    //[SerializeField] private AudioSoundEventChannelSO _endMusicEvent = default;

    private void OnEnable()
    {
        //_onSceneReady.OnEventRaised += PlayMusic;
        PlayMusic();
    }

    private void OnDisable()
    {
        EndMusic();
        ///_onSceneReady.OnEventRaised -= PlayMusic;
    }

    private void PlayMusic()
    {
        _playMusicOn.RaiseEvent(audioToPlay);
    }

    private void EndMusic()
    {
        //_endMusicEvent.RaiseEvent(audioToPlay);
    }
}
