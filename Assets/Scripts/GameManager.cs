using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //[Header("Broadcasting on channels")]
    //[SerializeField] private BoolEventChannelSO _openUIGameEvent = default;
    //[SerializeField] private VoidEventChannelSO _onFinalCutsceneEnded;

    [Header("Listening on channels")]
    //[SerializeField] private BoolEventChannelSO _gameResultEvent = default;
    [SerializeField] private VoidEventChannelSO _playerDeathEvent = default;

    [SerializeField] private GameObject _loadScenesOnEnable;

    private void OnEnable()
    {
        if(_playerDeathEvent != null)
        {
            _playerDeathEvent.OnEventRaised += HandleGameResult;
        }
    }

    private void OnDisable()
    {
        if(_playerDeathEvent != null)
        {
            _playerDeathEvent.OnEventRaised -= HandleGameResult;
        }
    }

    private void HandleGameResult()
    {
        StartCoroutine(LoadGameOverMenu());
    }

    IEnumerator LoadGameOverMenu()
    {
        yield return new WaitForSeconds(5f);

        if(_loadScenesOnEnable != null)
        {
            _loadScenesOnEnable.SetActive(true);
        }

    }
}
