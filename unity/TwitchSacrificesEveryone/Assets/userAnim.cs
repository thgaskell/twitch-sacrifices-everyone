using UnityEngine;
using System.Collections;

public class userAnim : MonoBehaviour {

    public bool sacrifice = false;
    public bool animDelay = false;
    public GameObject chatbox;
    private int num = 0;
    private bool goUp = true;
    private bool run = false;

    void Start()
    {
        if (animDelay) num = -3;
    }

    void FixedUpdate()
    {
        if (run == false)
        {
            StartCoroutine(suspend());
            run = true;
        }
    }


    private IEnumerator suspend()
    {
        while (sacrifice == false)
        {
            if (goUp == true)
            {
                gameObject.transform.position = gameObject.transform.position + Vector3.up / 120;
                num++;
                if (num == 35)
                {
                    goUp = false;
                    yield return new WaitForSeconds(0.1f);
                }

            }
            else if (goUp == false)
            {
                gameObject.transform.position = gameObject.transform.position + Vector3.down / 120;
                num--;
                if (num == -35)
                {
                    goUp = true;
                    yield return new WaitForSeconds(0.1f);
                }
            }
            yield return new WaitForSeconds(0.04f);
        }
        num = 0;

        while (sacrifice == true)
        {
            if (chatbox) chatbox.transform.position = chatbox.gameObject.transform.position + Vector3.down / 80;
            gameObject.transform.position = gameObject.transform.position + Vector3.down / 80;
            yield return new WaitForSeconds(0.01f);
            num++;

            if(num > 100)
            {
                chatbox.gameObject.SetActive(false);
            }

            if(num > 1000)
            {
                break;
            }
        }
    }
    
}
