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

    public GameObject botLeftPoint;
    public GameObject topRightPoint;
    public GameObject player;

    public MailApplication mailApp;

    private ClientWebSocket ws;

    private bool isRunning = true;

    public GameMasterNotification notificationManager;


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

        //Debug.Log(json);

        await ws.SendAsync(new ArraySegment<byte>(Encoding.UTF8.GetBytes(json)), WebSocketMessageType.Text, true, CancellationToken.None);

        await Task.Delay(500);

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

            notificationManager.RequestNewNotification(str);


            //ReceiveNewMail(str);

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
        ConnectAsync();
    }

    private void OnDestroy()
    {
        if (ws != null)
            ws.CloseAsync(WebSocketCloseStatus.NormalClosure, string.Empty, CancellationToken.None);

        isRunning = false;
    }

}

