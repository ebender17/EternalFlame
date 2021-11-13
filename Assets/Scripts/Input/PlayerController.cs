using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 3;

    [Header("Player's Flame")]
    [SerializeField] private Light flameLight;
    [SerializeField] private float minIntensityRange = 0f;
    [SerializeField] private float maxIntensityRange = 1f;
    [SerializeField] private float flickerSpeed = 0.5f;
    [SerializeField] private float minFlameRange = 2f;
    [SerializeField] private float maxFlameRange = 10f;

    [SerializeField] private Material diffuseMaterial;
    [SerializeField] private Material defaultMaterial;

    public InputReader inputReader;
    
    private Vector2 movement;

    private Rigidbody2D rb;

    private Animator animator;

    private SpriteRenderer spriteRenderer;

    private float maxFlameStamina = 100f;

    private float currentFlameStamina;

    private float currentFlameRange;

    public VectorValue startingPosition;

    [Header("Broadcasting on")]
    [SerializeField] private FloatEventChannelSO _flameStaminaBarChannel;

    [Header("Listening on")]
    [SerializeField] private FlameEventChannelSO handleFlameStaminaUpdate;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        transform.position = startingPosition.initialValue;

        currentFlameRange = maxFlameRange;
        currentFlameStamina = maxFlameStamina;

        StartCoroutine(UpdateFlameStaminaBar(10f));
    }

    private void OnEnable()
    {
        inputReader.moveEvent += OnMovement;
        handleFlameStaminaUpdate.OnEventRaised += UpdateCurrentFlameStamina;
    }

    private void OnDisable()
    {
        inputReader.moveEvent -= OnMovement;
        handleFlameStaminaUpdate.OnEventRaised -= UpdateCurrentFlameStamina;
    }

    private void OnMovement(Vector2 inputMovement)
    {
        movement = inputMovement;

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.magnitude);

        if (movement.x == 1 || movement.x == -1 || movement.y == 1 || movement.y == -1)
        {
            animator.SetFloat("LastHorizontal", movement.x);
            animator.SetFloat("LastVertical", movement.y);
        }

    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * speed * Time.deltaTime);
    }

    private IEnumerator UpdateFlameStaminaBar(float value)
    {
        while(currentFlameStamina > 0f)
        {
            Debug.Log("Cour flame stamina: " + currentFlameStamina);

            Debug.Log("Cour flame range: " + currentFlameRange);

            _flameStaminaBarChannel.RaiseEvent(currentFlameStamina / 100f);

            currentFlameStamina = currentFlameStamina - value;

            currentFlameRange = currentFlameRange - 1f;

            flameLight.range = currentFlameRange;

            yield return new WaitForSeconds(5f);

        }

        //Update stamina bar once more so it shows it is empty. 
        _flameStaminaBarChannel.RaiseEvent(currentFlameStamina / 100f);
        flameLight.intensity = 0f;
        spriteRenderer.material = diffuseMaterial;
        Debug.Log("Flame light intesity set to 0");
    }

    public void UpdateCurrentFlameStamina(FlameDataSO data)
    {
        //If we already have 100% stamina, exit
        if (currentFlameStamina == maxFlameStamina)
        {
            return;
        }

        float oldFlameStamina = currentFlameStamina;
        currentFlameStamina = currentFlameStamina + data.FlameHealth;
        currentFlameRange = maxFlameRange * (currentFlameStamina / maxFlameStamina);

        currentFlameStamina = Mathf.Clamp(currentFlameStamina, 0f, maxFlameStamina);
        currentFlameRange = Mathf.Clamp(currentFlameRange, minFlameRange, maxFlameRange);

        Debug.Log("Old flame stamina: " + oldFlameStamina);
        Debug.Log("Current flame stamina: " + currentFlameStamina);
        Debug.Log("Current flame range: " + currentFlameRange);

        //Previously the flame was out and now the flame is revived
        if (oldFlameStamina < 0.01f)
        {
            spriteRenderer.material = defaultMaterial;
            StartCoroutine(UpdateFlameStaminaBar(10f));
        }
        //We don't need to start another coroutine. Just need to update flame stamina bar right away.
        else
        {
            _flameStaminaBarChannel.RaiseEvent(currentFlameStamina / 100f);
        }


        //TODO: Handle different types of flames

    }


    private void Update()
    {
        if (currentFlameStamina > 0.01f)
        {
            //spriteRenderer.material = diffuseMaterial;
            flameLight.intensity = Mathf.Lerp(minIntensityRange, maxIntensityRange, Mathf.PingPong(Time.time, flickerSpeed));
            return;
        }

        //flameLight.intensity = Mathf.Lerp(minIntensityRange, maxIntensityRange, Mathf.PingPong(Time.time, flickerSpeed));
        //flameLight.range = startFlameRange;
        
    }


}
