using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour {

    //定义子弹
    public GameObject bullet;
    //定义子弹速度
    public float speed = 5;
	// Use this for initialization
	void Start () {
        //GameObject.Instantiate(bullet,transform.position,transform.rotation);
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
           GameObject b=  GameObject.Instantiate(bullet, transform.position, transform.rotation);
            Rigidbody rgb = b.GetComponent<Rigidbody>();
            //velocity设置刚体的速度矢量，
            rgb.velocity = transform.forward * speed;
        }
	}
}
