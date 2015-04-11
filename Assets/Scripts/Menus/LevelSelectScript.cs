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

        Button thisButton = GameObject.Find("Level2").GetComponent<Button>();
        if (UnlockedLevels < 2)
        {
            thisButton.interactable = false;
            Debug.Log("Level 2 locked " + thisButton.interactable);
        }
	}

    public void SelectLevel1()
    {
        Application.LoadLevel(4);
    }

    public void SelectLevel2()
    {
        Application.LoadLevel(5);
    }

    public void ToMainMenu()
    {
        Application.LoadLevel(0);
    }
}
