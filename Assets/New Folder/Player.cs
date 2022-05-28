using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    int food_min = 0;//��С����ֵ
    int food_max = 100;//��󱥸�ֵ
    int food_cur = 0;//��ǰ����ֵ

    int spirit_min = 0;//��С����ֵ
    int spirit_max = 100;//�����ֵ
    int spirit_cur = 0;//��ǰ����ֵ

    public enum info_type
    {
        FOOD,SPIRIT,
    }
    public enum operate_type
    {
        INCREASE,DECREASE,
    }
    public void ChangePlayerInfo(info_type info_Type,operate_type operate_Type,int input_num)
    {
        if(info_Type==info_type.FOOD)
        {
            if(operate_Type==operate_type.INCREASE)
            {
                food_cur += input_num;
            }
            else
            {
                food_cur -= input_num;
            }
        }
        else
        {
            if (operate_Type == operate_type.INCREASE)
            {
                spirit_cur += input_num;
            }
            else
            {
                spirit_cur -= input_num;
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        print(food_cur);
    }
}
