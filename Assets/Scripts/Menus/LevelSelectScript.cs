using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LevelSelectScript : MonoBehaviour
{
    private int SaveSlot;
    private int UnlockedLevels;

	// Use this for initialization
	void Start ()
	{
	    SaveSlot = PlayerPrefs.GetInt("CurrentSaveSlot");
	    UnlockedLevels = PlayerPrefs.GetInt("Slot" + SaveSlot.ToString() + "UnlockedLevels");

	    for (var i = 0; i < UnlockedLevels; ++i)
	    {
            Button thisButton = GameObject.Find("Level" + (i + 1).ToString()).GetComponent<Button>();
	        thisButton.interactable = true;
	    }
	}

    public void SelectLevel(int level)
    {
        PlayerPrefs.SetInt("SelectedLevel", level);
        Application.LoadLevel(4);
    }

    public void ToMainMenu()
    {
        Application.LoadLevel(0);
    }
}
