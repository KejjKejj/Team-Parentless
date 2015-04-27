using UnityEngine;
using System.Collections;

public class MenuScript : MonoBehaviour {

    void Awake()
    {
        PlayerPrefs.SetInt("PlayerMaxHealth", 100);

        PlayerPrefs.SetInt("WeaponPriceID0", 0);
        PlayerPrefs.SetInt("WeaponPriceID1", 3000);
        PlayerPrefs.SetInt("WeaponPriceID2", 10000);
    }

    public void ChangeScene(int SceneNr)
    {
        Application.LoadLevel(SceneNr);
    }

    public void Quit()
    {
        Application.Quit();
    }
	
}
