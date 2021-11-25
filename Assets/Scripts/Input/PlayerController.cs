using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{

    #region State Variables
    [SerializeField] private PlayerData playerData;
    public PlayerStateMachine stateMachine { get; private set; }

    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }
    public PlayerAttackState attackState { get; private set; }

    #endregion

    public InputReader inputReader;

    public Light flameLight;

    private Rigidbody2D rb;

    public Animator animator { get; private set; }

    private SpriteRenderer spriteRenderer;


    private float maxFlameStamina = 100f;

    private float currentFlameStamina;

    private float currentFlameRange;

    private PlayerState currentPlayerState;

    public VectorValue startingPosition;

    #region Input
    private Vector2 rawMovement;
    public int normInputX { get; private set; }
    public int normInputY { get; private set; }
    public Vector2 currentVelocity { get; private set; }
    private Vector2 tempValue;


    public bool attackInput { get; private set; }
    public bool attackInputStop { get; private set; }
    #endregion

    #region Event Channels
    [Header("Broadcasting on")]
    [SerializeField] private FloatEventChannelSO _flameStaminaBarChannel;

    [Header("Listening on")]
    [SerializeField] private FlameEventChannelSO handleFlameStaminaUpdate;
    #endregion

    private void Awake()
    {
        stateMachine = new PlayerStateMachine();

        idleState = new PlayerIdleState(this, stateMachine, playerData, "Idle");
        moveState = new PlayerMoveState(this, stateMachine, playerData, "Move");
        attackState = new PlayerAttackState(this, stateMachine, playerData, "Attack");
    }

    private void OnEnable()
    {
        handleFlameStaminaUpdate.OnEventRaised += UpdateCurrentFlameStamina;

        inputReader.moveEvent += OnMovement;
        inputReader.attackEvent += OnAttack;
        inputReader.attackCanceledEvent += OnAttackCanceled;
    }

    private void OnDisable()
    {
        handleFlameStaminaUpdate.OnEventRaised -= UpdateCurrentFlameStamina;

        inputReader.moveEvent -= OnMovement;
        inputReader.attackEvent -= OnAttack;
        inputReader.attackCanceledEvent -= OnAttackCanceled;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        transform.position = startingPosition.initialValue;

        currentFlameRange = playerData.maxFlameRange;
        currentFlameStamina = maxFlameStamina;

        StartCoroutine(UpdateFlameStaminaBar(10f));

        stateMachine.Initialize(idleState);
    }
    private void OnMovement(Vector2 inputMovement)
    {
            rawMovement = inputMovement;

            if(Mathf.Abs(rawMovement.x) > 0.5f)
            {
                normInputX = (int)(rawMovement * Vector2.right).normalized.x;
            }
            else
            {
                normInputX = 0;
            }

            if (Mathf.Abs(rawMovement.y) > 0.5f)
            {
                normInputY = (int)(rawMovement * Vector2.up).normalized.y;
            }
            else
            {
                normInputY = 0;
            }

            //TODO: Move to state
            /*animator.SetFloat("Horizontal", rawMovement.x);
            animator.SetFloat("Vertical", rawMovement.y);
            animator.SetFloat("Speed", rawMovement.magnitude);

            if (rawMovement.x == 1 || rawMovement.x == -1 || rawMovement.y == 1 || rawMovement.y == -1)
            {
                animator.SetFloat("LastHorizontal", rawMovement.x);
                animator.SetFloat("LastVertical", rawMovement.y);
            }*/
    }

    public void SetVelocity(Vector2 value)
    {
        currentVelocity = value;
    }

    public void SetVelocityX(float value)
    {
        tempValue.Set(value, currentVelocity.y);
        rb.velocity = tempValue;
        currentVelocity = tempValue;
    }

    private void OnAttack()
    {
        StartCoroutine(AttackCo());

        attackInput = true;
        attackInputStop = false;
    }

    private IEnumerator AttackCo()
    {
        //movement = Vector2.zero;
        //animator.SetFloat("Speed", movement.magnitude);
        animator.SetBool("Attacking", true);
        //currentPlayerState = PlayerState.attack;
        yield return null;
        animator.SetBool("Attacking", false);
        yield return new WaitForSeconds(.33f);
        //currentPlayerState = PlayerState.idle;
    }

    public void OnAttackCanceled()
    {
        attackInput = false;
    }

    void AnimationFinishedCallback(AnimationEvent evt)
    {
        if(evt.animatorClipInfo.weight > 0.5)
        {
            stateMachine.currentState.AnimationFinish();
        }
    }

    private IEnumerator UpdateFlameStaminaBar(float value)
    {
        while(currentFlameStamina > 0f)
        {
            _flameStaminaBarChannel.RaiseEvent(currentFlameStamina / 100f);

            currentFlameStamina = currentFlameStamina - value;

            currentFlameRange = currentFlameRange - 1f;

            flameLight.range = currentFlameRange;

            yield return new WaitForSeconds(5f);

        }

        //Update stamina bar once more so it shows it is empty. 
        _flameStaminaBarChannel.RaiseEvent(currentFlameStamina / 100f);
        flameLight.intensity = 0f;
        spriteRenderer.material = playerData.diffuseMaterial;
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
        currentFlameRange = playerData.maxFlameRange * (currentFlameStamina / maxFlameStamina);

        currentFlameStamina = Mathf.Clamp(currentFlameStamina, 0f, maxFlameStamina);
        currentFlameRange = Mathf.Clamp(currentFlameRange, playerData.minFlameRange, playerData.maxFlameRange);

        //Previously the flame was out and now the flame is revived
        if (oldFlameStamina < 0.01f)
        {
            spriteRenderer.material = playerData.defaultMaterial;
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
        stateMachine.currentState.Execute();
        //currentVelocity = rb.velocity;

        if (currentFlameStamina > 0.01f)
        {
            flameLight.intensity = Mathf.Lerp(playerData.minIntensityRange, playerData.maxIntensityRange, Mathf.PingPong(Time.time, playerData.flickerSpeed));
            return;
        }
    }

    private void FixedUpdate()
    {
        stateMachine.currentState.ExecutePhysics();

        //TODO: left off here, updating movement base on rb.MovePosition vs. velocity 
        rb.MovePosition(rb.position + rawMovement * playerData.speed * Time.deltaTime);
    }
}
