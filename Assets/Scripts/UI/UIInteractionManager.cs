using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInteractionManager : MonoBehaviour
{
    [SerializeField] private List<InteractionSO> listInteractions = default;

    [SerializeField] private UIInteractionItemFiller interactionItem = default;

    public void FillInteractionPanel(InteractionType interactionType)
    {
        if ((listInteractions != null) && (interactionItem != null))
        {
            //Check if interactionType exists in list of interactions
            if (listInteractions.Exists(o => o.InteractionType == interactionType))
            {
                //Fill interaction panel if interactionType is present
                interactionItem.FillInteractionPanel(listInteractions.Find(o => o.InteractionType == interactionType));
            }
        }
    }
}
