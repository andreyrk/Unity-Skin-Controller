using System;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SkinController : MonoBehaviour {
	
	SpriteRenderer spriteRenderer;

	public string path;
	public string spriteSheet;
	public SpriteNaming type;
	public bool enableWarnings;

	/// <summary>
	/// This enum controls which searching method will be used to swap between sprites
	/// </summary>
	public enum SpriteNaming {
		Tag,
		Number
	}

	/// <summary>
	/// Caches the renderer for runtime use.
	/// </summary>
	void Start() {
		spriteRenderer = GetComponent<SpriteRenderer> ();
	}

	void LateUpdate() {
		SkinUpdate (spriteRenderer);
	}

	/// <summary>
	/// Shows changes in the Scene View
	/// </summary>
	void OnDrawGizmos() {
		SkinUpdate (GetComponent<SpriteRenderer> ());
	}

	public void SkinUpdate(SpriteRenderer spriteRenderer) {
		if (spriteSheet != "" && spriteRenderer.sprite) {
			string spriteName = spriteRenderer.sprite.name;

			Sprite[] otherSprites = Resources.LoadAll<Sprite> ((path != "" ? path + "/" : "") + spriteSheet);
			Sprite newSprite;

			Predicate<Sprite> match = null;

			if (type == SpriteNaming.Number) {
				// Match set to get sprites based on its numeric termination
				match = item => item.name.Substring (item.name.LastIndexOf ('_')) == spriteName.Substring (spriteName.LastIndexOf ('_'));
			} else if (type == SpriteNaming.Tag) {
				// Match set to get sprites based on its tag
				match = item => item.name == spriteName;
			}

			newSprite = Array.Find (otherSprites, match);

			// Apply changes
			if (newSprite) {
				spriteRenderer.sprite = newSprite;
			} else if (enableWarnings) {
				Debug.Log ("Swapping issue. Sprite is null.");
			}
		}
	}
}