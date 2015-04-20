using UnityEngine;
using System.Collections;

public class ScoreScript : MonoBehaviour
{

    public int CurrentScore = 0;

    private GameObject Player;

	// Use this for initialization
	void Start ()
	{
	    CurrentScore = 0;
	    Player = GameObject.FindGameObjectWithTag("Player");
	}

    void ApplyScore(int score)
    {
        CurrentScore += score;
    }

    void OnGUI()
    {     
        GUI.TextField(new Rect(1800, 100, 100, 20), "Money: " + CurrentScore.ToString());   
    }

	// Update is called once per frame
	void Update () {
	    if (Player.GetComponent<Movement>().Health <= 0)
	    {
	        CurrentScore = 0;
	    }
	}
}
