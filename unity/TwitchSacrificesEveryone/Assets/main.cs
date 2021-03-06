﻿using System;
using UnityEngine;
using SimpleJSON;
using System.Collections;

public class main : MonoBehaviour {

    private ClientScript cs;

	// Use this for initialization
	void Start () {

        StartGameLoop();       
	}

    private void StartGameLoop()
    {

        cs = GameObject.Find("ClientScript").GetComponentInChildren<ClientScript>();
        //WWW results = cs.GET("http://answers.unity3d.com/questions/208309/get-request-wrapper.html");
        // Debug.Log(results.text);
        //depending on the data we are polling, we will be making different get requests

        //  var N = JSON.Parse(results.text);

        //two get requests -> one to request for new names to sacrifice and one for a status check and confirmation to the server
        Debug.Log("Starting TCP socket connection");
        cs.socketInit();
        // cs.writeSocket("test!");

            String str = cs.readSocket();
            while (str == null)
            {
                Debug.Log("str is null");
                str = cs.readSocket();
            }
            Debug.Log(str);
        
        cs.closeSocket();
    }

    //need to make a simple state machine which keeps tab on the time and is able to reset the scene after a sacrifice has been made.

}
