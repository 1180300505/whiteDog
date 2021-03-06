﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{
    //冲刺是没有速度的位移
    
    public float dashtime;//设定冲刺时间
    public float speed;//设定冲刺速度
    public int consume;//设定冲刺消耗
    protected move move;//有角色控制必定有move组件，怪物ai同理
    protected Charactercontral contral;
    protected bool dashing;//是否正在冲刺
    protected bool right;//向哪个方向的冲刺
    protected float dashes;//冲刺的时间
    // Start is called before the first frame update
    private void Awake()
    {
        move = GetComponent<move>();//进行角色方向控制时就调用move中的函数
        contral = GetComponent<Charactercontral>();
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (dashes > 0)
        {
            dashes -= Time.deltaTime;

            if (right)
            {

                movenospeed(speed * Time.deltaTime, 0,transform);
            }
            else
            {
                movenospeed(-speed * Time.deltaTime, 0,transform);
            }


            if (dashes < 0)
            {
                contral.Setstatenum(1);
                //animator.SetBool("dashing", false);
                setdashing(false);
            }

        }
    }

    private void FixedUpdate()
    {

    }

    public void dash()
    {//冲刺
        Heathscrips health = gameObject.GetComponent<Heathscrips>();
        if (contral.GetStatenum() < 2 &&(health==null|| health.cando()))
        {
            //animator.SetBool("dashing", true);
            setdashing(true);
            move.setX(0);
            move.setY(0);
            dashes = dashtime;//dash的时间设置为dashtime
            contral.Setstatenum(3);//技能状态
            right = move.Turn();
            

            if(health!=null)
            health.consumeqi(consume);

        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag=="character"&&dashes>0)
        {
            //animator.SetBool("dashing", false);
            setdashing(false);
            dashes = -0.00001f;
            contral.Setstatenum(1);
            move emove = collision.transform.GetComponent<move>();
            if (right)
            {
                movenospeed(-0.2f, 0, transform);
                movenospeed(0.2f, 0,collision. transform);
                move.AddSpeed(-0.2f, 0);
                emove.AddSpeed(0.2f, 0);
            }

            else
            {
                movenospeed(0.2f, 0, transform);
                movenospeed(-0.2f, 0, collision.transform);
                move.AddSpeed(0.2f, 0);
                emove.AddSpeed(-0.2f, 0);
            }

            
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.transform.tag == "character"&&dashes>0)
        {
            //animator.SetBool("dashing", false);
            setdashing(false);
            dashes = -0.00001f;
            contral.Setstatenum(1);


        }
    }


    private void movenospeed(float x, float y,Transform transformer)
    {
        Vector3 vector3 = transformer.localPosition;
        vector3.x += x;
        vector3.y += y;

        transformer.localPosition = vector3;
    }

    public bool isdashing()
    {
        return dashing;
    }

    public void setdashing(bool a)
    {
        dashing = a; 
    }
}
