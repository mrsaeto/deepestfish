using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{

    public GameObject menuStuff;
    public GameObject creditsUI;
    public GameObject HowToUI;

    void Start(){
        menuStuff.SetActive(true);
        HowToUI.SetActive(false);
        creditsUI.SetActive(false);
    }

    public void PlayGame(){
        SceneManager.LoadScene("RiverArea");
    }

    public void Credits(){
        menuStuff.SetActive(false);
        HowToUI.SetActive(false);
        creditsUI.SetActive(true);
    }

    public void HowTo(){
        menuStuff.SetActive(false);
        creditsUI.SetActive(false);
        HowToUI.SetActive(true);
    }

    public void Quit(){
        Application.Quit();
    }

    void Update(){
        if (Input.GetKeyDown(KeyCode.Escape)){
            Start();
        }
    }
}
