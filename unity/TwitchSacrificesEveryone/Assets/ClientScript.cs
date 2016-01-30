using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ClientScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public WWW GET(string url)
	{
		WWW www = new WWW (url);
		StartCoroutine(
	}
}
