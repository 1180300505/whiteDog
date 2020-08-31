using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moves : MonoBehaviour
{
    // Start is called before the first frame update
    protected move move;//有角色控制必定有move组件，怪物ai同理
    protected Charactercontral contral;

    private bool right;
    private bool left;
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
        if (right && left)
        {
            if (contral.GetStatenum() == 1)
                contral.Setstatenum(0);
        }
        else if (left)//输入为左，即为负  
        {
            if (contral.GetStatenum() <= 1)
            {
                move.Turn(false);//向左转向 ，即为负  角色向左走
                contral.Setstatenum(1);//如果不是移动状态设置为移动状态

            }
            if (contral.GetStatenum() <= 2)
                move.moves(false);//如果不是技能状态，就可以移动    但是此状态下无法转向
        }
        else if (right)//输入为右，即为正  角色向右走
        {
            if (contral.GetStatenum() <= 1)
            {
                move.Turn(true);//向右转向
                contral.Setstatenum(1);//如果不是移动状态设置为移动状态

            }
            if (contral.GetStatenum() <= 2)
                move.moves(true);//如果不是技能状态，就可以移动    但是此状态下无法转向
        }
        else //停止状态
        {
            if (contral.GetStatenum() == 1)
                contral.Setstatenum(0);
        }

        right = false;
        left = false;
    }

    public void moveright()
    {
        right = true;
    }

    public void moveleft()
    {
        left = true;
    }
}
