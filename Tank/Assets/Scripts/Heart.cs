using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour {
	private SpriteRenderer sr;
	public GameObject explosionPrefab;//用于存放爆炸特效
	public  Sprite brokenSprite;
	public AudioClip dieAudio;//死亡音效

	// Use this for initialization
	void Start () {
		sr=GetComponent<SpriteRenderer >();
	}
	
	public void Die(){
        sr.sprite =brokenSprite ;//切换为老窝被击破后的图片
		Instantiate (explosionPrefab ,transform .position ,transform .rotation );//生成爆炸特效
		PlayerManager.Instance.isDefeat=true;
		AudioSource.PlayClipAtPoint(dieAudio,transform .position);
	}
}
