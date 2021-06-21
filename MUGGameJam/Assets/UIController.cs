using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public GameObject deathPanel;
    public GameObject menuPanel;
    public Text infoText;

    public void Death(float time)
    {

        deathPanel.SetActive(true);

        int mins = Mathf.FloorToInt(time / 60);
        int secs = Mathf.FloorToInt(time) - mins * 60;

        infoText.text = "oops! you're dead!\n" + "your time: " + mins + ":" + secs;


    }

    public void Restart()
    {
        SceneManager.LoadScene(1);
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene(0);
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            menuPanel.SetActive(!menuPanel.active);
    }
}
