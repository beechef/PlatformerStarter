using UnityEngine;

public class VictoryPoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ColliderDictionary.GetCombatController(other).Win();
        }
    }
}
