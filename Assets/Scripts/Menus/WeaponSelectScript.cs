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
    }

    public void WeaponSelect(int weaponId)
    {
        PlayerPrefs.SetInt("WeaponSelected", weaponId);
        Application.LoadLevel(PlayerPrefs.GetInt("SelectedLevel") + 4);
    }
}
