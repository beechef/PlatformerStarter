using System.Collections;
using UnityEngine;

public class EnemyCombatController : CombatController
{
    [SerializeField] private SpriteRenderer spriteRenderer;

    private void Start()
    {
        coll = GetComponent<Collider2D>();
        statsController = GetComponent<StatsController>();
        ColliderDictionary.AddCombatController(coll, this);
    }

    public override void TakeDamage(Stats enemyStats)
    {
        base.TakeDamage(enemyStats);
        if (stats.Health <= 0)
        {
            StartCoroutine(DeadAnimation());
            return;
        }

        StartCoroutine(TakeDamageAnimation());
    }

    private IEnumerator DeadAnimation()
    {
        spriteRenderer.color = new Color(0, 0, 0, 0.5f);
        yield return new WaitForSeconds(0.35f);
        Destroy(gameObject);
    }

    private IEnumerator TakeDamageAnimation()
    {
        Color color = spriteRenderer.color;
        spriteRenderer.color = new Color(1, 0, 0, 0.5f);
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.color = color;
    }

    private void OnDestroy()
    {
        ColliderDictionary.RemoveCombatController(coll);
    }
}