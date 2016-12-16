using UnityEngine;
using System.Collections;

public class DestroyOnJointBreak : MonoBehaviour {

	private SpriteRenderer myRenderer;
	private Color color = Color.white;
	bool fadeOut = false;
	float spriteAlpha = 0f; // 0 is 100% full alpha

	// Use this for initialization
	void Start () {
		myRenderer = gameObject.GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
	}

	void FixedUpdate(){

		if (fadeOut) {
			spriteAlpha += 0.01f;
			Fade (myRenderer, spriteAlpha);
			if(spriteAlpha >= 1.0f) Destroy(gameObject);
		}

	}

	void OnJointBreak2D (Joint2D brokenJoint) {
		fadeOut = true;
	}
		
	void Fade(SpriteRenderer spriteRenderer, float amount){
		spriteRenderer.color = Color.Lerp (Color.white, new Color (1, 1, 1, 0), amount);
	}

}
