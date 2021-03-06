﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCreation : MonoBehaviour {
	//用来装饰初始化地图所需物体的数组。
	//0.老家1.墙2.障碍3.出生效果4.河流5.草6.空气墙
	public GameObject[] item;

	//已有东西的位置列表
	private List<Vector3> itemPositionList=new List<Vector3> ();
	private void Awake(){
		InitMap();
	}
	private void InitMap(){
		//实例化老家
		CreateItem (item[0],new Vector3(0,-8,0),Quaternion.identity );
		//实例化老家围墙
		CreateItem (item[1],new Vector3(-1,-8,0),Quaternion.identity );
		CreateItem (item[1],new Vector3(1,-8,0),Quaternion.identity );
		for(int i=-1;i<2;i++){
			CreateItem (item[1],new Vector3(i,-7,0),Quaternion.identity );
		}
		//实例化外墙
		for(int i=-11;i<12;i++){
			CreateItem (item[6],new Vector3(i,9,0),Quaternion.identity );
		}
		for(int i=-11;i<12;i++){
			CreateItem (item[6],new Vector3(i,-9,0),Quaternion.identity );
		}
		for(int i=-8;i<9;i++){
			CreateItem (item[6],new Vector3(-11,i,0),Quaternion.identity );
		}
		for(int i=-8;i<9;i++){
			CreateItem (item[6],new Vector3(11,i,0),Quaternion.identity );
		}
		//初始化玩家
		GameObject go=Instantiate(item[3],new Vector3(-2,-8,0),Quaternion.identity);
		go.GetComponent<Born>().createPlayer=true;
		//产生敌人
		CreateItem(item[3],new Vector3(-10,8,0),Quaternion.identity);
		CreateItem(item[3],new Vector3(0,8,0),Quaternion.identity);
		CreateItem(item[3],new Vector3(10,8,0),Quaternion.identity);
		//第一次调用"CreateEnemy"函数为第4秒，之后没5秒调用一次
		InvokeRepeating("CreateEnemy",4,5);

		//实例化墙
		for(int i=0;i<50;i++){
			CreateItem (item[1],CreateRandomPosition(),Quaternion.identity );
		}
		//实例化障碍
		for(int i=0;i<20;i++){
			CreateItem (item[2],CreateRandomPosition(),Quaternion.identity );
		}
		//实例化河流
		for(int i=0;i<20;i++){
			CreateItem (item[4],CreateRandomPosition(),Quaternion.identity );
		}
		//实例化草
		for(int i=0;i<20;i++){
			CreateItem (item[5],CreateRandomPosition(),Quaternion.identity );
		}
	}
	
	private void CreateItem(GameObject item,Vector3 position,Quaternion rotation){
		GameObject itemGo=Instantiate (item,position,rotation );
		itemGo .transform .SetParent (gameObject .transform );//设置父级
		itemPositionList .Add(position);
	}
	//生成随机位置的函数
	private Vector3 CreateRandomPosition(){
		//不生成x=-10,10的两列，y=-8,8的两行的位置
		while(true){
			Vector3 createPosition=new Vector3 (Random.Range(-9,10),Random.Range(-7,8),0);
			if(!HasThePosition(createPosition))
				return createPosition;
		}
	}
	//判断位置列表中是否有这个位置
	private bool HasThePosition(Vector3 createPos) {
		for(int i=0;i<itemPositionList.Count;i++){
			if(createPos==itemPositionList[i])
			return true;
		}
		return false;
	}
	//产生敌人的方法
	private void CreateEnemy(){
		int num =Random.Range(0,3);
		Vector3 EnemyPos=new Vector3 ();
		if(num==0)
			EnemyPos =new Vector3 (-10,8,0);
		else if(num==1)
			EnemyPos =new Vector3 (0,8,0);
		else
			EnemyPos =new Vector3 (10,8,0);
		CreateItem(item[3],EnemyPos,Quaternion.identity );
	}
}
