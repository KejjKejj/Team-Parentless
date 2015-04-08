using UnityEngine;
using System.Collections;

public class MenuScript : MonoBehaviour {

    public void ChangeScene(int SceneNr)
    {
        Application.LoadLevel(SceneNr);
    }
	
}
