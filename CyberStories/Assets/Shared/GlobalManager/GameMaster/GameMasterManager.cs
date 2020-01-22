using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class GameMasterManager : MonoBehaviour
{
    [Serializable]
    private class Position
    {
        public float x;
        public float y;

        public Position(float x, float y) { this.x = x; this.y = y; }
    }

    [Serializable]
    private class EmailReceived
    {
        public int id;
        public string @object;
        public string message;

        public EmailReceived(int id, string obj, string message) { this.id = id; this.@object = obj; this.message = message; }
    }

    [Serializable]
    private class ObjectToSend
    {
        public string @namespace;
        public string data;

        public ObjectToSend(string type, string data) { this.@namespace = type; this.data = data; }
    }

    [Serializable]
    private class LocationSender
    {
        public string @namespace = "position";

        public string data;

        public LocationSender(float x, float y) { Position p = new Position(x, y); data = JsonUtility.ToJson(p); }
    }

    [Serializable]
    private class JsonToWebSowkcet
    {
        public string type;
        public string payload;

        public JsonToWebSowkcet(string payload) { this.type = "message"; this.payload = payload; }
    }

    [Serializable]
    private class MessageToWebSocket
    {
        public string type = "message";
        public LocationSender payload;

        public MessageToWebSocket(float x, float y) { payload = new LocationSender(x, y); }
    }

    public GameObject botLeftPoint;
    public GameObject topRightPoint;
    public GameObject player;

    public MailApplication mailApp;

    private ClientWebSocket ws;

    private bool isRunning = true;

    public GameMasterNotification notificationManager;

    private List<String> messageToSend;

    private float ComputeLocation(float min, float max, float value)
    {
        if (min > max)
            return 1f - ComputeLocation(max, min, value);

        if (value > max)
            return 1;
        else if (value < min)
            return 0;

        return (value - min) / (max - min);
    }

    private async void SendLocationToWebsocketAsync()
    {
        float x = ComputeLocation(botLeftPoint.transform.position.x, topRightPoint.transform.position.x, player.transform.position.x) * 980;
        float y = (1 - ComputeLocation(botLeftPoint.transform.position.z, topRightPoint.transform.position.z, player.transform.position.z)) * 322;

        MessageToWebSocket messageToWebSocket = new MessageToWebSocket(x, y);
        string json = JsonUtility.ToJson(messageToWebSocket);

        try
        {
            await ws.SendAsync(new ArraySegment<byte>(Encoding.UTF8.GetBytes(json)), WebSocketMessageType.Text, true, CancellationToken.None);
        }
        catch (Exception e)
        {
            Debug.Log(e);
            return;
        }

        await Task.Delay(1000);

        if (isRunning)
            SendLocationToWebsocketAsync();
    }

    private async void GetWebSocketResponseAsync()
    {
        try
        {
            ArraySegment<byte> buf = new ArraySegment<byte>(new byte[5012]);
            WebSocketReceiveResult r = await ws.ReceiveAsync(buf, CancellationToken.None);
            string str = Encoding.UTF8.GetString(buf.Array, 0, r.Count);

            ObjectToSend obj = JsonUtility.FromJson<ObjectToSend>(str);
            if (obj != null)
            {
                if (obj.@namespace != "position")
                    Debug.Log("RECEIVE => " + str);

                if (obj.data == "")
                {
                    string dataJson = str.Substring(str.LastIndexOf('{'));
                    dataJson = dataJson.Remove(dataJson.LastIndexOf('}'));
                    obj.data = dataJson;
                }

                switch (obj.@namespace)
                {
                    case "position":
                        break;

                    case "chat":
                        notificationManager.RequestNewNotification(obj.data, GameMasterNotificationItem.NotificationType.Standard);
                        break;

                    case "email_sent":
                        ReceiveNewMail(obj.data);
                        notificationManager.RequestNewNotification("Un email vient d'arriver", GameMasterNotificationItem.NotificationType.Mail);
                        break;

                    default:
                        break;
                }
            }

            if (isRunning)
                GetWebSocketResponseAsync();
        }
        catch (Exception e)
        {
            Debug.LogError(e.Message);
            return;
        }
    }

    private async void ConnectAsync()
    {
        ws = new ClientWebSocket();
        try
        {
            await ws.ConnectAsync(new Uri("wss://cyberstories.herokuapp.com/ws/"), CancellationToken.None);

            if (ws.State != WebSocketState.Open)
                throw new Exception();

            SendLocationToWebsocketAsync();
            GetWebSocketResponseAsync();
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
            isRunning = false;
            gameObject.SetActive(false);

            if (ws != null)
                await ws.CloseAsync(WebSocketCloseStatus.NormalClosure, string.Empty, CancellationToken.None);
            ws = null;
        }
    }

    private void ReceiveNewMail(String json)
    {
        EmailReceived er = JsonUtility.FromJson<EmailReceived>(json);
        if (er == null)
            return;

        const string senderKey = "GameMaster_mailName";
        string mailObject = er.@object;
        string mailBody = er.message;
        DateTime date = DateTime.Now;

        string objectKey = "GameMaster_" + date.ToString() + "_obj";
        string bodyKey = "GameMaster_" + date.ToString() + "_body";

        GlobalManager.AddNewLocalization(objectKey, mailObject);
        GlobalManager.AddNewLocalization(bodyKey, mailBody);

        mailApp.ReceiveNewMail(senderKey, "Game Master", objectKey, mailObject, bodyKey, mailBody, date);
    }

    // Start is called before the first frame update
    void Start()
    {
        messageToSend = new List<string>();

        ConnectAsync();
    }

    private void OnDestroy()
    {
        if (ws != null)
            ws.CloseAsync(WebSocketCloseStatus.NormalClosure, string.Empty, CancellationToken.None);

        isRunning = false;
    }

    public void MessageRequestToGameMaster(string message)
    {
        messageToSend.Add(message);
    }
}

