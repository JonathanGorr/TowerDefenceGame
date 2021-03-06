﻿using UnityEngine;
using System.Collections;

public class Wall : MonoBehaviour {

	public Sprite dmgSprite;
	public int hp = 4;

	//sfx
	public AudioClip chopSound1;
	public AudioClip chopSound2;

	private SpriteRenderer spriteRenderer;

	// Use this for initialization
	void Awake () {
		spriteRenderer = GetComponent<SpriteRenderer>();
	}

	public void DamageWall(int loss)
	{
		if(gameObject.tag != "Pit")
		{
			SoundManager.instance.RandomizeSfx(chopSound1,chopSound2);
			spriteRenderer.sprite = dmgSprite;
			hp -= loss;

			if(hp <= 0)
				gameObject.SetActive(false);
		}
	}
}
