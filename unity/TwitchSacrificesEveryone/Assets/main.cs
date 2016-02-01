using System;
using UnityEngine;
using UnityEngine.UI;
using SimpleJSON;
using System.Collections;

public class main: MonoBehaviour {


    public Sprite god1;
    public Sprite god2;
    public Sprite god3;
    public Sprite god4;
    public Sprite god5;
    public Sprite god6;
    public Sprite god7;
    public Sprite god8;

    public ArrayList gods;

    private ClientScript cs;
    private Vector3 leftgoatpos;
    private Vector3 rightgoatpos;

    private Text leftgoatchat;
    private Text rightgoatchat;
    private Text leftgodvotes;
    private Text rightgodvotes;

    private Text leftgoatname;
    private Text rightgoatname;

    private GameObject rngsus = GameObject.Find("RNGsus");
    private bool allowVotes = false;

    private Hashtable goatA;
    private Hashtable goatB;

    private String godA;
    private String godB;

    private GameObject godImageA;
    private GameObject godImageB;

    public float timer = 180; //how many seconds to vote
    private Text timerText;


    private GameObject volcano;
    public String httpserveraddr;
    

    

    // Use this for initialization
    void Start () {

        volcano = GameObject.Find("Volcano");

        GameObject leftGoat = GameObject.Find("left goat");
        leftgoatpos = leftGoat.gameObject.transform.position;

        GameObject rightGoat = GameObject.Find("right goat");
        rightgoatpos = rightGoat.gameObject.transform.position;


        leftgoatchat = GameObject.Find("left goat chat text").GetComponent<Text>();
        rightgoatchat = GameObject.Find("right goat chat text").GetComponent<Text>();
        leftgodvotes = GameObject.Find("Left God Name").GetComponent<Text>();
        rightgodvotes = GameObject.Find("Right God Name").GetComponent<Text>();
        timerText = GameObject.Find("timer").GetComponent<Text>();

        leftgoatname = GameObject.Find("Left Goat Name").GetComponent<Text>();
        rightgoatname = GameObject.Find("Right Goat Name").GetComponent<Text>();

        godImageA = GameObject.Find("LEFT GOD");
        godImageB = GameObject.Find("RIGHT GOD");

        //initialize the socket
        cs = GameObject.Find("ClientScript").GetComponentInChildren<ClientScript>();
        Debug.Log("Starting TCP socket connection");
        cs.socketInit();


        ArrayList allGods = new ArrayList() { "Evi Rubber Duck", "Killer Toast", "Sentient Hairpiece", "Nicolas Mage", "Potato", "L Shark", "R Shark", "Swirly Rock" }; ;
        gods = allGods;


        StartCoroutine(StartGameLoop());       
    }

    void FixedUpdate()
    {
        timer -= Time.deltaTime;
        float timeRem = 180 + timer;
      
        timerText.text = "Time Remaining: "+Math.Round(timeRem)+" Seconds";

        socketRead();

        if(timeRem <= 0)
        {
            StartCoroutine(PlayGameEnd());
        }
    }

    private IEnumerator PlayGameEnd()
    {
       allowVotes = false;
       cs.GET("192.168.1.124:27017/game/stop");

        //ONCE TIMER REACHES ZERO, PLAY EXPLOTION ANIMATION. SACRIFICE THE USER(S) TO THE VOLCANO, REMOVE THE LOSING GOD AND SHOW THE WINNING ONE
        volcano.GetComponent<explosion>().explode = true;

        //DISPLAY THE LEADERBOARD FOR WINNING GODS, AND HOW MANY VOTES (MAYBE COLLECTIVELY?)
       
        //restart game at end
        startOver();
        return null;

    }

    private void startOver() {
        //TELL THE SERVER THAT WE ARE GOING TO START A NEW ROUND BY LOOPING OVER AGAIN, BEGINNING WITH GOD SELECTION

        timer = 180;
        cs.GET("192.168.1.124:27017/game/reset");
        StartCoroutine(StartGameLoop());
    }


    private IEnumerator StartGameLoop()
    {
        String[] goats = null;
        

        //VERY FIRST THING - GET/SELECT THE FIRST TWO GODS, GET THEIR IMAGES IN THE GAMEOBJECTS 
        selectGods(gods);

        //NEXT - GET THE TWO TWITCH USERS. IF NO RESPONSE FROM THE SERVER (OR WE HAVE NO PARTICIPANTS), LOOP UNTIL CONDITIONS ARE MET. DISPLAY RNGESUS WITH THE SELECTED NAMES
        while (goats == null) {
            goats = retrieveGoats();
            yield return new WaitForSeconds(30);
         
        }
         

        //THEN - ASSIGN THE TWO USERS HANDLES TO THE TEXT SPACE ABOVE THE CHARACTERS, LINK THEIR CHAT TO THE TEXT BOX
        //yield return new WaitForSeconds(10);
        setGoats(goats);
        //BEGIN THE COUNTDOWN TIMER. RETRIEVE VOTES AND GOAT CHAT AND UPDATE THE GAME AS NECESSARY;
        
     
    }
    
    private void selectGods(ArrayList gods)
    {
        Sprite[] godimages = new Sprite[] { god1, god2, god3, god4, god5, god6, god7, god8 };
        //choose a god and display it's image in the game
        int godselected = (int)UnityEngine.Random.Range(0, 8);
        int godselected2 = (int)UnityEngine.Random.Range(0, 8);

        while(godselected2 == godselected) godselected2 = (int)UnityEngine.Random.Range(0, 8);

        godImageA.GetComponent<Image>().sprite = godimages[godselected];
        godImageB.GetComponent<Image>().sprite = godimages[godselected2];

        leftgodvotes.text = gods[godselected] + ": 0";
        leftgodvotes.text = gods[godselected2] + ": 0";

        godA = gods[godselected]+"";
        godB = gods[godselected2]+"";






    }

    private String[] retrieveGoats()
    {


        WWW results = cs.GET("192.168.1.124:27017/game/start");
        //split up results as given by server

        String names = results.text + "";
        Debug.Log(names);
        if (names == "null") return null;
        else
        {
            String[] name = names.Split(' ');


            //assign goatA and goatB
            goatA.Add("name", name[0]);
            goatB.Add("name", name[1]);
            //display RNGsus and the chosen names

            Text rngGoatA = GameObject.Find("left goat rng text").GetComponent<Text>();
            Text rngGoatB = GameObject.Find("right goat rng text").GetComponent<Text>();


            rngGoatA.text = (String)goatA["name"];
            rngGoatB.text = (String)goatB["name"];
            rngsus.SetActive(true);
            return name;
        }

    }

    private void setGoats(String[] name)
    {
        //set their names in the game
        leftgoatname.text = name[0];
        rightgoatname.text = name[1];
        //set the votes to begin counting
        rngsus.SetActive(false);
        allowVotes = true;

    }

    private void socketRead()
    {
        
        String str = cs.readSocket();     
        Debug.Log(str);

        if (str != null)
        {
            
            String[] arr = str.Split(':');
            

            if (arr[0] == (String)goatA["name"]) leftgoatchat.text = arr[1]; //update new text to left goat chat bubble
            else if (arr[0] == (String)goatB["name"]) rightgoatchat.text = arr[1]; //update new text to right goat
            else if (arr[0] == "!" + (String)goatA["name"] && allowVotes == true) leftgodvotes.text = godA + ": " + arr[1];  //update vote count for left god
            else if (arr[0] == "!" + (String)goatB["name"] && allowVotes == true) rightgodvotes.text = godB + ": " + arr[1]; //update vote count for right god
            else Debug.Log("Hit default case from TCP data");
        }
       
    }

  

}
