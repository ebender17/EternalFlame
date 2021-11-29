using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Broadcasting on channels")]
    [SerializeField] private BoolEventChannelSO _openUIGameEvent = default;
    [SerializeField] private VoidEventChannelSO _onFinalCutsceneEnded;

    [Header("Listening on channels")]
    [SerializeField] private BoolEventChannelSO _gameResultEvent = default;

    private void OnEnable()
    {
        //picked up from Player Controller when game is won or death occurs
        if (_gameResultEvent != null)
            _gameResultEvent.OnEventRaised += HandleGameResult;
    }

    private void OnDisable()
    {
        if (_gameResultEvent != null)
            _gameResultEvent.OnEventRaised -= HandleGameResult;
    }

    private void HandleGameResult(bool isWon)
    {
        //picked up by UI Manager
        //_openUIGameEvent.RaiseEvent(isWon, playerScore);

        //TODO: start courotine and then load final end menu
        StartCoroutine(RaiseLoadEndMenuEvent());
    }

    IEnumerator RaiseLoadEndMenuEvent()
    {
        yield return new WaitForSeconds(5f);


        _onFinalCutsceneEnded.RaiseEvent();
    }
}
