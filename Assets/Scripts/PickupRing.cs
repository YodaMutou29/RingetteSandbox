using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof (AudioSource))]
public class PickupRing : MonoBehaviour
{
    // objects
    public GameObject Player;
    public Component Camera;
    public GameObject Ring;
    public GameObject Tip;
    public GameObject OpponentNet;
    public Rigidbody RingRigidbody;

    // audio
    private AudioSource Audio;
    [SerializeField] private AudioClip ShotSound;
    [SerializeField] private AudioClip RingStabSound;

    // positioning
    private Vector3 RingStartPosition;
    private Quaternion RingRotation;
    private Quaternion CameraRotation;

    // other variables
    public float shotSpeed = 100f;
    public bool HasRing = false;

    // Start is called before the first frame update
    void Start()
    {
        Ring = GameObject.Find("Ring");
        OpponentNet = GameObject.Find("Net2");
        Player = GameObject.Find("FirstPersonCharacter");
        Camera = Player.GetComponent<Camera>();
        Audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space")) {
            if (Vector3.Distance(Ring.transform.position, Tip.transform.position) < 5) {
                ToggleRingStab();
            }
        }
        if (HasRing) {
            Ring.transform.position = Tip.transform.position;
            if (Input.GetKeyDown("e")) {
                Audio.PlayOneShot(ShotSound);
                Shoot();
            }
        }
    }

    void Shoot() 
    {
        Ring.transform.parent = null;
        Ring.transform.position = transform.position + Camera.transform.forward * 2;
        RingRigidbody.velocity = Camera.transform.forward * 200;
        HasRing = false; 
    }

    void ToggleRingStab() 
    {
        if (!HasRing) {
            Audio.PlayOneShot(RingStabSound);
        }
        HasRing = !HasRing;
    }
}
