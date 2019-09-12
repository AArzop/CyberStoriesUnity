using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMasterNotification : MonoBehaviour
{
    public int maxNotification = 4;

    public GameObject prefab;

    public Vector3 SpanwPoint;

    private List<GameMasterNotificationItem> currentNotification;
    private List<string> buffer;

    private bool isAdding = false;

    // Start is called before the first frame update
    void Start()
    {
        currentNotification = new List<GameMasterNotificationItem>();
        buffer = new List<string>();
    }

    // Update is called once per frame
    void Update()
    {
        if (buffer.Count != 0 && currentNotification.Count < maxNotification && !isAdding)
        {
            string message = buffer[0];
            buffer.RemoveAt(0);
            StartCoroutine(AddNewNotification(message));
        }
    }

    public void RequestNewNotification(string message)
    {
        buffer.Add(message);
    }

    private IEnumerator AddNewNotification(string message)
    {
        isAdding = true;
        if (currentNotification.Count != 0)
        {
            foreach (var e in currentNotification)
                e.SlideUp();

            while (currentNotification[0].isSlidding)
                yield return new WaitForSeconds(0.01f);
        }

        GameMasterNotificationItem item = Instantiate(prefab)?.GetComponent<GameMasterNotificationItem>();
        if (item != null)
        {
            item.transform.SetParent(transform);
            item.transform.localPosition = SpanwPoint;
            item.message = message;

            currentNotification.Insert(0, item);
        }

        isAdding = false;
        yield return null;
    }

    public void RemoveNotification(GameMasterNotificationItem item)
    {
        if (currentNotification.Contains(item))
        {
            currentNotification.Remove(item);
        }
    }
}
