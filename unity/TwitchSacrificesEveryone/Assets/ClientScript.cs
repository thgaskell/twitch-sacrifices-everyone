using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;

public class ClientScript : MonoBehaviour {

    bool ready = false;
    TcpClient socket;
    NetworkStream stream;
    StreamWriter writer;
    StreamReader reader;
    String hostname = "192.168.1.122";
    Int32 port = 3001;

    //http://answers.unity3d.com/questions/208309/get-request-wrapper.html
    public WWW GET(string url)
	{
		WWW www = new WWW (url);
        StartCoroutine(WaitForRequest(www));
        return www;
    }

    private IEnumerator WaitForRequest(WWW www)
    {
        yield return www;
        if(www.error == null)
        {
            Debug.Log("WWW successful! " + www.text);
        }
        else
        {
            Debug.Log("WWW error " + www.error);
        }
    }

    //referenced http://answers.unity3d.com/questions/15422/unity-project-and-3rd-party-apps.html#answer-15477
    public void socketInit()
    {
        try
        {
            socket = new TcpClient(hostname, port);
            stream = socket.GetStream();
            writer = new StreamWriter(stream);
            reader = new StreamReader(stream);
            ready = true;
            Debug.Log("Socket is ready to go!");
        }
        catch(Exception e)
        {
            Debug.Log("Error setting up the socket: " + e);
        }
    }

    public String readSocket()
    {
        if (!ready)
        {
            Debug.Log("Socket not ready to read.");
            return null;
        }
        try
        {
            String line = reader.ReadLine();
            return line;
        }
        catch (Exception e)
        {
            Debug.Log("Problem reading data: "+e);
            return null;
        }
    }

    public void writeSocket(string str)
    {
        if (!ready)
        {
            Debug.Log("Socket Not Ready to write.");
            return;
        }
        String data = str + "\r\n";
        writer.Write(data);
        writer.Flush();
        Debug.Log(data + "was written to stream successfully!");
    }

    public void closeSocket()
    {
        if (!ready)
        {
            Debug.Log("Could not close socket as it was not ready.");
            return;
        }
        writer.Close();
        reader.Close();
        socket.Close();
        ready = false;
    }


}
