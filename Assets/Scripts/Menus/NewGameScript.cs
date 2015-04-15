using UnityEngine;
using System.Collections;

public class NewGameScript : MonoBehaviour {

    public void Slot1()
    {
        PlayerPrefs.SetInt("CurrentSaveSlot", 1);
        PlayerPrefs.SetInt("Slot" + PlayerPrefs.GetInt("CurrentSaveSlot").ToString() + "UnlockedLevels", 1);
        PlayerPrefs.SetInt("Slot" + PlayerPrefs.GetInt("CurrentSaveSlot").ToString() + "GameActive", 1);
        PlayerPrefs.SetInt("Slot" + PlayerPrefs.GetInt("CurrentSaveSlot").ToString() + "TotalMoney", 0);
        Application.LoadLevel(2);
    }

    public void Slot2()
    {
        PlayerPrefs.SetInt("CurrentSaveSlot", 2);
        PlayerPrefs.SetInt("Slot" + PlayerPrefs.GetInt("CurrentSaveSlot").ToString() + "UnlockedLevels", 1);
        PlayerPrefs.SetInt("Slot" + PlayerPrefs.GetInt("CurrentSaveSlot").ToString() + "GameActive", 1);
        PlayerPrefs.SetInt("Slot" + PlayerPrefs.GetInt("CurrentSaveSlot").ToString() + "TotalMoney", 0);
        Application.LoadLevel(2);
    }
}
