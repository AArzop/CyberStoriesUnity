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
    public GameObject botLeftPoint;
    public GameObject topRightPoint;
    public GameObject player;

    private MailApplication mailApp;

    private ClientWebSocket ws;
    private bool isRunning = true;


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
        float x = ComputeLocation(botLeftPoint.transform.position.x, topRightPoint.transform.position.x, player.transform.position.x);
        float y = ComputeLocation(botLeftPoint.transform.position.z, topRightPoint.transform.position.z, player.transform.position.z);

        await ws.SendAsync(new ArraySegment<byte>(Encoding.UTF8.GetBytes("x:" + x + " y:" + y)), WebSocketMessageType.Text, true, CancellationToken.None);

        await Task.Delay(2000);

        SendLocationToWebsocketAsync();
    }

    private async void GetWebSocketResponseAsync()
    {
        ArraySegment<byte> buf = new ArraySegment<byte>(new byte[1024]);

        WebSocketReceiveResult r = await ws.ReceiveAsync(buf, CancellationToken.None);
        Debug.Log("Got: " + Encoding.UTF8.GetString(buf.Array, 0, r.Count));
        GetWebSocketResponseAsync();
    }

    private async void ConnectAsync()
    {
        ws = new ClientWebSocket();
        try
        {
            await ws.ConnectAsync(new Uri("wss://echo.websocket.org"), CancellationToken.None);
            if (ws.State == WebSocketState.Open)
                Debug.Log("connected");

            mailApp = GameObject.FindObjectOfType<MailApplication>();

            SendLocationToWebsocketAsync();
            GetWebSocketResponseAsync();

        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
            isRunning = false;
            gameObject.SetActive(false);
        }
    }

    private void ReceiveNewMail(String input)
    {
        // Parse input

        const string senderKey = "GameMaster_mailName";
        string mailObject = "";
        string mailBody = "";
        DateTime date = DateTime.Now;

        string objectKey = "GameMaster_" + date.ToString() + "_obj";
        string bodyKey = "GameMaster_" + date.ToString() + "_body";

        GlobalManager.AddNewLocalization(objectKey, mailObject);
        GlobalManager.AddNewLocalization(bodyKey, mailBody);

        mailApp.ReceiveNewMail(senderKey, objectKey, bodyKey, date);
    }

    // Start is called before the first frame update
    void Start()
    {
        ConnectAsync();
    }
}
