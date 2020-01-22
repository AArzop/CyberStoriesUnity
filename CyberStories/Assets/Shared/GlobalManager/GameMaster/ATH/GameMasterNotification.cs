using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMasterNotification : MonoBehaviour
{
    public int maxNotification = 4;

    public GameObject prefab;

    public Vector3 SpanwPoint;

    private List<GameMasterNotificationItem> currentNotification;

    private class BufferItem
    {
        public BufferItem(string message, GameMasterNotificationItem.NotificationType type)
        {
            this.message = message;
            this.type = type;
        }

        public string message;
        public GameMasterNotificationItem.NotificationType type;
    }

    private List<BufferItem> buffer;

    private bool isAdding = false;

    // Start is called before the first frame update
    void Start()
    {
        currentNotification = new List<GameMasterNotificationItem>();
        buffer = new List<BufferItem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (buffer.Count != 0 && currentNotification.Count < maxNotification && !isAdding)
        {
            BufferItem item = buffer[0];
            buffer.RemoveAt(0);
            StartCoroutine(AddNewNotification(item));
        }
    }

    public void RequestNewNotification(string message, GameMasterNotificationItem.NotificationType type = GameMasterNotificationItem.NotificationType.Standard)
    {
        buffer.Add(new BufferItem(message, type));
    }

    private IEnumerator AddNewNotification(BufferItem bufferItem)
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
            item.transform.rotation = transform.rotation;
            item.transform.localPosition = SpanwPoint;
            item.message = bufferItem.message;
            item.SetHeaderColor(bufferItem.type);

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


    public void TMPBUTTON(string message)
    {
        buffer.Add(new BufferItem(message, GameMasterNotificationItem.NotificationType.Warning));
    }
}
