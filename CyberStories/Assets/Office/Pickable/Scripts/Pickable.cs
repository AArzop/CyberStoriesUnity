using System.Security.Cryptography;
using UnityEngine;
using Valve.VR.InteractionSystem;

[RequireComponent( typeof( Throwable ) )]
public class Pickable : MonoBehaviour
{
    [HideInInspector]
    public Throwable throwable;

    public Inventory inventory;

    // Start is called before the first frame update
    protected virtual void Awake()
    {
        throwable = GetComponent<Throwable>();

        if (!inventory)
        {
            Debug.LogWarning(gameObject + " is a pickable but does not have any inventory set.");
        }
    }

    // Update is called once per frame
    protected void Update()
    {
        if (throwable.attached && Input.GetKeyDown(KeyCode.P))
        {
            // destroy itself like it was set in an inventory
            Destroy(gameObject);
            
            // add to inventory
            inventory.AddItem(this);
        }
    }
}
