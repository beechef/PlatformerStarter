using UnityEngine;

public class Collectable : MonoBehaviour
{
    [SerializeField] private Animator animator;
    protected CollectionController collectionController;

    protected Collider2D coll;
    private void Start()
    {
        coll = GetComponent<Collider2D>();
        ColliderDictionary.AddCollectable(coll, this);
    }

    public void Collect(CollectionController controller)
    {
        collectionController = controller;
        animator.SetBool("Collect", true);
    }

    protected virtual void CollectObject()
    {
        
    }

    protected virtual void DestroyObject()
    {
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        ColliderDictionary.RemoveCollectable(coll);
    }
}
