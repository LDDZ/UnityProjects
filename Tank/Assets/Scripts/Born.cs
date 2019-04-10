using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Born : MonoBehaviour {
	public GameObject playerPrefab;//用于存放玩家预制体
	public GameObject[] enemyPrefabList;//用于存在敌人列表

	public  bool createPlayer;//出生的是否为玩家坦克

	// Use this for initialization
	void Start () {
		Invoke("BornTank",1f);//0.8秒后调用“BornTank”函数
		Destroy (gameObject ,1f);//0.8秒后销毁出生特效
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	private void BornTank(){
		if(createPlayer ){
			Instantiate (playerPrefab ,transform .position ,Quaternion.identity );//玩家坦克出生
		}else {
			int num =Random .Range (0,2);//Random 用于生成随机数据的类。Range()返回范围内的随机函数【0或1】
			Instantiate (enemyPrefabList [num],transform .position ,Quaternion .identity );//敌人出生
		}
		
	}
}
