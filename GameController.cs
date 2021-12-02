using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[System.Serializable]
public struct WaitRange {
    public float minTime;
    public float maxTime;
}

public class GameController : MonoBehaviour {

    public static GameController instance;
    public GameObject regionController;
    private Animator animator;

    private bool casted = false;
    public WaitRange castWaitRange;
    private float castWaitTime = 0.0f;
    private float smallTimer;

    public static bool battleEnded;

    private bool found;
    public GameObject foundAlert;
    public float foundReactTime = 0.0f;
    private float foundTime = 0.0f;

    private bool cooldown;
    public float cooldownMaxTime = 0.0f;
    private float cooldownTime = 0.0f;

    public static int money;

    //UI stuff -James
    public Text moneyText;
    public Text fishCaughtText;
    public GameObject fishCaughtObj;
    public static bool gameIsPaused = false;
    public GameObject pauseMenuUI;
    public GameObject newBoatUI;
    public GameObject boatFlyerUi;

    public GameObject fishObject;

    public void Awake(){
        instance = this;
    }

    public void Start() {
        newBoatUI.SetActive(true);
        boatFlyerUi.SetActive(false);
        animator = GetComponent<Animator>();
        
        //ui stuff
        fishCaughtObj.SetActive(false);
        pauseMenuUI.SetActive(false);
        gameIsPaused = false;

        foundAlert.SetActive(false);
        foundTime = foundReactTime;
        cooldownTime = cooldownMaxTime;

        moneyText.text = "$" + money.ToString();
	}

    public void Update() {

        moneyText.text = "$" + money.ToString();

        if (Input.GetKeyDown("space") && !casted && !found && !cooldown) {
            castWaitTime = Random.Range(castWaitRange.minTime, castWaitRange.maxTime);
            smallTimer = 0.05f;

            foundAlert.SetActive(false);
            FindObjectOfType<AudioManager>().Play("cast1");

            fishCaughtObj.SetActive(false);
            if (fishObject != null) {
                Destroy(fishObject);
			}
            
            casted = true;
            animator.Play("Trevor_Cast");
        }

        if (casted) {
            castWaitTime -= Time.deltaTime;
            if (castWaitTime <= 0.0f) {
                found = true;
                casted = false;
            }

            smallTimer -= Time.deltaTime;
            if (Input.GetKeyDown("space") && smallTimer <= 0.0f) {
                casted = false;
                cooldown = true;
                animator.Play("Trevor_Idle");
            }
		}

        if (found) {
            foundAlert.SetActive(true);
            animator.Play("Trevor_Catch");

            if (Input.GetKeyDown("space")) {
                foundAlert.SetActive(false);//Gets rid of the fish text when you cast
                
                found = false;

                RegionController rc = regionController.GetComponent<RegionController>();
                BattleController.currentFish = rc.getFish();

                SceneManager.LoadScene("BattleScene");              
            }

            foundTime -= Time.deltaTime;
            if (foundTime <= 0.0f) {
                cooldown = true;
                foundAlert.SetActive(false);
                found = false;
                animator.Play("Trevor_Idle");
            }
        } else {
            foundTime = foundReactTime;
		}

        if (cooldown) {
            cooldownTime -= Time.deltaTime;
            if (cooldownTime <= 0.0f) {
                cooldown = false;
                cooldownTime = cooldownMaxTime;
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape)){
            if (gameIsPaused){
                Resume();
            }else{
                Pause();
            }

        }

        if (battleEnded) {
            battleEnded = false;
            fishCaughtUI();
        }
    }

    //UI STUFF
    public void Resume(){
        pauseMenuUI.SetActive(false);
        boatFlyerUi.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
    }

    public void Pause(){
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
    }

    public void Quit(){
        Debug.Log("Game has been quit.");
        Application.Quit();
    }
    public void fishCaughtUI(){
        fishObject = Instantiate(BattleController.currentFish);
        Fish fish = fishObject.GetComponent<Fish>();

        fishCaughtObj.SetActive(true);
        fishCaughtText.text = "You caught a " + fish.fishName + " worth $" + fish.baseValue +"!";
        money += (int)fish.baseValue;
        moneyText.text = "$" + money.ToString();
    }

    public void newBoatUi(){
        if (gameIsPaused){
            Resume();
        }else{
            boatFlyerUi.SetActive(true);
            gameIsPaused = true;
            Time.timeScale = 0f;
        }

    }

    public void newBoatPurchase1(){
        if(money >= 100){
            BattleController.battleMaxTime += 10;
            PlayerController.speed += 0.5f;
            money -= 100;
            moneyText.text = "$" + money.ToString();
            pauseMenuUI.SetActive(false);
            boatFlyerUi.SetActive(false);
            Time.timeScale = 1f;
            gameIsPaused = false;
            BattleController.currentArea = "LakeArea";
            SceneManager.LoadScene("LakeArea");
        }
    }

    public void newBoatPurchase2(){
        if(money >= 1000){
            BattleController.battleMaxTime += 20;
            PlayerController.speed += 0.5f;
            PlayerController.maxHealth += 1;
            money -= 1000;
            moneyText.text = "$" + money.ToString();
            pauseMenuUI.SetActive(false);
            boatFlyerUi.SetActive(false);
            Time.timeScale = 1f;
            gameIsPaused = false;
            BattleController.currentArea = "BeachArea";
            SceneManager.LoadScene("BeachArea");
        }
    }

    public void newBoatPurchase3(){
        if(money >= 10000){
            BattleController.battleMaxTime += 10;
            PlayerController.speed += 0.5f;
            money -= 10000;
            moneyText.text = "$" + money.ToString();
            pauseMenuUI.SetActive(false);
            boatFlyerUi.SetActive(false);
            Time.timeScale = 1f;
            gameIsPaused = false;
            BattleController.currentArea = "DeepOcean";
            SceneManager.LoadScene("DeepOcean");
        }
    }

    public void newBoatPurchase4(){
        if(money >= 100000){
            BattleController.battleMaxTime += 10;
            PlayerController.speed += 0.5f;
            PlayerController.maxHealth += 1;
            money -= 100000;
            moneyText.text = "$" + money.ToString();
            pauseMenuUI.SetActive(false);
            boatFlyerUi.SetActive(false);
            Time.timeScale = 1f;
            gameIsPaused = false;
            BattleController.currentArea = "DeeperOcean";
            SceneManager.LoadScene("DeeperOcean");
        }
    }

    public void newBoatPurchase5(){
        if(money >= 1000000){
            BattleController.battleMaxTime += 10;
            PlayerController.speed += 0.5f;
            money -= 1000000;
            moneyText.text = "$" + money.ToString();
            pauseMenuUI.SetActive(false);
            boatFlyerUi.SetActive(false);
            Time.timeScale = 1f;
            gameIsPaused = false;
            BattleController.currentArea = "SpaceArea";
            SceneManager.LoadScene("SpaceArea");
        }
    }
}
