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

        // Send
        Debug.Log("x -> " + x + "   y -> " + y);

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

            //Start Coroutine
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

    // Start is called before the first frame update
    void Start()
    {
        ConnectAsync();
    }

    // Update is called once per frame
    void Update()
    {}
}
