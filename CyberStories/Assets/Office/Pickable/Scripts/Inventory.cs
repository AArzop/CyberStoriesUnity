using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<Pickable> items;

    private void Start()
    {
        items = new List<Pickable>();
    }

    public void AddItem(Pickable item)
    {
        items.Add(item);
    }

    private void Update()
    {
        Debug.Log(items.Count);
    }
}
