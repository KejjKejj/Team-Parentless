using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LoadGameScript : MonoBehaviour
{
    public AudioClip Blip;
    private AudioSource _source;

	// Use this for initialization
	void Start ()
	{
	    _source = gameObject.GetComponent<AudioSource>();
        Button thisButton = GameObject.Find("Slot1").GetComponent<Button>();
	    if (PlayerPrefs.GetInt("Slot1GameActive") != 1)
	    {
	        thisButton.interactable = false;
	    }
        thisButton = GameObject.Find("Slot2").GetComponent<Button>();
        if (PlayerPrefs.GetInt("Slot2GameActive") != 1)
        {
            thisButton.interactable = false;
        }
	}

    public void Slot1()
    {
        _source.PlayOneShot(Blip);
        PlayerPrefs.SetInt("CurrentSaveSlot", 1);
        Application.LoadLevel(2);
    }

    public void Slot2()
    {
        _source.PlayOneShot(Blip);
        PlayerPrefs.SetInt("CurrentSaveSlot", 2);
        Application.LoadLevel(2);
    }
}
