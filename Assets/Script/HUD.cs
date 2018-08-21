using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HUD : MonoBehaviour {

    public GameObject bagPanel;

    public void LoadSence(string SceneName)
    {
        SceneManager.LoadScene(SceneName);
    }
    public void DelaySetActive(float time)
    {
        StartCoroutine(WaitSeconds(time));  
    }
    IEnumerator WaitSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        bagPanel.SetActive(false);
    }

}
