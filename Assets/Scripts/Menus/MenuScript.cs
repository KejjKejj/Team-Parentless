using UnityEngine;
using System.Collections;

public class MenuScript : MonoBehaviour {

    void Awake()
    {
        PlayerPrefs.SetInt("PlayerMaxHealth", 100);
    }

    public void ChangeScene(int SceneNr)
    {
        Application.LoadLevel(SceneNr);
    }

    public void Quit()
    {
        Application.Quit();
    }
	
}
