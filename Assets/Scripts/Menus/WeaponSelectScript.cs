using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WeaponSelectScript : MonoBehaviour
{

    public GameObject ScoreText;

    void Start()
    {
        Text text = ScoreText.GetComponent<Text>();
        text.text =
            PlayerPrefs.GetInt("Slot" + PlayerPrefs.GetInt("CurrentSaveSlot").ToString() + "TotalMoney").ToString();

        for (var i = 0; i < 3; ++i)
        {
            Button thisButton = GameObject.Find(i.ToString()).GetComponent<Button>();
            if(PlayerPrefs.GetInt("Slot" + PlayerPrefs.GetInt("CurrentSaveSlot").ToString() + "WeaponUnlockedID" + i) == 0)
                thisButton.interactable = false;
        }
    }

    public void WeaponSelect(int weaponId)
    {
        PlayerPrefs.SetInt("WeaponSelected", weaponId);
        Application.LoadLevel(PlayerPrefs.GetInt("SelectedLevel") + 5);
    }
}
