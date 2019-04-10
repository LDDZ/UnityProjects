using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
	//属性值
    public float moveSpeed = 9;//移动速度
    private Vector3 bullectEulerAngles;//子弹欧拉角
	private float v=-1;
	private float h;

    //引用
    private SpriteRenderer sr;
    public Sprite[] tankSprite;//上 右 下 左
    public GameObject bullectPrefab;//子弹
    public GameObject explodePrefab;//爆炸特效
	
	//计时器
    private float timeVal;//时间计时，用于控制攻击间隔
	private float timeValChangeDirection;//改变行走方向的计时器

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //攻击时间间隔
        if(timeVal >= 3)
        {
            Attack();
        }else
        {
            timeVal += Time.deltaTime;
        }
    }
    public void FixedUpdate()
    {
        Move();
    }
    /// <summary>
    /// 坦克移动函数
    /// </summary>
    private void Move()
    {
		if(timeValChangeDirection>=4){
			int num=Random .Range (0,8);
			if(num>4){
				v=-1;
				h=0;
			}else if(num==0){
				v=1;
				h=0;
			}else if(num==1||num==2){
				v=0;
				h=-1;
			}else if(num==3||num==4){
				v=0;
				h=-1;
			}
			timeValChangeDirection=0;
		}else {
			timeValChangeDirection+=Time.fixedDeltaTime;
		}
        
        //在平移的方向和距离上移动变换。
        transform.Translate(Vector3.up * v * moveSpeed * Time.fixedDeltaTime, Space.World);
        if (v < 0)
        {
            sr.sprite = tankSprite[2];
            bullectEulerAngles = new Vector3(0, 0, -180);
        }
        else if (v > 0)
        {
            sr.sprite = tankSprite[0];
            bullectEulerAngles = new Vector3(0, 0, 0);
        }

        //移动优先级，当垂直轴有输入时，直接return
        if (v != 0)
            return;

        //Time.deltaTime（它表示距上一次调用Update或FixedUpdate 所用的时间）
        //Time.fixedDeltaTime 固定增量时间
        transform.Translate(Vector3.right * h * moveSpeed * Time.fixedDeltaTime, Space.World);
        if (h < 0)
        {
            sr.sprite = tankSprite[3];
            bullectEulerAngles = new Vector3(0, 0, 90);
        }
        else if (h > 0)
        {
            sr.sprite = tankSprite[1];
            bullectEulerAngles = new Vector3(0, 0, -90);
        }
    }

    /// <summary>
    /// 坦克攻击函数
    /// </summary>
    private void Attack()
    {
        //子弹产生的角度：当前坦克的角度+子弹应该旋转的角度
        Instantiate(bullectPrefab, transform.position, Quaternion.Euler(transform.eulerAngles + bullectEulerAngles));
        timeVal = 0;//计时器归零
        
    }

    // 死亡函数
    public void Die()
    {
        PlayerManager .Instance.playerScore++;
        //产生爆炸特效
        Instantiate(explodePrefab, transform.position, transform.rotation);
        //死亡（销毁坦克）
        Destroy(gameObject);
    }
    //当敌人向撞，让敌人直接转向
    private void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.tag=="Enemy")
            timeValChangeDirection=4;
    }
}
