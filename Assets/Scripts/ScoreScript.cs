using UnityEngine;
using System.Collections;

public class ScoreScript : MonoBehaviour
{

    public int CurrentScore = 0;

    private GameObject Player;

    public GUIStyle GuiFont;
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
        GUI.TextField(new Rect(Screen.width - 100, 0, 180, 50), "Money: " + CurrentScore.ToString(),GuiFont);   
    }

	// Update is called once per frame
	void Update () {
	    if (Player.GetComponent<Movement>().Health <= 0)
	    {
	        CurrentScore = 0;
	    }
	}
}
