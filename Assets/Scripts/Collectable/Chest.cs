
using UnityEngine;

public class Chest : Collectable
{
    public ChestType type;
    public float value;
    private void Start()
    {
        coll = GetComponent<Collider2D>();
        ColliderDictionary.AddCollectable(coll, this);

    }

    protected override void CollectObject()
    {
        base.CollectObject();
        switch (type)
        {
            case ChestType.Heal:
            {
                collectionController.Heal(value);
                break;
            }
            case ChestType.AttackSpeed:
            {
                collectionController.AttackSpeed(value);
                break;
            }
        }
    }

    private void OnDestroy()
    {
        ColliderDictionary.RemoveCollectable(coll);        
    }
}

public enum ChestType
{
    Heal,
    AttackSpeed,
}