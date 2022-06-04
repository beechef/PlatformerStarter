using UnityEngine;

public class SpikeBall : MonoBehaviour
{
    [SerializeField] private LayerMask playerMask;
    private StatsController statsController;
    private Transform target;
    private Stats stats => statsController.GetStats();

    private float lastAttack;
    private void Start()
    {
        statsController = GetComponent<StatsController>();
    }

    private void Update()
    {
        Attack();
        GetTarget();
        FollowTarget();
    }

    private void GetTarget()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, stats.EffectRadius, playerMask);
        if (colliders == null) return;
        Transform minTarget = null;
        float minDistance = float.MaxValue;
        foreach (var collider in colliders)
        {
            float distance = Vector2.Distance(transform.position, collider.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                minTarget = collider.transform;
            }
        }

        target = minTarget;
    }
    private void FollowTarget()
    {
        if (target == null) return;
        Vector2 dir = (target.position - transform.position).normalized;
        transform.Translate(dir * (stats.MoveSpeed * Time.deltaTime));
    }
    private void Attack()
    {
        if (Time.time - lastAttack <= 2 / stats.AttackSpeed) return;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, stats.AttackRange, playerMask);
        if (colliders == null) return;
        foreach (var collider in colliders)
        {
            ColliderDictionary.GetCombatController(collider).TakeDamage(stats);
        }

        lastAttack = Time.time;
    }
}