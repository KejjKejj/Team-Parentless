using UnityEngine;
using System.Collections;

public class MenuScript : MonoBehaviour
{
    public AudioClip Blip;
    private AudioSource _source;

    void Awake()
    {
        PlayerPrefs.SetInt("PlayerMaxHealth", 100);

        PlayerPrefs.SetInt("WeaponPriceID0", 0);
        PlayerPrefs.SetInt("WeaponPriceID1", 3000);
        PlayerPrefs.SetInt("WeaponPriceID2", 5000);
        PlayerPrefs.SetInt("WeaponPriceID3", 3000);
        PlayerPrefs.SetInt("WeaponPriceID4", 8000);
        PlayerPrefs.SetInt("WeaponPriceID5", 6500);
        PlayerPrefs.SetInt("WeaponPriceID6", 10000);
    }

    void Start()
    {
        _source = gameObject.GetComponent<AudioSource>();
    }

    public void ChangeScene(int SceneNr)
    {
        _source.PlayOneShot(Blip);
        Application.LoadLevel(SceneNr);
    }

    public void Quit()
    {
        Application.Quit();
    }
	
}
