using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : MonoBehaviour
{
    // objects
    protected GameObject Ring;
    protected GameObject Tip;
    protected GameObject OpponentNet;
    protected Rigidbody RingRigidbody;

    // audio
    protected AudioSource Audio;
    [SerializeField] protected AudioClip RingStabSound;

    // speeds
    [SerializeField] protected float speed = 7.0f;
    [SerializeField] protected float ShotSpeed = 200f;

    // booleans
    public bool HasRing = false;
    protected bool RingShot = false;

    // delay variables
    protected float Delay = 3f;

    protected float DistanceFromRing;
    protected float DistanceFromNet;
}