using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "New Stats", menuName = "Stats")]
public class Stats : ScriptableObject
{
    //Ratio convert Armor to Reduction Damage - Ex: Ratio = 100 -> 100 Armor = 1% Reduction Damage
    public static float armorToReductionRatio = 100;

    //Duration of Attack Animation
    public static float attackSpeedBase = 1.4f;

    //Duration of Block Animation
    public static float blockSpeedBase = 1.4f;

    //Stamina consume per Second
    public static float staminaConsumeRate = 10f;


    [SerializeField] private float maxHealth;

    public float MaxHealth
    {
        get => maxHealth;
        set => maxHealth = value;
    }

    [HideInInspector] [SerializeField] private float health;

    public float Health
    {
        get => health;
        set => health = value;
    }

    [SerializeField] private float attack;

    public float Attack
    {
        get => attack;
        set => attack = value;
    }

    [SerializeField] private float armor;

    public float Armor
    {
        get => armor;
        set => armor = value;
    }

    [SerializeField] private float moveSpeed;

    public float MoveSpeed
    {
        get => moveSpeed;
        set => moveSpeed = value;
    }

    [SerializeField] private float sprintSpeed;

    public float SprintSpeed
    {
        get => sprintSpeed;
        set => sprintSpeed = value;
    }

    [SerializeField] private float jumpHeight;

    public float JumpHeight
    {
        get => jumpHeight;
        set => jumpHeight = value;
    }

    [SerializeField] private float maxStamina;

    public float MaxStamina
    {
        get => maxStamina;
        set => maxStamina = value;
    }

    [HideInInspector] [SerializeField] private float stamina;

    public float Stamina
    {
        get => stamina;
        set => stamina = value;
    }

    [SerializeField] private float staminaRecovery;

    // Stamina Recovery per Second
    public float StaminaRecovery
    {
        get => staminaRecovery;
        set => staminaRecovery = value;
    }

    [SerializeField] private float attackSpeed;

    public float AttackSpeed
    {
        get => attackSpeed;
        set => attackSpeed = value;
    }

    [SerializeField] private float attackRange;

    public float AttackRange
    {
        get => attackRange;
        set => attackRange = value;
    }

    [SerializeField] private float blockSpeed;

    public float BlockSpeed
    {
        get => blockSpeed;
        set => blockSpeed = value;
    }

    [SerializeField] private float blockRange;

    public float BlockRange
    {
        get => blockRange;
        set => blockRange = value;
    }

    [SerializeField] private float effectRadius;

    public float EffectRadius
    {
        get => effectRadius;
        set => effectRadius = value;
    }
}