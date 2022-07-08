using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Data;
using System.IO;
using System.Text.RegularExpressions;

public static class GlobalFunctions
{
   
    //public static Player player;
    public static List<GameObject> cam_arr = new List<GameObject>();
    public static int cur_cam_idx = 0;

    public static List<Action> act_arr = new List<Action>();
    public static RandomEventUI randomEventUI;
    public static PrepareFoodQTE PrepareFoodQTE;
    public static PickFood PickFood;

    public static DataTable random_event_dt;
    public static DataTable food_qte_dt;

    public static List<Recipe> recipe_arr = new List<Recipe>();


    public enum OperateType
    {
        INCREASE, DECREASE,
    }

    public static void InitializeAction(Player player)
    {
        act_arr.Add(() => Event.FeedCat(player));
        act_arr.Add(() => Event.CovidTest(player));
        act_arr.Add(() => Event.EatFood(player));
        act_arr.Add(() => Event.ExchangeFood(player));
        act_arr.Add(() => Event.Cook(player));
        act_arr.Add(() => Event.Chat(player));
        act_arr.Add(() => Event.ViewNews(player));
        act_arr.Add(() => Event.RandomEvent(player, 1));
        act_arr.Add(() => Event.RandomEvent(player, 2));

        
    }

    public static void ChangeCamera(int cam_idx)
    {
        cam_arr[cam_idx].SetActive(true);
        foreach(GameObject cam in cam_arr)
        {
            if (cam == cam_arr[cam_idx]) { continue; }
            cam.SetActive(false);
        }
    }

    public static void InvokeAction(int act_idx)
    {
        act_arr[act_idx].Invoke();
    }

    public static void ChangePlayerInfo(Info info, OperateType operate_type, int val)
    {
        if(operate_type == OperateType.INCREASE)
        {
            info.increase(val);
        }
        if (operate_type == OperateType.DECREASE)
        {
            info.decrease(val);
        }
    }

    public static void EndDay()
    {
        // scene transition effect
        
        // adjust variables
    }

    public static void ChangeRandomEventUIVisible()
    {
        randomEventUI.gameObject.SetActive(!randomEventUI.gameObject.activeSelf);
    }
   
    public static DataTable ReadCSV(string file_path)
    {
        DataTable dt = new DataTable();
        using (FileStream fs = new FileStream(file_path, FileMode.Open, FileAccess.Read))
        {
            using(StreamReader sr=new StreamReader(fs,System.Text.Encoding.UTF8))
            {
                string str_line = "";
                string[] ary_line = null;
                string[] table_head = null;
                int colum_count = 0;
                bool is_first_line = true;
                while(!String.IsNullOrWhiteSpace(str_line = sr.ReadLine()))
                {
                    if (is_first_line==true)
                    {
                        table_head = str_line.Split(',');
                        is_first_line = false;
                        colum_count = table_head.Length;
                        for(int i=0;i<colum_count;i++)
                        {
                            DataColumn dc = new DataColumn(table_head[i]);
                            dt.Columns.Add(dc);
                        }
                    }
                    else
                    {
                        ary_line = str_line.Split(',');
                        DataRow dr = dt.NewRow();
                        for(int i=0;i<colum_count;i++)
                        {
                            dr[i] = ary_line[i];
                        }
                        dt.Rows.Add(dr);
                    }
                }
                sr.Close();
                fs.Close();
                return dt;
            }
        }
    }

    public static void ReadRecipeCSV(string file_path)
    {
        string ingredient_pattern = "[a-zA-Z]+";
        string num_pattern = "\\d+";
        DataTable dt = ReadCSV(file_path);
        
        foreach (DataRow r in dt.Rows)
        {
            string str = r["ingredient"].ToString();
            Regex rgx = new Regex(ingredient_pattern);
            var ingredients_names = rgx.Matches(str);
            rgx = new Regex(num_pattern);
            var nums = rgx.Matches(str);
            Dictionary<string, int> ingredients = new Dictionary<string, int>();

            for(int i = 0; i < ingredients_names.Count; i++)
            {
                ingredients.Add(ingredients_names[i].Value, int.Parse(nums[i].Value));
            }

            Sprite sprite = Resources.Load<Sprite>(r["name"].ToString());

            Recipe recipe = new Recipe(r["name"].ToString(), int.Parse(r["fullness"].ToString()), int.Parse(r["spirit"].ToString()), ingredients, sprite);
            
            recipe_arr.Add(recipe);
        }
    }

}
