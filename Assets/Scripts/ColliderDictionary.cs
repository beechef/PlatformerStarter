using System.Collections.Generic;
using UnityEngine;

public static class ColliderDictionary
{
    private static readonly Dictionary<Collider2D, CombatController> CombatControllers = new Dictionary<Collider2D, CombatController>();
    private static readonly Dictionary<Collider2D, Collectable> Collectables = new Dictionary<Collider2D, Collectable>();
    private static readonly Dictionary<Collider2D, Arrow> Arrows = new Dictionary<Collider2D, Arrow>();
    public static CombatController GetCombatController(Collider2D collider)
    {
        return CombatControllers[collider];
    }

    public static void AddCombatController(Collider2D collider, CombatController combatController)
    {
        CombatControllers.Add(collider, combatController);
    }

    public static void RemoveCombatController(Collider2D collider)
    {
        CombatControllers.Remove(collider);
    }
    
    public static Collectable GetCollectable(Collider2D collider)
    {
        return Collectables[collider];
    }

    public static void AddCollectable(Collider2D collider, Collectable collectable)
    {
        Collectables.Add(collider, collectable);
    }

    public static void RemoveCollectable(Collider2D collider)
    {
        Arrows.Remove(collider);
    }
    
    public static Arrow GetArrow(Collider2D collider)
    {
        return Arrows[collider];
    }

    public static void AddArrow(Collider2D collider, Arrow arrow)
    {
        Arrows.Add(collider, arrow);
    }

    public static void RemoveArrow(Collider2D collider)
    {
        Arrows.Remove(collider);
    }
}