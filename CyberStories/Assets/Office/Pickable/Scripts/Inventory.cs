using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<GameObject> items;

    private void Start()
    {
        items = new List<GameObject>();
    }

    public void AddItem(GameObject item)
    {
        items.Add(item);
    }

    private void Update()
    {
        Debug.Log(items.Count);
    }
}
