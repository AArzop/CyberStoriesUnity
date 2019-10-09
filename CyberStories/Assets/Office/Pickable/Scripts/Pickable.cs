using UnityEngine;
using Valve.VR.InteractionSystem;

[RequireComponent( typeof( Throwable ) )]
public class Pickable : MonoBehaviour
{
    [HideInInspector]
    public Throwable throwable;
    
    // Start is called before the first frame update
    protected virtual void Awake()
    {
        throwable = GetComponent<Throwable>();
    }

    // Update is called once per frame
    void Update()
    {
        if (throwable.attached && Input.GetKeyDown(KeyCode.Return))
        {
            // deactivate itself like it was set in an inventory
            gameObject.SetActive(false);
        }
    }
}
