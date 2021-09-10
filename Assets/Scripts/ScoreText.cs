using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreText : MonoBehaviour
{
    private Text text;
    public GameObject Player;
    public GameObject AI;
    public static int PlayerScore;
    public static int AIScore;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find ("FPSController");
        AI = GameObject.Find ("AI");
        text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerScore = Player.GetComponent<ScoreTracker>().score;
        AIScore = AI.GetComponent<ScoreTracker>().score;
        text.text = PlayerScore + " | " + AIScore;
    }
}
