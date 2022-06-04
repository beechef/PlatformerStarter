using System;
using UnityEngine;

public class DeadZone : MonoBehaviour
{
    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.collider.CompareTag("Player"))
        {
            ColliderDictionary.GetCombatController(other.collider).Dead();
        }
    }
}