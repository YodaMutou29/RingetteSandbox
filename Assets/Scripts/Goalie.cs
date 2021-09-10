using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goalie : MonoBehaviour
{
    // objects
    private GameObject Ring;
    private Rigidbody RingRigidbody;

    // audio
    private AudioSource Audio;
    [SerializeField] private AudioClip SaveSound;

    // positioning
    private Vector3 RingPosition;
    private float LeftGoalPost = 2.43f;
    private float RightGoalPost = 7.05f;

    // other variables
    private float Speed = 2f;

    // Start is called before the first frame update
    void Start()
    {
        Ring = GameObject.Find("Ring");
        RingRigidbody = Ring.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        TrackRing();
    }

    void TrackRing() {
        if (transform.position.z > LeftGoalPost && transform.position.z < RightGoalPost) {
            RingPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, Ring.transform.localPosition.z);
            float step =  Speed * Time.deltaTime; // calculate distance to move
            transform.position = Vector3.MoveTowards(transform.position, RingPosition, step);
        } else if (transform.position.z <= LeftGoalPost) {
            transform.position = new Vector3(transform.localPosition.x, transform.localPosition.y, LeftGoalPost + 1);
        } else {
            transform.position = new Vector3(transform.localPosition.x, transform.localPosition.y, RightGoalPost - 1);
        }        
    }
}
