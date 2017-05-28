using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class duckMovement : MonoBehaviour {

	private enum ScaleType { scaleLeft = -1, scaleRight = +1 };

	public float Velocity = 0.5f;
	public Rigidbody2D RigidBody;
	public float JumpForce = 250f;
	private	bool GameOver = false;
	private ScaleType currentScaleType = ScaleType.scaleRight;
	private const int MAX_JUMPS = 4;
	public int Jumps = MAX_JUMPS;
	public AudioClip JumpAudioClip;
	public AudioClip CatchCoinAudioClip;
	public AudioClip GameOverAudioClip;
	public int Coins = 0;
	public UnityEngine.UI.Text JumpsCounter;
	public UnityEngine.UI.Text CoinsCounter;
	public UnityEngine.UI.Text GameOverText;
	public UnityEngine.UI.Button NewGameButton;

	// Use this for initialization
	void Start () {
		UpdateJumps (MAX_JUMPS);
		UpdateCoins (0);
		this.GameOver = false;
		this.GameOverText.enabled = false;
		this.NewGameButton.gameObject.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		if (this.GameOver == true) {
			return;
		}


		if (Input.GetKeyDown (KeyCode.Space) && this.Jumps > 0) {
			this.RigidBody.AddForce (new Vector2 (0, this.JumpForce * Time.deltaTime), ForceMode2D.Impulse);
			UpdateJumps(this.Jumps - 1);
			if (this.JumpAudioClip != null) {
				AudioManager.Instance.PlayClip (this.JumpAudioClip);	
			}
		}


		if (Input.GetKey (KeyCode.LeftArrow) || Input.GetKey (KeyCode.RightArrow)) {
			float deltaX = 0f;
			ScaleType scale;
			if (Input.GetKey(KeyCode.LeftArrow)){
				deltaX = (+1) * Time.deltaTime * this.Velocity;	
				scale = ScaleType.scaleLeft;
			} else {
				deltaX = (-1) * Time.deltaTime * this.Velocity;
				scale = ScaleType.scaleRight;
			}
			this.transform.Translate (new Vector2 (deltaX, 0));
			ChangeScale(scale);
		}
	}

	private void ChangeScale(ScaleType newScale){
		var localScale = this.transform.localScale;
		// verifica se houve mudanca de Scala no Eixo X
		if (!this.currentScaleType.Equals (newScale)) {
			localScale.x = newScale.GetHashCode();
			this.transform.localScale = localScale;	
			this.currentScaleType = newScale;
		}
	}

	public void OnCollisionEnter2D(Collision2D sender){
		if (sender.gameObject.CompareTag ("grass") || sender.gameObject.CompareTag ("platform")) {
			UpdateJumps (MAX_JUMPS);
		}


	}

	public void OnTriggerEnter2D(Collider2D sender)
	{
		if (sender.gameObject.CompareTag ("coin")) {
			if (this.CatchCoinAudioClip != null) {
				AudioManager.Instance.PlayClip (this.CatchCoinAudioClip);	
			}
			Destroy (sender.gameObject);
			UpdateCoins(this.Coins+1);
		}

		if (sender.gameObject.CompareTag ("hole")) {
			if (this.GameOverAudioClip != null) {
				AudioManager.Instance.PlayClip (this.GameOverAudioClip);	
			}
			this.GameOver = true;
			this.GameOverText.enabled = true;
			this.NewGameButton.gameObject.SetActive (true);
		}
	}

	public void UpdateJumps(int value){
		if (this.JumpsCounter != null) {
			this.JumpsCounter.text = value.ToString ();
		}
		this.Jumps = value;
	}

	public void UpdateCoins(int value){
		if (this.CoinsCounter != null) {
			this.CoinsCounter.text = value.ToString ();
		}
		this.Coins = value;
	}

}
