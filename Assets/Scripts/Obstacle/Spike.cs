using UnityEngine;

public class Spike : MonoBehaviour
{
    public static float attackSpeedBase = 1f;

    [SerializeField] private CombatController combatController;
    [SerializeField] private Stats stats;
    public float lastAttack;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (Time.time - lastAttack < attackSpeedBase / stats.AttackSpeed) return;
        if (!other.CompareTag("Player")) return;
        ColliderDictionary.GetCombatController(other).TakeDamage(stats);
        // other.GetComponent<CombatController>().TakeDamage(stats);
        lastAttack = Time.time;
    }
}