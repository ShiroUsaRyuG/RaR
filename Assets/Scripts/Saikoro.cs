using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Saikoro : MonoBehaviour
{
    private int charaHP = 20;
    private int enemyHP = 15;
    private int charaDice;
    private int enemyDice;


    [SerializeField]
    private int[] skillCounts;
    
    public SkillDataSO skillDataSO;
    [SerializeField]
    private SkillData[] charaSkillDatas;
    [SerializeField]
    private SkillData[] enemySkillDatas;
    
    [SerializeField]
    private CharaData[] charaDatas;
    public CharaDataSO charaDataSO;

    // Start is called before the first frame update
    private void Start()
    {
        skillCounts = new int[6];
        charaDatas = new CharaData[2];
        for (int i = 0; i < charaDatas.Length; i++)
        {
            charaDatas[i] = charaDataSO.charaDatasList[i];
            int[] skillNum = charaDatas[i].skillNumbers.Split(',').Select(x => int.Parse(x)).ToArray();
            if (i == 0)
            {
                charaSkillDatas = new SkillData[6];
                for (int j = 0; j < charaSkillDatas.Length; j++)
                {
                    charaSkillDatas[j] = skillDataSO.skillDatasList[skillNum[j]];
                    skillCounts[j] = charaSkillDatas[j].maxSkillCount;
                }
            }
            else
            {
                enemySkillDatas = new SkillData[6];
                for (int j = 0; j < enemySkillDatas.Length; j++)
                {
                    enemySkillDatas[j] = skillDataSO.skillDatasList[skillNum[j]];
                }
            }
            
        }
        //charaDatasskillDatas = new SkillData[6];
        //for (int i = 0; i < charaDatasskillDatas.Length; i++)
        //{
        //    skillDatas[i] = skillDataSO.skillDatasList[i];
        //}
    }

    private void Update()
    {
        //Debug.Log(CheckHP());
        if (Input.GetKeyDown(KeyCode.B) && CheckHP() == false)
        {
            //Debug.Log("チェック");
            Battle();
        }
    }

    private void Battle()
    {
        Debug.Log("こっちの攻撃！");
        RollCharaDice();
        if (CheckHP() == false){
            Debug.Log("敵の攻撃！");
            RollEnemyDice();
        }
    }

    private void RollCharaDice()
    {
        charaDice = Random.Range(0, 6);
        enemyHP = enemyHP - charaSkillDatas[charaDice].attackPow;
        Debug.Log("自分の攻撃！　"+charaSkillDatas[charaDice].skillName+
            "！　敵に"+charaSkillDatas[charaDice].attackPow+"のダメージを与えた！"); 
        if (enemyHP > 0) {
            Debug.Log("敵のHPは残り" + enemyHP);
        }
        else
        {
            enemyHP = 0;
            Debug.Log("敵を倒した！");
        }
    }

    private void RollEnemyDice()
    {
        enemyDice = Random.Range(0, 6);
        charaHP = charaHP - enemySkillDatas[enemyDice].attackPow;
        Debug.Log("敵の攻撃！　"+enemySkillDatas[enemyDice].skillName+
            "！　自分は"+enemySkillDatas[enemyDice].attackPow+"のダメージを受けた！");
        if (charaHP > 0)
        {
            Debug.Log("自分のHPは残り" + charaHP);
        }
        else
        {
            charaHP = 0;
            Debug.Log("自分は倒れてしまった！");
        }
    }

    private bool CheckHP()
    {
        if (charaHP == 0 || enemyHP == 0) return true;
        else return false;
    }
}
