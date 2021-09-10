using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Crease : MonoBehaviour
{
    // objects
    private GameObject Ring;
    private GameObject PlayerController;
    private GameObject PlayerTip;
    private GameObject OpponentTip;
    private GameObject Opponent;
    private GameObject TeamGoalie;
    private GameObject OpponentGoalie;

    // positioning
    private Vector3 RingResetPosition = new Vector3(11.2f, 10f, 4.6f);
    private Vector3 PlayerControllerStart;
    private Vector3 OpponentStart = new Vector3(-1.03999996f, 2.38000011f, 4.86000013f);
    private Vector3 PlayerControllerFreeze;
    private Vector3 OpponentFreeze;
    private Vector3 TeamGoalieFreeze;
    private Vector3 OpponentGoalieFreeze;

    // audio
    private AudioSource Audio;
    [SerializeField] private AudioClip Whistle;

    // other
    private Text text;
    private float Delay = 0;
    private bool CreaseViolated = false;
    private CharacterController cc;

    // Start is called before the first frame update
    void Start()
    {
        PlayerController = GameObject.Find("FPSController");
        PlayerTip = GameObject.Find("PlayerTip");
        Opponent = GameObject.Find("AI");
        OpponentTip = GameObject.Find("Tip");
        TeamGoalie = GameObject.Find("TeamGoalie");
        OpponentGoalie = GameObject.Find("OpponentGoalie");
        
        PlayerControllerStart = new Vector3(29.75f, 6.03999996f, 4.42000008f);
        cc = PlayerController.GetComponent<CharacterController>();

        text = GameObject.Find("Violation").GetComponent<Text>();
        Ring = GameObject.Find("Ring");
        Audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Delay -= Time.deltaTime;

        if (CreaseViolated)
        {
            CreaseViolation();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name != "Ring" 
            && other.gameObject.name != "TeamGoalie"
            && other.gameObject.name != "OpponentGoalie") 
        {
            Audio.PlayOneShot(Whistle);
            CreaseViolated = true;
            GetFreezePositions();
            Delay = 3;
        }
    }

    void CreaseViolation()
    {
        if (Delay > 0)
        {
            FreezePlayers();
            text.text = "Crease Violation";
        }
        else
        {
            text.text = " ";
            if (CreaseViolated)
            {
                ResetRing();
                ResetPlayers();
                GameObject.Find("Countdown").GetComponent<CountdownText>().timeRemaining = 4;
                CreaseViolated = false;
            }
        }
    }

    void FreezePlayers() 
    {
        PlayerController.transform.position = PlayerControllerFreeze;
        Opponent.transform.position = OpponentFreeze;
        TeamGoalie.transform.position = TeamGoalieFreeze;
        OpponentGoalie.transform.position = OpponentGoalieFreeze;
        Input.ResetInputAxes();
    }

    void GetFreezePositions()
    {
        PlayerControllerFreeze = PlayerController.transform.position;
        OpponentFreeze = Opponent.transform.position;
        TeamGoalieFreeze = TeamGoalie.transform.position;
        OpponentGoalieFreeze = OpponentGoalie.transform.position;       
    }

    void ResetRing() 
    {
        PlayerTip.GetComponent<PickupRing>().HasRing = false;
        Opponent.GetComponent<AI>().HasRing = false;
        Ring.transform.parent = null;
        Ring.transform.position = RingResetPosition;
        Ring.GetComponent<Rigidbody>().velocity = Vector3.zero;
    }

    void ResetPlayers()
    {
        cc.enabled = false;
        PlayerController.transform.position = PlayerControllerStart;
        Opponent.transform.position = OpponentStart;
        cc.enabled = true;
    }
}
