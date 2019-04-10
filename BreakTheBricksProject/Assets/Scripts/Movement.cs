using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

    public float speed = 3;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //返回由axisName标识的虚拟轴的值,Horizontal指水平轴。
        float h = Input.GetAxis("Horizontal");
        //Vertical垂直
        float v = Input.GetAxis("Vertical");
        //Debug.Log(h);
        //Translate()在平移的方向和距离上移动变换。Vector3()用给定的x、y、z分量创建一个新的向量。
        //Time.deltaTime完成最后一帧所需的时间(以秒为单位)(只读)
        transform.Translate(new Vector3(h, v, 0) * Time.deltaTime * speed);
	}
}
