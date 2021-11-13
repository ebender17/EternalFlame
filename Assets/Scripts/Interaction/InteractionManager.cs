using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InteractionType { None = 0, Flame, Talk, Read }

public class Interaction
{
    public InteractionType type;
    public GameObject interactableObject; 

    public Interaction(InteractionType iType, GameObject obj)
    {
        type = iType;
        interactableObject = obj;
    }
}

public class InteractionManager : MonoBehaviour
{
    [HideInInspector] public InteractionType currentInteractionType;
    [SerializeField] private InputReader inputReader = default;

    private LinkedList<Interaction> _potentialInteractions = new LinkedList<Interaction>();

    [Header("Listening to channels")]
    [SerializeField] private VoidEventChannelSO _onInteractionEnded = default;

    [Header("Broadcasting on channels")]
    [SerializeField] private InteractionUIEventChannelSO _toggleInteractionUI = default;

    private void OnEnable()
    {
        inputReader.interactEvent += OnInteraction;
        _onInteractionEnded.OnEventRaised += OnInteractionEnd;
    }

    private void OnDisable()
    {
        inputReader.interactEvent -= OnInteraction;
        _onInteractionEnded.OnEventRaised += OnInteractionEnd;
    }

    private void OnInteraction()
    {
        if (_potentialInteractions.Count == 0)
            return;

        currentInteractionType = _potentialInteractions.First.Value.type;

        if(currentInteractionType == InteractionType.Flame)
        {
            _potentialInteractions.First.Value.interactableObject.GetComponent<FlameController>().InteractWithFlame();
        }
    }

    private void OnInteractionEnd()
    {
        if(currentInteractionType == InteractionType.Flame)
        {
            RequestUIUpdate(true);
        }
    }

    /// <summary>
    /// Called by the Event on the trigger collider on child GameObject named "InteractionDetector"
    /// Example Event <see cref="ZoneTriggerController"/>
    /// </summary>
    /// <param name="isWithin"></param>
    /// <param name="obj"></param>
    public void OnTriggerChangeDetected(bool isWithin, GameObject obj)
    {
        //Add this check so interaction UI does not get triggered during a cutscene with dialogue
        if (inputReader.CurrentSchema == GameSchemas.Gameplay)
        {
            if (isWithin)
            {
                AddPotentialInteraction(obj);
            }
            else
            {
                RemovePotentialInteraction(obj);
            }      
        }
    }

    private void AddPotentialInteraction(GameObject obj)
    {
        Interaction newPotentialInteraction = new Interaction(InteractionType.None, obj);

        if (obj.CompareTag("Flame"))
        {
            newPotentialInteraction.type = InteractionType.Flame;
        }

        if (newPotentialInteraction.type != InteractionType.None)
        {
            _potentialInteractions.AddFirst(newPotentialInteraction);
            RequestUIUpdate(true);
        }
    }

    private void RemovePotentialInteraction(GameObject obj)
    {
        LinkedListNode<Interaction> currentNode = _potentialInteractions.First;

        //Loop through LinkedList until object is found and removed
        while (currentNode != null)
        {
            if (currentNode.Value.interactableObject == obj)
            {
                _potentialInteractions.Remove(currentNode);
                break;
            }
            currentNode = currentNode.Next;
        }

        //Toggle UI depending on if there are more interactions or not 
        RequestUIUpdate(_potentialInteractions.Count > 0);
    }

    private void RequestUIUpdate(bool isVisible)
    {
        if (isVisible)
        {
            _toggleInteractionUI.RaiseEvent(true, _potentialInteractions.First.Value.type);
        }
        else
        {
            //TODO: Do we need to raise this event? Just do nothing instead.
            _toggleInteractionUI.RaiseEvent(false, InteractionType.None);
        }  
    }
}
