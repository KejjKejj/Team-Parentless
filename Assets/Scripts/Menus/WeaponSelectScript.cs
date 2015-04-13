using UnityEngine;
using System.Collections;

public class WeaponSelectScript : MonoBehaviour {

    public void WeaponSelect(int weaponId)
    {
        PlayerPrefs.SetInt("WeaponSelected", weaponId);
        Application.LoadLevel(PlayerPrefs.GetInt("SelectedLevel") + 4);
    }
}
