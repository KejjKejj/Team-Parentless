using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LevelSelectScript : MonoBehaviour
{
    private int SaveSlot;
    private int UnlockedLevels;

    public AudioClip Blip;
    private AudioSource _source;

	// Use this for initialization
	void Start ()
	{
	    _source = gameObject.GetComponent<AudioSource>();
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
        _source.PlayOneShot(Blip);
        PlayerPrefs.SetInt("SelectedLevel", level);
        Application.LoadLevel(5);
    }

    public void WeaponShopButton()
    {
        _source.PlayOneShot(Blip);
        Application.LoadLevel(4);
    }

    public void ToMainMenu()
    {
        _source.PlayOneShot(Blip);
        Application.LoadLevel(0);
    }
}
