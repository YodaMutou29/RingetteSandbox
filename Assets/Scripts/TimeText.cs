using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TimeText : MonoBehaviour
{
    public GameObject gametime;
    public float timeRemaining = 300;
    private Text text;
    private AudioSource buzzerSound;

    void Awake() {
		buzzerSound = gametime.GetComponents<AudioSource>()[0];
	}

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (timeRemaining > 0) {
            timeRemaining -= Time.deltaTime;
            if ((int)timeRemaining % 60 > 9) {
                text.text =  ((int)(timeRemaining / 60)).ToString() + ":" + ((int)timeRemaining % 60).ToString();
            } else {
                text.text =  ((int)(timeRemaining / 60)).ToString() + ":0" + ((int)timeRemaining % 60).ToString();
            }
        } else {
            SceneManager.LoadScene("Game Over");
        }
    }
}
