using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    public bool bounch;//角色是否会被踩到的敌人弹起
    public float jumpspeed;//角色的向上跳跃
    public int consume;//跳跃消耗
    public int consumejumping;//跳跃空中维持速度消耗
    protected move move;//有角色控制必定有move组件，怪物ai同理
    protected Charactercontral contral;
    protected Heathscrips health;
    // Start is called before the first frame update

    private bool jumping;//是否在自主跳跃的途中，用于判断是否会废气
    private void Awake()
    {
        move = GetComponent<move>();//进行角色方向控制时就调用move中的函数
        contral = GetComponent<Charactercontral>();
        health = gameObject.GetComponent<Heathscrips>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        if (move.rispeed().y < -0.01)
        {
            jumping = false;
        }

        if (move.rispeed().y < -0.0001f && move.isgroundcharater()&&bounch)
        {
            Vector2 a = Vector2.zero;
            a.x = 0;
            a.y = jumpspeed * 3 / 5;
            move.AddSpeeds(a);
        }
    }

    public void jump()//跳跃消耗和速度
    {
        
        if (contral.GetStatenum() <= 2 && move.isground() && health.cando())
        {
            move.AddSpeed(0, jumpspeed);
            health.consumeqi(consume);
            health.setrecoverqi(false);
            jumping = true;
        }
    }

    public void stopjump()
    {
        if(move.rispeed().y > 0.0001f && move.rispeed().y < jumpspeed)
            move.setY(0);
    }

    public void jumpconsumeqi()
    {
        if (jumping)
        {
            health.consumeqi(consumejumping);
        }
    }
}
