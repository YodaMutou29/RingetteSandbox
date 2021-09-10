using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountdownText : MonoBehaviour
{
    // players
    public GameObject Player;
    public GameObject Opponent;

    // positioning
    private Vector3 PlayerStart;
    private Vector3 OpponentStart;

    // UI
    private Text text;

    // audio
    private AudioSource Audio;
    public AudioClip Whistle;

    //countdown
    public float timeRemaining = 4;  
    private bool Playing = false;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
        Audio = GetComponent<AudioSource>();
        Player = GameObject.Find("FPSController");
        Opponent = GameObject.Find("AI");
        PlayerStart = Player.transform.position;
        OpponentStart = Opponent.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (timeRemaining > 1) {
            timeRemaining -= Time.deltaTime;
            text.text =  ((int)timeRemaining).ToString();
            FreezePlayers();
            Playing = false;
        } 
        else
        {
            text.text = " ";
            if (!Playing) {
                Audio.PlayOneShot(Whistle);
                Playing = true;
            }
        }
    }

    void FreezePlayers() {
        Player.transform.position = PlayerStart;
        Opponent.transform.position = OpponentStart;
        Input.ResetInputAxes();
    }
}
