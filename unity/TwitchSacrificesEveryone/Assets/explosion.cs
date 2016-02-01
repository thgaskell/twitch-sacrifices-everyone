using UnityEngine;
using System.Collections;

public class explosion : MonoBehaviour {

    //number of seconds before explosion goes off
    public float time = 5;
    private GameObject heat;
    private GameObject lavaFlow;
    private GameObject lavaExplosion;
    private GameObject cam;
    private GameObject leftgoat;
    private GameObject rightgoat;
    private Vector3 camOrigin;

    public bool explode = false;
    

	// Use this for initialization
	void Start () {
        heat = GameObject.Find("heat");
        lavaFlow = GameObject.Find("lava flow");
        lavaExplosion = GameObject.Find("lava explosion");
        cam = GameObject.Find("Main Camera");
        leftgoat = GameObject.Find("left goat");
        rightgoat = GameObject.Find("right goat");
        camOrigin = cam.gameObject.transform.position;

        lavaFlow.gameObject.SetActive(false);
        lavaExplosion.gameObject.SetActive(false);

       


	}
	
	// Update is called once per frame
	void Update () {

        if(explode == true)
        {
            StartCoroutine(SetExplosion());
        }

	}

    private IEnumerator SetExplosion()
    {
 
        
        userAnim lganim = leftgoat.gameObject.GetComponent<userAnim>();
        userAnim rganim = rightgoat.gameObject.GetComponent<userAnim>();
        lganim.sacrifice = true;
        rganim.sacrifice = true;
        StartCoroutine(explodeVolcano());
        return null;

    }

    private IEnumerator explodeVolcano()
    {
        GameObject chats = GameObject.Find("Goat Chat");
        chats.SetActive(false);
        cam.gameObject.transform.position = cam.gameObject.transform.position + Vector3.forward*5;
        yield return new WaitForSeconds(2);
        StartCoroutine(shakeCam());
        yield return new WaitForSeconds(2);
        heat.gameObject.SetActive(false);
        lavaFlow.gameObject.SetActive(true);
        lavaExplosion.gameObject.SetActive(true);
        yield return new WaitForSeconds(17); //2+17 = 19 seconds is roughly how long it takes to finish explosion
        heat.gameObject.SetActive(true);
        //volcano is now reset
    }

    private IEnumerator shakeCam()
    {
        for (int i = 0; i < 35; i++)
        {
            Vector3 newX1 = camOrigin + Vector3.right/4;
            cam.gameObject.transform.position = newX1;
            yield return new WaitForSeconds(0.02f);
            Vector3 newX2 = camOrigin + Vector3.left/4;
            cam.gameObject.transform.position = newX2;
            yield return new WaitForSeconds(0.02f);
        }
         cam.gameObject.transform.position = camOrigin;
    }

}
