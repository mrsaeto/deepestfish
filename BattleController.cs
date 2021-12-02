using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[System.Serializable]
public struct AttackInfo {
    public string name;
    public GameObject attack;
    public float stamina;
}

public class BattleController : MonoBehaviour {
    public static string currentArea = "RiverArea";

    public static GameObject currentFish;
    private GameObject fishInstance;
    private Fish fish;

    public static float battleMaxTime = 15;
    private float battleTime;

    public GameObject playerObject;
    private PlayerController player;

    public GameObject bubbleInstance;
    private List<GameObject> bubbles;

    public GameObject bubbleInstanceLeft;
    public GameObject[] spawnPoints;
    public GameObject[] spawnPointsLeft;

    private float spawnMaxTimeRight = 2f;
    private float spawnMinTimeRight = 0.25f;
    private float spawnTargetTimeRight = 2f;

    private float spawnMaxTimeLeft = 3.5f;
    private float spawnMinTimeLeft = 0.5f;
    private float spawnTargetTimeLeft = 3.5f;

    private float alternateTime = 0.5f;
    private bool alternate = false;
    private bool leftSide = true;
    private bool rightSide = true;

    public Text healthText;
    public Text timerR;

    public void OnLevelWasLoaded(int level) {
        battleTime = battleMaxTime;
        spawnTargetTimeRight = 0.25f;
        spawnTargetTimeLeft = 0.25f;

        player = playerObject.GetComponent<PlayerController>();
        player.health = PlayerController.maxHealth;

        if (level == 7 && currentFish != null) {
            fish = currentFish.GetComponent<Fish>();
            fishInstance = Instantiate(currentFish, new Vector3(0, 2.5f, 0), Quaternion.identity);
        }
    }

	public void Start() {
        bubbles = new List<GameObject>();
	}

	public void Update() {
        spawnTargetTimeRight -= Time.deltaTime;
        spawnTargetTimeLeft -= Time.deltaTime;
        if (spawnTargetTimeRight <= 0.0f && rightSide) {
            if (alternate) {
                rightSide = false;
                leftSide = true;
			}

            int index = Random.Range(0, spawnPoints.Length);
            bubbles.Add(Instantiate(bubbleInstance, spawnPoints[index].transform.position, Quaternion.identity));
            if (spawnMaxTimeRight > spawnMinTimeRight)
                spawnMaxTimeRight -= 0.1f;
            else
                alternate = true;

            if (!alternate)
                spawnTargetTimeRight = spawnMaxTimeRight;
            else
                spawnTargetTimeRight = alternateTime;
        }

        healthText.text = "Health: " + player.health.ToString();
        timerR.text = "Time Remaining: " + ((int)battleTime).ToString();

        if (spawnTargetTimeLeft <= 0.0f && leftSide) {
            if (alternate) {
                rightSide = true;
                leftSide = false;
            }

            int index2 = Random.Range(0, spawnPointsLeft.Length - 1);
            bubbles.Add(Instantiate(bubbleInstanceLeft, spawnPointsLeft[index2].transform.position, Quaternion.identity));
            if (spawnMaxTimeLeft > spawnMinTimeLeft)
                spawnMaxTimeLeft -= 0.1f;

            if (!alternate)
                spawnTargetTimeLeft = spawnMaxTimeLeft;
            else
                spawnTargetTimeLeft = alternateTime;
        }
        
        battleTime -= Time.deltaTime;

        if (battleTime <= 0f){
            Destroy(fishInstance);

            foreach (GameObject b in bubbles) {
                Destroy(b);
			}

            GameController.battleEnded = true;
            SceneManager.LoadScene(currentArea);
        }

    }

}
