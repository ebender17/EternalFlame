using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameController : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] private FlameDataSO flameData;

    [Header("Broadcasting on channels")]
    [SerializeField] private FlameEventChannelSO updateFlameStaminaEvent = default;

    private PlayerController playerController;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            playerController = collision.gameObject.GetComponent<PlayerController>();
            Debug.Log("Retrieved player gameobject in flame controller.");
        }

    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        playerController = null;
        Debug.Log("Set playercontroller to null in Flame Controller.");
    }

    public void InteractWithFlame()
    {
        if(flameData != null)
        {
            UpdateFlameStaminaBar();
        }
    }

    private void UpdateFlameStaminaBar()
    {
        if(playerController != null)
        {
            //playerController.UpdateCurrentFlameStamina(flameData);
        }
        updateFlameStaminaEvent?.RaiseEvent(flameData);
    }
}
