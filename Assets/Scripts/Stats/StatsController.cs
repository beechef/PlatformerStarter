using UnityEngine;

public class StatsController : MonoBehaviour
{
    [SerializeField] private Stats stats;


    public BarRenderer healthBarRenderer;
    public BarRenderer staminaBarRenderer;

    private void Awake()
    {
        InitialValue();
    }

    public Stats GetStats()
    {
        return stats;
    }

    private void InitialValue()
    {
        //Clone from ScriptableObject Data
        stats = Instantiate(stats);
        
        //Set up value
        stats.Health = stats.MaxHealth;
        stats.Stamina = stats.MaxStamina;
    }

    private float CalcDamage(Stats rivalStats)
    {
        float reductionDamagePercent = Mathf.Clamp(stats.Armor / Stats.armorToReductionRatio, 0, 100);
        float damage = Mathf.Clamp(rivalStats.Attack * ((100 - reductionDamagePercent) / 100), 0, float.MaxValue);
        return damage;
    }

    public void Hit(Stats rivalStats)
    {
        float damageTaken = CalcDamage(rivalStats);
        stats.Health -= damageTaken;
        healthBarRenderer.Render(stats.Health, stats.MaxHealth);
    }

    public bool ConsumeStamina(float second)
    {
        float staminaConsume = Stats.staminaConsumeRate * second;
        if (stats.Stamina - staminaConsume <= 0)
        {
            staminaBarRenderer.Render(stats.Stamina, stats.MaxStamina);
            return false;
        }
        else
        {
            stats.Stamina -= staminaConsume;
            staminaBarRenderer.Render(stats.Stamina, stats.MaxStamina);
            return true;
        }
    }

    public void RecoveryStamina(float second)
    {
        float recoveryStamina = stats.StaminaRecovery * second;
        if (stats.Stamina + recoveryStamina >= stats.MaxStamina)
        {
            stats.Stamina = stats.MaxStamina;
        }
        else
        {
            stats.Stamina += recoveryStamina;
        }
        staminaBarRenderer.Render(stats.Stamina, stats.MaxStamina);
    }

    public void Heal(float health)
    {
        if (stats.Health + health >= stats.MaxHealth)
        {
            stats.Health = stats.MaxHealth;
        }
        else
        {
            stats.Health += health;
        }
        healthBarRenderer.Render(stats.Health, stats.MaxHealth);
    }

    public void AttackSpeed(float attackSpeed)
    {
        stats.AttackSpeed += attackSpeed;
    }
}