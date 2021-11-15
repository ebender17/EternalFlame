using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FlameState
{
    Lit, 
    Nonlit
}

public class FlameController : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] private FlameDataSO flameData;

    [Header("Broadcasting on channels")]
    [SerializeField] private FlameEventChannelSO updateFlameStaminaEvent = default;

    public float timeTilFlameRelights = 10f;

    private PlayerController playerController;

    private Animator animator;

    private FlameState currentFlameState;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        currentFlameState = FlameState.Lit;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            playerController = collision.gameObject.GetComponent<PlayerController>();
        }

    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        playerController = null;
    }

    public void InteractWithFlame()
    {
        if(flameData != null &&  currentFlameState == FlameState.Lit)
        {
            UpdateFlameStaminaBar();
        }
    }

    private void UpdateFlameStaminaBar()
    {
        updateFlameStaminaEvent?.RaiseEvent(flameData);

        if(animator != null)
        {
            animator.SetBool("FlameOut", true);
            currentFlameState = FlameState.Nonlit;
            StartCoroutine(ResetFlame());
        }
    }

    private IEnumerator ResetFlame()
    {
        yield return new WaitForSeconds(timeTilFlameRelights);
        animator.SetBool("FlameOut", false);
        currentFlameState = FlameState.Lit;
        
    }
}
