using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerContral : Charactercontral
{
    // Start is called before the first frame update
    // 这个类中不会有具体控制刚体的代码，而是调用武器和移动来控制角色
    //主要是角色的移动控制，其次是武器的控制


    // 技能动作脚本
    private Dash dash;//冲刺
    private Attack attack;//攻击
    private Jump jump;//攻击
    private Moves moves;//移动
    //健康脚本
    private Heathscrips health;

    //角色的状态有：0正常 1移动 2攻击 3技能 4僵直


    //以下是判断按键的方式，根据按键，角色将会做出不同的动作
    private bool Up//是否按了上键
    {
        get
        {
            return Input.GetKey(KeyCode.W);
        }
    }
    private bool Down//是否按了下键
    {
        get
        {
            return Input.GetKey(KeyCode.S);
        }
    }

    //以下是其他函数，用来具体控制
    protected override void Awakes()
    {
        dash = gameObject.GetComponent<Dash>();
        health = gameObject.GetComponent<Heathscrips>();
        attack = gameObject.GetComponent<Attack>();
        jump = gameObject.GetComponent<Jump>();
        moves = gameObject.GetComponent<Moves>();
    }

    protected override void Starts()
    {

    }
    // Update is called once per frame
    protected override void Updates()
    {

        //用来响应按键
        bool J = Input.GetKeyDown(KeyCode.J);//攻击
        bool K = Input.GetKeyDown(KeyCode.K);//跳跃
        bool L = Input.GetKeyDown(KeyCode.L);//冲刺
        bool A = Input.GetKey(KeyCode.A);
        bool D = Input.GetKey(KeyCode.D);
        bool Kup= Input.GetKeyUp(KeyCode.K);

        if (GetStatenum() < 2)//检查能否回气
        {
            health.setrecoverqi(true);
        }
        else
        {
            health.setrecoverqi(false);
        }

        if (A)
        {
            moves.moveleft();
        }

        if (D)
        {
            moves.moveright();
        }

        if (J)
        {
            attack.doattack("hack",0.1700f);
        }

        if (K)
        {
            jump.jump();
        }

        if (Kup)
        {
            jump.stopjump();
        }

        if (L)
        {
            if (move.isground())
            {
                dash.dash();
            }
        }


    }


    protected override void FixedUpdates()
    {
        //动画的设置
        animator.SetBool("ground", move.isground());//设置着陆为move的着陆算法
        animator.SetFloat("speedy", move.rispeed().y);//让speedy成为刚体的纵向速度
        
        animator.SetBool("up", Up);
        animator.SetBool("down", Down);

        animator.SetBool("dashing", dash.isdashing());
        
        if (GetStatenum() == 1)
        {
            animator.SetBool("move", true);//为状态1时为移动状态
        }

        if(GetStatenum() == 0 ||!move.isground())
        {
            animator.SetBool("move", false);//为状态0时为站立状态
        }

        if(GetStatenum()>1)
        {
            animator.SetBool("move", false);
        }

    }

}
