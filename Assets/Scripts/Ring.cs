using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ring : MonoBehaviour
{
    // objects
    private Rigidbody RingRigidbody;
    
    // audio
    private AudioSource Audio;
    public AudioClip OutOfBounds;
    public AudioClip BoardsHit;

    // positioning and dimensions
    private Vector3 RingDropPosition = new Vector3(11.2f, 10f, 4.6f);
    private float SideWallLeft = -24.1f;
    private float SideWallRight = 33.8f;
    private float ForwardWall = -50f;
    private float BackWall = 69.9f;
    private float BoardsHeight = 7.79f;

    // Start is called before the first frame update
    void Start()
    {
        RingRigidbody = GetComponent<Rigidbody>();
        Audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.localPosition.z < SideWallLeft 
        || transform.localPosition.z > SideWallRight 
        || transform.localPosition.x < ForwardWall 
        || transform.localPosition.x > BackWall) {
            if (transform.localPosition.y > BoardsHeight) {
                Audio.PlayOneShot(OutOfBounds);
            } else {
                Audio.PlayOneShot(BoardsHit);
                RingRigidbody.velocity = -(RingRigidbody.velocity);
            }
            RingRigidbody.velocity = Vector3.zero;
            transform.position = RingDropPosition;
        }
    }
}
