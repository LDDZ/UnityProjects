using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    //属性值
    public float moveSpeed = 9;//移动速度
    private Vector3 bullectEulerAngles;//子弹欧拉角
    private float timeVal;//时间计时，用于控制攻击冷却
    public  float defendTimeVal=9;//无敌时间计时器
    private bool isDefended=true;//是否处于无敌

    //引用
    private SpriteRenderer sr;
    public Sprite[] tankSprite;//上 右 下 左
    public GameObject bullectPrefab;//子弹
    public GameObject explodePrefab;//爆炸特效
    public GameObject defendEffectPrefab;//无敌状态特效
    public AudioSource moveAudio;//存放音频组件
    public AudioClip[] tankAudio;//存放音频资源

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //是否处于无敌状态
        if (isDefended)
        {
            defendEffectPrefab.SetActive(true);//是否激活
            defendTimeVal -= Time.deltaTime;
            if (defendTimeVal <= 0)
            {
                isDefended = false;
                defendEffectPrefab.SetActive(false);
            }
        }
        
    }
    public void FixedUpdate()
    {
        if(PlayerManager.Instance .isDefeat){
            return;
        }
        Move();
        //攻击CD
        if(timeVal >= 0.4f)
        {
            Attack();
        }else
        {
            timeVal += Time.fixedDeltaTime;
        }
    }
    /// <summary>
    /// 坦克移动函数
    /// </summary>
    private void Move()
    {
        //对应键盘上面的上下箭头，当按下上或下箭头时触发
        float v = Input.GetAxisRaw("Vertical");
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
        if(Mathf.Abs(v)>0.05f){
            moveAudio.clip=tankAudio[1];
            if(!moveAudio.isPlaying){
                moveAudio.Play();//当音效不在播放状态时，才再次播放
            }
        }

        //移动优先级，当垂直轴有输入时，直接return
        if (v != 0)
            return;

        //对应键盘上面的左右箭头，当按下左或右箭头时触发
        float h = Input.GetAxisRaw("Horizontal");
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
        if(Mathf.Abs(h)>0.05f){
            moveAudio.clip=tankAudio[1];
            if(!moveAudio.isPlaying){
                moveAudio.Play();//当音效不在播放状态时，才再次播放
            }
        }else
        {
             moveAudio.clip=tankAudio[0];
            if(!moveAudio.isPlaying){
                moveAudio.Play();//当音效不在播放状态时，才再次播放
            }
        }
    }

    /// <summary>
    /// 坦克攻击函数
    /// </summary>
    private void Attack()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //子弹产生的角度：当前坦克的角度+子弹应该旋转的角度
            Instantiate(bullectPrefab, transform.position, Quaternion.Euler(transform.eulerAngles + bullectEulerAngles));
            timeVal = 0;//计时器归零
        }
    }

    // 死亡函数
    public void Die()
    {
        if (isDefended)
            return;
        
        PlayerManager .Instance.isDead =true;
        //产生爆炸特效
        Instantiate(explodePrefab, transform.position, transform.rotation);
        //死亡（销毁坦克）
        Destroy(gameObject);
    }
}
