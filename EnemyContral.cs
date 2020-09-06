using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyContral : Charactercontral
{
    //敌人攻击：敌人的攻击状态，有冷却限制
    //敌人闲散：敌人将会在原地左右散步，或是做一些无关紧要的事情
    //敌人视野：一个碰撞箱，当玩家碰撞了这个箱子，敌人就进入仇恨状态
    //敌人仇恨：进入仇恨状态会追踪一段时间，当远离一定的距离，敌人会放弃仇恨
    //敌人追踪：如果和玩家在同一个平台上，敌人会向玩家靠近，如果不在同一个平台，敌人将会尝试跨越障碍（包括阻挡物或者中空）


    //控制系统将会联系一个子物体，那个子物体将会具体判定当前的仇恨，以及路径方法


    //子物体将会有两个碰撞判定，一个是判断敌人是否在视野范围内，另一个是判定前方是否有中空或阻挡

    public float chouhentime;//仇恨的固定时间
    public float chouhenlength;//敌人的仇恨半径


    private bool chouhen;//是否在仇恨状态
    private enemySight sight;
    private float chouhencooldown;//仇恨冷却
    private Transform target;//目标

    
    private Moves moves;
    private Jump jump;


    protected override void Awakes()
    {
        sight = GetComponentInChildren<enemySight>();
        moves = GetComponent<Moves>();
        jump = GetComponent<Jump>();
        chouhencooldown = 0;
    }

    protected override void Starts()
    {
        //throw new System.NotImplementedException();
    }

    protected override void Updates()
    {

        if(target ==null)
        target = sight.targets();//当前目标为视野给予的目标
        move enemymove; //获取敌人的move
        if (target != null)
            enemymove = target.gameObject.GetComponent<move>();
        else
            enemymove = null;
        
        float distent=0;

        if (target == null)
        {
            chouhencooldown = 0;
            chouhen = false;
        }
        else
        {
            distent = (transform.position - target.position).sqrMagnitude;//令distance为当前与目标的距离
        }

        

        if (chouhencooldown > 0f&&distent>chouhenlength&&chouhen)//当冷却大于0且大于仇恨半径的时候，减少冷却
        {
            chouhencooldown -= Time.deltaTime;
        }

        if(!chouhen &&sight.insights())//当非仇恨状态，敌人进入视野，进入仇恨状态
        {
            chouhen = true;
            chouhencooldown = chouhentime;
        }

        if (sight.insights()||distent<=chouhenlength)//当敌人还在视野里的时候，仇恨不消失，仇恨冷却时间不变
        {
            chouhencooldown = chouhentime;
            chouhen = true;
        }

        if (chouhencooldown <= 0)//当仇恨冷却降为0的时候
        {
            chouhen = false;
            target = null;//放弃目标
        }


        //敌人的仇恨状态，将会选择性地追随
        //敌人仇恨状态的反应：
        if (chouhen&&target!=null)//在地面上时
        {
            if (target.position.x < transform.position.x)//向着敌人的方向走去
            {
                moves.moveleft();  
            }
            else
            {
                moves.moveright();
            }
            
            if(enemymove!=null)
            if(enemymove.theground() != move.theground())//与敌人不在同一个地面上时
            {
                if (!sight.isground())//遇到空地，进行跳跃
                {
                    jump.jump();
                }
            }
        }

        if (!move.isground())//在空中，会跟着已经跳出的方向走
        {
            if (moves.isleft())
            {
                moves.moveleft();
            }
            else if (moves.isright())
            {
                moves.moveright();
            }
        }
        
    }

    protected override void FixedUpdates()
    {
        
    }
    // Start is called before the first frame update

}
