﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FoodMaker : MonoBehaviour {
	private static FoodMaker _instance;
	public static FoodMaker Instance{
		get{return _instance;}
	}

	public int xlimit=21;
	public int ylimit=11;
	public int xoffset=7;
	public GameObject foodPrefab;
	public Sprite[] foodSprites;
	private Transform foodHolder;
	
	private void Awake() {
		_instance=this;
	}
	private void Start() {
		foodHolder=GameObject.Find("FoodHolder").transform;
		MakeFood();
	}
	public void MakeFood(){
		int index=Random.Range(0,foodSprites.Length);
		GameObject food =Instantiate(foodPrefab);
		food.transform.SetParent(foodHolder,false);
		food.GetComponent<Image>().sprite=foodSprites[index];
		int x=Random.Range(-xlimit+xoffset ,xlimit );
		int y=Random .Range (-ylimit ,ylimit);
		food.transform.localPosition =new Vector3(x*30,y*30,0);
	}
}
