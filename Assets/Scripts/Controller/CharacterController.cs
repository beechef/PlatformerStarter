using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterAnimationController),
    typeof(Rigidbody2D))]
public class CharacterController : MonoBehaviour
{
    private Rigidbody2D rb;
    private PlayerInputActions inputController;
    private InputAction move;
    private InputAction sprint;
    private InputAction jump;
    private InputAction attack;
    private InputAction block;
    private InputAction collect;

    [SerializeField] private Transform collectPoint;

    private new CharacterAnimationController animation;

    [SerializeField] private LayerMask groundMask;
    [SerializeField] private LayerMask collectableMask;
    private BoxCollider2D charCollider;

    public Transform gfx;


    private float lastAttack;
    private float lastBlock;

    private float xAxis;
    private bool isMove;
    private bool isSprint;
    private bool isJump;
    private bool isAttack;
    private bool isBlock;
    private bool isCollect;

    private CharacterAnimation currentAnimation;
    private CharacterCollectionController collectionController;

    private StatsController statsController;
    private Stats stats => statsController.GetStats();

    private float timeScale;

    private void Start()
    {
        animation = GetComponent<CharacterAnimationController>();
        statsController = GetComponent<StatsController>();
        collectionController = GetComponent<CharacterCollectionController>();

        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;

        charCollider = GetComponent<BoxCollider2D>();

        InitialPlayerInput();
    }

    private void Update()
    {
        timeScale = 1;

        Move();
        Jump();
        Attack();
        Block();
        Collect();

        if (!isSprint)
        {
            //Recovery Stamina per Frame
            statsController.RecoveryStamina(Time.deltaTime);
        }

        if (!isMove && !isJump && !isAttack && !isBlock)
        {
            currentAnimation = CharacterAnimation.Idle;
        }

        animation.ExecAnimation(currentAnimation, timeScale);
    }

    private void InitialPlayerInput()
    {
        inputController = new PlayerInputActions();
        move = inputController.Player.Move;
        move.Enable();
        sprint = inputController.Player.Sprint;
        sprint.Enable();
        jump = inputController.Player.Jump;
        jump.Enable();
        attack = inputController.Player.Attack;
        attack.Enable();
        block = inputController.Player.Block;
        block.Enable();
        collect = inputController.Player.Collect;
        collect.Enable();
    }

    private void ChangeVelocity(float x, float y)
    {
        rb.velocity = new Vector2(x, y);
    }

    private void Move()
    {
        bool isEnoughStamina = true;
        isSprint = sprint.IsPressed();
        float speed = isSprint ? stats.SprintSpeed : stats.MoveSpeed;
        if (isSprint)
        {
            isEnoughStamina = statsController.ConsumeStamina(Time.deltaTime);
            if (!isEnoughStamina)
            {
                speed = stats.MoveSpeed;
            }
        }

        float x = move.ReadValue<Vector2>().x * speed;
        float y = rb.velocity.y;
        isMove = x != 0;
        if (!isMove) return;
        ChangeVelocity(x, y);
        Vector3 scale = gfx.localScale;
        gfx.localScale = new Vector3(Mathf.Clamp(x * Mathf.Abs(scale.x), -Mathf.Abs(scale.x), Mathf.Abs(scale.x)),
            scale.y, scale.z);
        if (isSprint && isEnoughStamina)
        {
            currentAnimation = CharacterAnimation.Sprint;
        }
        else
        {
            currentAnimation = CharacterAnimation.Walk;
        }
    }

    private void Jump()
    {
        isJump = jump.IsPressed();
        if (!isJump) return;
        if (!IsGround())
        {
            isJump = false;
            return;
        }

        var velocity = rb.velocity;
        float x = velocity.x;
        float y = isJump ? stats.JumpHeight : velocity.y;
        ChangeVelocity(x, y);
        currentAnimation = CharacterAnimation.Jump;
    }


    private void Attack()
    {
        isAttack = attack.IsPressed();
        if (!isAttack) return;
        if (Time.time - lastAttack < Stats.attackSpeedBase / stats.AttackSpeed)
        {
            isAttack = false;
            return;
        }

        timeScale = stats.AttackSpeed;
        currentAnimation = CharacterAnimation.Attack;
        lastAttack = Time.time;
    }

    private void Block()
    {
        isBlock = block.IsPressed();
        if (!isBlock) return;
        if (Time.time - lastBlock < Stats.blockSpeedBase / stats.BlockSpeed)
        {
            isBlock = false;
            return;
        }

        timeScale = stats.BlockSpeed;
        currentAnimation = CharacterAnimation.Block;
        lastBlock = Time.time;
    }

    private bool IsGround()
    {
        var bounds = charCollider.bounds;
        return Physics2D.BoxCast(bounds.center, new Vector2(bounds.size.x - 0.1f, bounds.size.y), 0f, Vector2.down, .1f,
            groundMask);
    }

    private void Collect()
    {
        isCollect = collect.WasPerformedThisFrame();
        if (!isCollect) return;
        Collider2D[] collectables =
            Physics2D.OverlapCircleAll(collectPoint.position, stats.EffectRadius, collectableMask);
        if (collectables == null) return;
        foreach (var collectable in collectables)
        {
            ColliderDictionary.GetCollectable(collectable).Collect(collectionController);
            // collectable.GetComponent<Collectable>().Collect(collectionController);
        }
    }
}