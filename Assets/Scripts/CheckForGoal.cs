using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckForGoal : MonoBehaviour
{
    // objects
    private GameObject Player;
    private GameObject Opponent;
    private GameObject TeamGoalie;
    private GameObject OpponentGoalie;
    private GameObject Countdown;
    private GameObject Ring;
    private Rigidbody RingRigidbody;
    private CharacterController cc;

    // positioning
    private Vector3 RingResetPosition = new Vector3(11.2f, 10f, 4.6f);
    private Vector3 PlayerStart = new Vector3(29.75f, 6.03999996f, 4.42000008f);
    private Vector3 OpponentStart = new Vector3(-1.03999996f, 2.38000011f, 4.86000013f);
    private Vector3 PlayerFreeze;
    private Vector3 OpponentFreeze;
    private Vector3 TeamGoalieFreeze;
    private Vector3 OpponentGoalieFreeze;

    // audio
    private AudioSource Audio;
    [SerializeField] private AudioClip GoalHorn;

    // delay variables
    private bool StartDelay = false;
    private float Delay = 7;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("FPSController");
        Opponent = GameObject.Find("AI");
        TeamGoalie = GameObject.Find("TeamGoalie");
        OpponentGoalie = GameObject.Find("OpponentGoalie");
        cc = Player.GetComponent<CharacterController>();

        Ring = GameObject.Find("Ring");
        RingRigidbody = Ring.GetComponent<Rigidbody>();
        
        Audio = GetComponent<AudioSource>();
        Countdown = GameObject.Find("Countdown");
    }

    // Update is called once per frame
    void Update()
    {
        if (StartDelay) {
            if (Delay >= 1)
            {
                Delay -= Time.deltaTime;
                FreezePlayers();
            }
            else
            {
                ResetAfterGoal();
            }
        }
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.name == "Ring") {
            GoalScored();
        }
    }

    void GoalScored() {
        Audio.PlayOneShot(GoalHorn);
        Player.GetComponent<ScoreTracker>().score++;
        RingRigidbody.velocity = Vector3.zero;
        GetFreezePositions();
        StartDelay = true;
    }

    void ResetAfterGoal() {
        StartDelay = false;
        Delay = 5;
        ResetRing();
        ResetPlayers();
        Countdown.GetComponent<CountdownText>().timeRemaining = 4;
    }

    void FreezePlayers() {
        Player.transform.position = PlayerFreeze;
        Opponent.transform.position = OpponentFreeze;
        TeamGoalie.transform.position = TeamGoalieFreeze;
        OpponentGoalie.transform.position = OpponentGoalieFreeze;
        Input.ResetInputAxes();
    }

    void GetFreezePositions()
    {
        PlayerFreeze = Player.transform.position;
        OpponentFreeze = Opponent.transform.position;
        TeamGoalieFreeze = TeamGoalie.transform.position;
        OpponentGoalieFreeze = OpponentGoalie.transform.position;       
    }

    void ResetRing()
    {
        Ring.transform.parent = null;
        Ring.transform.position = RingResetPosition;
        RingRigidbody.velocity = Vector3.zero;       
    }

    void ResetPlayers()
    {
        cc.enabled = false;
        Player.transform.position = PlayerStart;
        Opponent.transform.position = OpponentStart;
        cc.enabled = true;       
    }
}
