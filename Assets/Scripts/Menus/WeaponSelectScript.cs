using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WeaponSelectScript : MonoBehaviour
{

    public AudioClip Blip;
    private AudioSource _source;

    void Start()
    {
        _source = gameObject.GetComponent<AudioSource>();
        for (var i = 0; i < 7; ++i)
        {
            Button thisButton = GameObject.Find(i.ToString()).GetComponent<Button>();
            if(PlayerPrefs.GetInt("Slot" + PlayerPrefs.GetInt("CurrentSaveSlot").ToString() + "WeaponUnlockedID" + i) == 0)
                thisButton.interactable = false;
        }
    }

    public void WeaponSelect(int weaponId)
    {
        _source.PlayOneShot(Blip);
        PlayerPrefs.SetInt("WeaponSelected", weaponId);
        Application.LoadLevel(PlayerPrefs.GetInt("SelectedLevel") + 5);
    }

    public void LevelSelectClick()
    {
        _source.PlayOneShot(Blip);
        Application.LoadLevel(2);
    }
}
