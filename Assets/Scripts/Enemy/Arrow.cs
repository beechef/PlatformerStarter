using System;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    // public Transform target;
    public Stats stats;
    public float moveSpeed;
    private Collider2D coll;

    private void Start()
    {
        coll = GetComponent<Collider2D>();
        ColliderDictionary.AddArrow(coll, this);
    }

    private void Update()
    {
        // FollowTarget();
        Move();
    }

    private void Move()
    {
        transform.Translate(Vector2.right * (moveSpeed * Time.deltaTime), Space.Self);
    }
    // private void FollowTarget()
    // {
    //     // var position = transform.position;
    //     // Vector3 dir = (target.position - position).normalized;
    //     // position += dir * moveSpeed * Time.deltaTime;
    //     // transform.position = position;
    // }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ColliderDictionary.GetCombatController(other).TakeDamage(stats);
            // other.GetComponent<CombatController>().TakeDamage(stats);
        }

        DestroyObject();
    }

    public void DestroyObject()
    {
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        ColliderDictionary.RemoveArrow(coll);
    }
}