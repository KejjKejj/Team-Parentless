using UnityEngine;
using System.Collections;

public class NewGameScript : MonoBehaviour {

    public AudioClip Blip;
    private AudioSource _source;

    void Start()
    {
        _source = gameObject.GetComponent<AudioSource>();
    }

    public void Slot1()
    {
        _source.PlayOneShot(Blip);
        PlayerPrefs.SetInt("CurrentSaveSlot", 1);
        PlayerPrefs.SetInt("Slot" + PlayerPrefs.GetInt("CurrentSaveSlot").ToString() + "UnlockedLevels", 1);
        PlayerPrefs.SetInt("Slot" + PlayerPrefs.GetInt("CurrentSaveSlot").ToString() + "GameActive", 1);
        PlayerPrefs.SetInt("Slot" + PlayerPrefs.GetInt("CurrentSaveSlot").ToString() + "TotalMoney", 100000000);
        PlayerPrefs.SetInt("Slot" + PlayerPrefs.GetInt("CurrentSaveSlot").ToString() + "WeaponUnlockedID0", 1);
        PlayerPrefs.SetInt("Slot" + PlayerPrefs.GetInt("CurrentSaveSlot").ToString() + "WeaponUnlockedID1", 0);
        PlayerPrefs.SetInt("Slot" + PlayerPrefs.GetInt("CurrentSaveSlot").ToString() + "WeaponUnlockedID2", 0);
        PlayerPrefs.SetInt("Slot" + PlayerPrefs.GetInt("CurrentSaveSlot").ToString() + "WeaponUnlockedID3", 0);
        PlayerPrefs.SetInt("Slot" + PlayerPrefs.GetInt("CurrentSaveSlot").ToString() + "WeaponUnlockedID4", 0);
        PlayerPrefs.SetInt("Slot" + PlayerPrefs.GetInt("CurrentSaveSlot").ToString() + "WeaponUnlockedID5", 0);
        PlayerPrefs.SetInt("Slot" + PlayerPrefs.GetInt("CurrentSaveSlot").ToString() + "WeaponUnlockedID6", 0);
        Application.LoadLevel(2);
    }

    public void Slot2()
    {
        _source.PlayOneShot(Blip);
        PlayerPrefs.SetInt("CurrentSaveSlot", 2);
        PlayerPrefs.SetInt("Slot" + PlayerPrefs.GetInt("CurrentSaveSlot").ToString() + "UnlockedLevels", 1);
        PlayerPrefs.SetInt("Slot" + PlayerPrefs.GetInt("CurrentSaveSlot").ToString() + "GameActive", 1);
        PlayerPrefs.SetInt("Slot" + PlayerPrefs.GetInt("CurrentSaveSlot").ToString() + "TotalMoney", 0);
        PlayerPrefs.SetInt("Slot" + PlayerPrefs.GetInt("CurrentSaveSlot").ToString() + "WeaponUnlockedID0", 1);
        PlayerPrefs.SetInt("Slot" + PlayerPrefs.GetInt("CurrentSaveSlot").ToString() + "WeaponUnlockedID1", 0);
        PlayerPrefs.SetInt("Slot" + PlayerPrefs.GetInt("CurrentSaveSlot").ToString() + "WeaponUnlockedID2", 0);
        PlayerPrefs.SetInt("Slot" + PlayerPrefs.GetInt("CurrentSaveSlot").ToString() + "WeaponUnlockedID3", 0);
        PlayerPrefs.SetInt("Slot" + PlayerPrefs.GetInt("CurrentSaveSlot").ToString() + "WeaponUnlockedID4", 0);
        PlayerPrefs.SetInt("Slot" + PlayerPrefs.GetInt("CurrentSaveSlot").ToString() + "WeaponUnlockedID5", 0);
        PlayerPrefs.SetInt("Slot" + PlayerPrefs.GetInt("CurrentSaveSlot").ToString() + "WeaponUnlockedID6", 0);
        Application.LoadLevel(2);
    }
}
