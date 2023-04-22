using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AI : Player
{
    // Start is called before the first frame update
    void Start()
    {
        Ring = GameObject.Find("Ring");
        Tip = GameObject.Find("Tip");
        OpponentNet = GameObject.Find("Net");
        RingRigidbody = Ring.GetComponent<Rigidbody>();
        Audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        DistanceFromRing = Vector3.Distance(Ring.transform.position, Tip.transform.position);
        DistanceFromNet = Vector3.Distance(OpponentNet.transform.position, Tip.transform.position);
        Delay -= Time.deltaTime;
        //if cpu is within 2 units of ring, pick up ring
        if (DistanceFromRing < 2 && RingShot == false) {
            StabRing();
        } else {
            HasRing = false;
        }
        if (HasRing) {
            MoveTowardsNet();
            if (DistanceFromNet < 25) {
                Ring.transform.parent = null;
                RingShot = true;
                HasRing = false;
            }
        } else {
            if (RingShot) {
                Shoot();
                StartDelay();
                RingShot = false;
            } else {
                if (Delay < 1) {
                    MoveTowardsRing();
                }
            }
        }
    }

    void Shoot() {
        Ring.transform.parent = null;
        Ring.transform.position = transform.position + transform.forward * 2;
        RingRigidbody.velocity = transform.forward * ShotSpeed;
        HasRing = false; 
    }

    void MoveTowardsRing() {
        // step size
        float step =  speed * Time.deltaTime;
        // move
        transform.position = Vector3.MoveTowards(transform.position, Ring.transform.position, step);
        // rotate
        Vector3.RotateTowards(transform.forward, Ring.transform.position - transform.position, step, 0.0f);
        transform.rotation = Quaternion.LookRotation(Ring.transform.position - transform.position);
        // stay on ground
        transform.position = new Vector3(transform.localPosition.x, 2.59f, transform.localPosition.z);
    }

    void MoveTowardsNet() {
        // step size
        float step =  speed * Time.deltaTime;
        // move
        transform.position = Vector3.MoveTowards(transform.position, OpponentNet.transform.position, step);
        // rotate
        Vector3 LookDirection = Vector3.RotateTowards(transform.forward, OpponentNet.transform.position - transform.position, step, 0.0f);  
        LookDirection.y = 0f;
        transform.rotation = Quaternion.LookRotation(LookDirection);
        // stay on ground
        transform.position = new Vector3(transform.localPosition.x, 2.59f, transform.localPosition.z);
    }

    void StabRing() {
        if (!HasRing && Delay < 1) {
            Audio.PlayOneShot(RingStabSound);
            Delay = 2f;
        }
        Ring.transform.position = Tip.transform.position;
        HasRing = true;   
    }

    void StartDelay() {
        Delay = 5f;
    }
}
