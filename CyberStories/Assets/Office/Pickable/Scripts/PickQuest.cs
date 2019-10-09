using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/**
 * Add this MonoBehavior on an empty game object with the name of your quest (for
 * example : "PrepareBeforeTravelPickQuest") and specify a list of Pickable's to be picked for the quest to be considered 
 * as successfully finished.
 */
public class PickQuest : MonoBehaviour
{
    public List<Pickable> itemsToBePickedUp;
    public Inventory inventory;

    public bool IsCompleted()
    {
        return itemsToBePickedUp.All(item => inventory.items.Contains(item));
    }

    private void Update()
    {
        Debug.Log(IsCompleted());
    }
}
