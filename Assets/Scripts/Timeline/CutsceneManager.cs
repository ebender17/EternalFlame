using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CutsceneManager : MonoBehaviour
{
    [Header("Listening on channels")]
    [SerializeField] private PlayableDirectorChannelSO _playCutsceneEvent;

    [Header("Broadcasting on channels")]
    [SerializeField] private VoidEventChannelSO _endingCutscenePlaying;

    //[Header("Broadcasting on channels")]
    //[SerializeField] private BoolEventChannelSO _gameResultEvent;
    //[SerializeField] private VoidEventChannelSO _loadEndMenuEvent;

    private PlayableDirector _activePlayableDirector;
    private bool _isPaused = false;
    private bool _stopLooping = false; //flag to indicate when advanceDialogueEvent has been pressed
    private uint _loopingCounter = 0;
    private float _advanceTime = 0;
    private bool _isEndingCutscene; //flag for raising load end menu

    private void OnEnable()
    {
        if(_playCutsceneEvent != null)
        {
            _playCutsceneEvent.OnEventRaised += PlayCutscene;
        }
    }

    private void OnDisable()
    {
        if(_playCutsceneEvent != null)
        {
            _playCutsceneEvent.OnEventRaised -= PlayCutscene;
        }
    }

    void PlayCutscene(PlayableDirector activePlayableDirector, bool isEndingCutscene)
    {
        _activePlayableDirector = activePlayableDirector;

        _isPaused = false;
        _isEndingCutscene = isEndingCutscene;
        _activePlayableDirector.Play();
        //When cutscene ends
        _activePlayableDirector.stopped += HandleDirectorStopped;
    }

    void CutsceneEnded()
    {
        if (_activePlayableDirector != null)
            _activePlayableDirector.stopped -= HandleDirectorStopped;

        if(_isEndingCutscene)
        {
            _endingCutscenePlaying.RaiseEvent();
        }
        /*if (_isEndingCutscene)
        {
            _gameResultEvent.RaiseEvent(true);
            _loadEndMenuEvent.RaiseEvent();
        }*/
    }

    private void HandleDirectorStopped(PlayableDirector director) => CutsceneEnded();

}
