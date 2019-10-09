﻿using System;
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
    private class Message
    {
        public string message;

        public Message(string message) { this.message = message; }
    }

    [Serializable]
    private class ObjectToSend
    {
        public string type;
        public string objJson;

        public ObjectToSend(string type, string objJson) { this.type = type; this.objJson = objJson; }
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
        float x = ComputeLocation(botLeftPoint.transform.position.x, topRightPoint.transform.position.x, player.transform.position.x) * 600;
        float y = (1 - ComputeLocation(botLeftPoint.transform.position.z, topRightPoint.transform.position.z, player.transform.position.z)) * 600;

        Position p = new Position(x, y);
        string json = JsonUtility.ToJson(p).ToString();

        ObjectToSend o = new ObjectToSend("Position", json);
        json = JsonUtility.ToJson(o).ToString();
        Debug.Log(json);

        await ws.SendAsync(new ArraySegment<byte>(Encoding.UTF8.GetBytes(json)), WebSocketMessageType.Text, true, CancellationToken.None);

        await Task.Delay(500);

        if (isRunning)
            SendLocationToWebsocketAsync();
    }

    private async void SendMessageToGameMasterAsync()
    {
        if (messageToSend.Count != 0)
        {
            string message = messageToSend[0];
            messageToSend.RemoveAt(0);

            Message m = new Message(message);
            string json = JsonUtility.ToJson(m).ToString();

            ObjectToSend o = new ObjectToSend("Chat", json);
            await ws.SendAsync(new ArraySegment<byte>(Encoding.UTF8.GetBytes(JsonUtility.ToJson(o).ToString())), WebSocketMessageType.Text, true, CancellationToken.None);
        }

        await Task.Delay(1000);

        if (isRunning)
            SendMessageToGameMasterAsync();
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
                switch (obj.type)
                {
                    case "Position":
                        break;

                    case "Chat":
                        notificationManager.RequestNewNotification(str, GameMasterNotificationItem.NotificationType.Standard);
                        break;

                    case "Mail":
                        //ReceiveNewMail(str);
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
            //await ws.ConnectAsync(new Uri("wss://cyberstories.herokuapp.com/ws/game-master/"), CancellationToken.None);
            await ws.ConnectAsync(new Uri("wss://echo.websocket.org"), CancellationToken.None);

            if (ws.State != WebSocketState.Open)
                throw new Exception();

            SendLocationToWebsocketAsync();
            SendMessageToGameMasterAsync();
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

    private void ReceiveNewMail(String input)
    {
        // Parse input
        string str = input.Substring(28, input.Length - 28);

        int messageBegin = str.IndexOf("\\\"") + 19;

        string obj = str.Substring(0, str.IndexOf("\\\""));
        string message = str.Substring(messageBegin, str.Length - messageBegin - 5);

        Debug.Log(obj);
        Debug.Log(message);

        const string senderKey = "GameMaster_mailName";
        string mailObject = "";
        string mailBody = "";
        DateTime date = DateTime.Now;

        string objectKey = "GameMaster_" + date.ToString() + "_obj";
        string bodyKey = "GameMaster_" + date.ToString() + "_body";

        GlobalManager.AddNewLocalization(objectKey, mailObject);
        GlobalManager.AddNewLocalization(bodyKey, mailBody);

        mailApp.ReceiveNewMail(senderKey, "Game Master", objectKey, obj, bodyKey, message, date);
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

