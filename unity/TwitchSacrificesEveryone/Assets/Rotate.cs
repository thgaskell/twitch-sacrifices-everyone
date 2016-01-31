using UnityEngine;
using System.Collections;

public class Rotate : MonoBehaviour
{
	public float speed = 10f;
	public float x = 0f;
	public float y = 0f;
	public float z = 0f; 

	void Update ()
	{
		transform.Rotate(new Vector3(x,y,z), speed * Time.deltaTime);
	}
}