using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("Interaction Events")]
    [SerializeField] private InteractionUIEventChannelSO setInteractionEvent = default;

    [Header("HUD Events")]
    [SerializeField] private FloatEventChannelSO updateFlameStaminaEvent = default;

    [Header("UI Panels")]
    [SerializeField] private UIHUDManager HUD = default;

    [SerializeField] private UIInteractionManager interactionUI = default;

    [SerializeField] private UIHeartsManager heartsManager = default;

    [Header("Listening on")]
    [SerializeField] private VoidEventChannelSO endingCutsceneEvent = default;

    private void OnEnable()
    {
        if(updateFlameStaminaEvent != null)
        {
            updateFlameStaminaEvent.OnEventRaised += UpdateFlameStamina;
        }

        if(setInteractionEvent != null)
        {
            setInteractionEvent.OnEventRaised += SetInteractionUI;
        }

        if(endingCutsceneEvent != null)
        {
            endingCutsceneEvent.OnEventRaised += HidePlayerHUD;
        }
    }

    private void OnDisable()
    {
        if(updateFlameStaminaEvent != null)
        {
            updateFlameStaminaEvent.OnEventRaised -= UpdateFlameStamina;
        }

        if(setInteractionEvent != null)
        {
            setInteractionEvent.OnEventRaised -= SetInteractionUI;
        }

        if (endingCutsceneEvent != null)
        {
            endingCutsceneEvent.OnEventRaised -= HidePlayerHUD;
        }
    }

    private void UpdateFlameStamina(float value)
    {
        HUD.SetValue(value);
    }

    public void SetInteractionUI(bool isOpen, InteractionType interactionType)
    {
        if(isOpen)
        {
            interactionUI.FillInteractionPanel(interactionType);
        }

        interactionUI.gameObject.SetActive(isOpen);
    }

    private void HidePlayerHUD()
    {
        HUD.gameObject.SetActive(false);
        interactionUI.gameObject.SetActive(false);
        heartsManager.DisableHearts();
    }
}
