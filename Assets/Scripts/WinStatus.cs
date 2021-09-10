using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinStatus : MonoBehaviour
{
    private Text text;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (ScoreText.PlayerScore > ScoreText.AIScore) {
            text.text = "YOU WIN!";
        } else if (ScoreText.PlayerScore < ScoreText.AIScore) {
            text.text = "YOU LOSE!";
        } else {
            text.text = "IT'S A TIE!";
        }
    }
}
