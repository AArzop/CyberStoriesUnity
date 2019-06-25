﻿using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class PlayersGet : MonoBehaviour
{
    public string Response { get; private set; }
    public bool IsError { get; private set; }
    public bool IsLoading { get; private set; }

    void Awake()
    {
        // A correct website page.
        IsLoading = true;
        StartCoroutine(GetRequest("https://cyberstories.herokuapp.com/leaderboard/players/"));
    }

    IEnumerator GetRequest(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            IsLoading = false;

            if (webRequest.isNetworkError)
            {
                Response = webRequest.error;
                IsError = true;
            }
            else
            {
                Response = webRequest.downloadHandler.text;
                IsError = false;
            }
            yield return null;
        }
    }
}
