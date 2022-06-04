using System;
using UnityEngine;

public class HiddenArcher : MonoBehaviour
{
    // [SerializeField] private LayerMask playerMask;
    private StatsController statsController;
    private Stats stats => statsController.GetStats();
    [SerializeField] private Transform attackPoint;
    [SerializeField] private GameObject arrowPrefab;
    private float lastAttack;

    private void Start()
    {
        statsController = GetComponent<StatsController>();
    }

    private void Update()
    {
        CastArrow();
    }

    private void CastArrow()
    {
        if (Time.time - lastAttack <= 2 / stats.AttackSpeed) return;
        // Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, stats.AttackRange, playerMask);
        // if (colliders == null) return;

        Arrow arrow;

        arrow = Instantiate(arrowPrefab).GetComponent<Arrow>();
        arrow.stats = stats;
        var arrowTransform = arrow.transform;
        arrowTransform.position = attackPoint.position;
        arrowTransform.rotation = attackPoint.rotation;
        // arrow.transform.LookAt(collider.transform, Vector3.right);
        // arrow.transform.rotation =
        //     Quaternion.Euler(new Vector3(arrow.transform.rotation.x, 180, arrow.transform.rotation.z));


        lastAttack = Time.time;
    }
}