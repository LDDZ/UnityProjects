﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine .SceneManagement;

public class PlayerManager : MonoBehaviour {
	//属性值
	public int lifeValue=3;//生命值
	public int playerScore=0;//玩家得分
	public bool isDead;//是否死亡
	public bool isDefeat;//是否失败

	//引用
	public GameObject born;
	public Text playerScoreText;
	public Text playerLifeValueText;
	public GameObject isDefeatUI;

	//单例
	private static PlayerManager instance;
	public static PlayerManager Instance{
		get{return instance;}
		set{instance=value;}
	}
	private void Awake() {
		Instance=this;
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(isDefeat){
			isDefeatUI.SetActive(true);
			Invoke("ReturnToTheMainMenu",3);
			return;
		}
		if(isDead)
		Recover();
		playerScoreText.text=playerScore.ToString();
		playerLifeValueText.text=lifeValue.ToString();
	}
	private void Recover(){
		if(lifeValue <=0){
			//游戏失败，返回主界面
			isDefeat=true;
			Invoke("ReturnToTheMainMenu",3);
		}else{
			lifeValue--;
			GameObject go =Instantiate(born,new Vector3(-2,-8,0),Quaternion.identity);
			go.GetComponent<Born>().createPlayer=true;
			isDead=false;
		}
	}
	private void ReturnToTheMainMenu(){
		SceneManager .LoadScene (0);
	}
}