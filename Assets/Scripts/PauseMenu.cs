using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour 
{

    private bool _paused;

    private Canvas _canvasPaused;
    private Image _image;

	// Use this for initialization
	void Start () 
    {
        _canvasPaused = GameObject.Find("Canvas-Pause").GetComponent<Canvas>();
        _canvasPaused.enabled = false;
        _image = GameObject.Find("FadeImage").GetComponent<Image>();
        _image.transform.localScale = new Vector3(Screen.width, Screen.height, 0);
	    Cursor.visible = false;
    }

    void CheckInput()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !_paused)
        {
            Cursor.visible = true;
            _paused = true;
            Time.timeScale = 0;
            _canvasPaused.enabled = true;
            return;
        }
        if (Input.GetKeyDown(KeyCode.Escape) && _paused)
        {
            Cursor.visible = false;
            _paused = false;
            Time.timeScale = 1;
            _canvasPaused.enabled = false;
        }
    }

    public void ToMenuClick()
    {
        Time.timeScale = 1;
        Application.LoadLevel(0);
    }

    public void ToLevelSelectClick()
    {
        Time.timeScale = 1;
        Application.LoadLevel(2);
    }
	
	// Update is called once per frame
	void Update () 
    {
	    CheckInput();
	}
}
