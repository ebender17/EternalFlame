using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIInteractionItemFiller : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI interactionName = default;
    [SerializeField] private TextMeshProUGUI interactionKeyButton = default;

    public void FillInteractionPanel(InteractionSO interactionItem)
    {
        interactionName.text = interactionItem.InteractionName;
        interactionKeyButton.text = KeyCode.E.ToString(); //TODO: Change letter to different platforms
    }
}
