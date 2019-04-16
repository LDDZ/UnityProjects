﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
//using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SnakeHead : MonoBehaviour
{
	public List<Transform> bodyList=new List<Transform>();
    public float velocity = 0.35f;
    public int step;
    private int x;
    private int y;
    private Vector3 headPos;
	private Transform canvas;

	public GameObject bodyPrefab;
	public Sprite[] bodySprites=new Sprite[2];

    private void Awake() {
        canvas=GameObject.Find("Canvas").transform;
    }

    void Start()
    {
        InvokeRepeating("Move", 0, velocity);
        x = 0;y = step;
    }

    void Update()
    {
        //按下空格加速
        if (Input.GetKeyDown(KeyCode.Space) )
        {
            CancelInvoke();
            InvokeRepeating("Move", 0, velocity - 0.2f);
        }
        if (Input.GetKeyUp(KeyCode.Space) )
        {
            CancelInvoke();
            InvokeRepeating("Move", 0, velocity);
        }

        //方向控制
        if (Input.GetKey(KeyCode.W) && y != -step )
        {
            gameObject.transform.localRotation = Quaternion.Euler(0, 0, 0);
            x = 0;y = step;
        }
        if (Input.GetKey(KeyCode.S) && y != step )
        {
            gameObject.transform.localRotation = Quaternion.Euler(0, 0, 180);
            x = 0; y = -step;
        }
        if (Input.GetKey(KeyCode.A) && x != step )
        {
            gameObject.transform.localRotation = Quaternion.Euler(0, 0, 90);
            x = -step; y = 0;
        }
        if (Input.GetKey(KeyCode.D) && x != -step )
        {
            gameObject.transform.localRotation = Quaternion.Euler(0, 0, -90);
            x = step; y = 0;
        }
    }

    void Move()
    {
        headPos = gameObject.transform.localPosition;               //保存下来蛇头移动前的位置
        gameObject.transform.localPosition = new Vector3(headPos.x + x, headPos.y + y, headPos.z);  //蛇头向期望位置移动
        if (bodyList.Count > 0)
        {
            //由于我们是双色蛇身，此方法弃用
            // bodyList.Last().localPosition = headPos;                                              //将蛇尾移动到蛇头移动前的位置
            // bodyList.Insert(0, bodyList.Last());                                                  //将蛇尾在List中的位置更新到最前
            // bodyList.RemoveAt(bodyList.Count - 1);                                                //移除List最末尾的蛇尾引用

        //     //由于我们是双色蛇身，使用此方法达到显示目的
            for (int i = bodyList.Count - 2; i >= 0; i--)                                           //从后往前开始移动蛇身
            {
                bodyList[i + 1].localPosition = bodyList[i].localPosition;                          //每一个蛇身都移动到它前面一个节点的位置
            }
            bodyList[0].localPosition = headPos;                                                    //第一个蛇身移动到蛇头移动前的位置
        }
    }

	void Grow(){
		int index=(bodyList.Count%2==0)?0:1;
		GameObject body=Instantiate(bodyPrefab,new Vector3(2000,2000,0),Quaternion.identity);
		body.GetComponent<Image>().sprite=bodySprites[index];
        body.transform.SetParent(canvas, false);
		bodyList.Add(body.transform);
	}
	private void OnTriggerEnter2D(Collider2D other) {
		if (other.tag=="Food")
		{
			Destroy(other.gameObject);//删除Food。
            MainUIController.Instance.UpdateUI();
            Grow();
			FoodMaker.Instance.MakeFood((Random.Range(0,100)<20)?true:false);//必定生成Food,并且有20%的几率生成奖励
		}else if(other.tag=="Reward")
        {
            Destroy(other.gameObject);//销毁奖励
            MainUIController.Instance.UpdateUI(Random.Range(5,15)*10);
            Grow();//生成蛇身
        } else if(other.tag=="Body")
        {
            Debug.Log("Die");
        }else
        {
            switch(other.gameObject.name){
                case "Up":
                    transform .localPosition=new Vector3(transform .localPosition.x,-transform.localPosition.y+30,transform.localPosition.z);
                    break;
                case "Down":
                    transform .localPosition=new Vector3(transform .localPosition.x,-transform.localPosition.y-30,transform.localPosition.z);
                    break;
                case "Left":
                    transform .localPosition=new Vector3(-transform .localPosition.x+180,transform.localPosition.y,transform.localPosition.z);
                    break;
                case "Right":
                    transform .localPosition=new Vector3(-transform .localPosition.x+240,transform.localPosition.y,transform.localPosition.z);
                    break;
            }
        }
	}

}