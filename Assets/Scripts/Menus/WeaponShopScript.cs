using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WeaponShopScript : MonoBehaviour
{
    public GameObject ScoreText;

    public int SelectedWeaponId = -1;

	// Use this for initialization
	void Start () {
        for (var i = 0; i < 3; ++i)
        {
            Button thisButton = GameObject.Find(i.ToString()).GetComponent<Button>();
            if (PlayerPrefs.GetInt("Slot" + PlayerPrefs.GetInt("CurrentSaveSlot").ToString() + "WeaponUnlockedID" + i) == 1)
                thisButton.interactable = false;
        }
	}

    public void SetSelectedWeapon(int id)
    {
        SelectedWeaponId = id;
    }

    public void Back()
    {
        Application.LoadLevel(2);
    }

    public void BuyWeapon()
    {
        if (SelectedWeaponId != -1)
        {
            if (PlayerPrefs.GetInt("Slot" + PlayerPrefs.GetInt("CurrentSaveSlot").ToString() + "TotalMoney") >=
                PlayerPrefs.GetInt("WeaponPriceID" + SelectedWeaponId))
            {
                PlayerPrefs.SetInt("Slot" + PlayerPrefs.GetInt("CurrentSaveSlot").ToString() + "TotalMoney", 
                    PlayerPrefs.GetInt("Slot" + PlayerPrefs.GetInt("CurrentSaveSlot").ToString() + "TotalMoney") - (PlayerPrefs.GetInt("WeaponPriceID" + SelectedWeaponId)));

                PlayerPrefs.SetInt(("Slot" + PlayerPrefs.GetInt("CurrentSaveSlot").ToString() + "WeaponUnlockedID" + SelectedWeaponId), 1);

                Button thisButton = GameObject.Find(SelectedWeaponId.ToString()).GetComponent<Button>();
                thisButton.interactable = false;
            }
        }
    }

    void Update()
    {
        Text text = ScoreText.GetComponent<Text>();
        text.text = "$" + 
            PlayerPrefs.GetInt("Slot" + PlayerPrefs.GetInt("CurrentSaveSlot").ToString() + "TotalMoney").ToString();
    }
}
