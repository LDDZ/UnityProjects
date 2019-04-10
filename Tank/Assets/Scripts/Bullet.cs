using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public float speed = 10;//子弹速度
    public bool isPlayerBullect;//是否为玩家的子弹
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(transform.up * speed * Time.deltaTime,Space.World );
	}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag )
        {
            case "Tank":
                if(!isPlayerBullect){
                    collision.SendMessage("Die");
                    Destroy (gameObject );
                }
                break;
            case "Heart":
                collision .SendMessage ("Die");
                Destroy (gameObject );//销毁子弹
                break;
            case "Enemy":
                if(isPlayerBullect){
                    collision.SendMessage("Die");
                    Destroy (gameObject );
                }
                break;
            case "Wall":
            Destroy (collision .gameObject );//子弹打中墙，销毁墙
            Destroy (gameObject );//销毁子弹
                break;
            case "Barriar":
            if(isPlayerBullect){
                collision.SendMessage("PlayAudio");//调用播放子弹打中障碍的音效
            }
            Destroy (gameObject );//子弹打中障碍，销毁子弹
                break;
            default:
                break;
        }
    }
}
