using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {
    private Rigidbody2D body;

    public static float speed = 1.0f;
    private Vector2 movement;

    public static int maxHealth = 3;
    public int health;

    void Start() {
        body = GetComponent<Rigidbody2D>();
    }

    void Update() {
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");

        if (health <= 0) {
            SceneManager.LoadScene(BattleController.currentArea);
		}
    }

	private void FixedUpdate() {
        body.MovePosition(body.position + movement * speed * Time.fixedDeltaTime);
	}

    public void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Attack")) {
            health -= 1;
            Destroy(collision.gameObject);
        }
    }
}
