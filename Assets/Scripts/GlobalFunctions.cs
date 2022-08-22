using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Data;
using System.IO;
using System.Text.RegularExpressions;
using UnityEngine.AI;
using UnityEditor;

public static class GlobalFunctions
{
   
    public static Player player;
    
    public static List<Camera> cam_arr = new List<Camera>();

    public static List<Action> act_arr = new List<Action>();

    public static RandomEventUI randomEventUI;
    public static DataTable random_event_dt;
   

    public static List<Recipe> recipe_arr = new List<Recipe>();
    

    public static DataTable food_qte_dt;
    public static PrepareFoodQTE PrepareFoodQTE;
    public static PickFood PickFood;
    public static FoodBsaketBlank[,] foodBsaketBlanks = new FoodBsaketBlank[4, 3];


    //public static Dictionary<Transform, bool> outline_status = new Dictionary<Transform, bool>();
    
    
    public static Transform mouse_hovered_obj;
    
    public static Dictionary<string, bool> outline_objs = new Dictionary<string, bool>();
    public static Dictionary<string, Vector3> area_anchors = new Dictionary<string, Vector3>();

    public static NavMeshAgent navMeshAgent;

    public static Camera current_camera;

    public static List<GameObject> outline_objects = new List<GameObject>();

    //public static GameObject sliced_content = new GameObject();
    
    
    public enum GameState
    {
        MainScene, Cooking,
    }
    public static GameState current_state = GameState.MainScene;


    public static bool IsActionMode = false;

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
        cam_arr[cam_idx].enabled = true;
        foreach(Camera cam in cam_arr)
        {
            if (cam == cam_arr[cam_idx]) { continue; }
            cam.enabled = false;
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

    public static void InitFoodBasket()
    {
        for(int i=0;i<4;i++)
        {
            for(int j=0;j<3;j++)
                foodBsaketBlanks[i, j] = new FoodBsaketBlank();
        }

        foodBsaketBlanks[0, 0].position = new Vector3(960 + 30, 540 + 30, 0);
        foodBsaketBlanks[0, 0].rotation = new Quaternion(0, 0, 0, 0);
        foodBsaketBlanks[0, 0].scale = new Vector2(100, 100);

        foodBsaketBlanks[0, 1].position = new Vector3(960 + 10, 540 + 10, 0);
        foodBsaketBlanks[0, 1].rotation = new Quaternion(0, 0, 0, 0);
        foodBsaketBlanks[0, 1].scale = new Vector2(100, 100);

        foodBsaketBlanks[0, 2].position = new Vector3(0, 0, 0);
        foodBsaketBlanks[0, 2].rotation = new Quaternion(0, 0, 0, 0);
        foodBsaketBlanks[0, 2].scale = new Vector2(1, 1);

        foodBsaketBlanks[1, 0].position = new Vector3(0, 0, 0);
        foodBsaketBlanks[1, 0].rotation = new Quaternion(0, 0, 0, 0);
        foodBsaketBlanks[1, 0].scale = new Vector2(1, 1);

        foodBsaketBlanks[1, 1].position = new Vector3(0, 0, 0);
        foodBsaketBlanks[1, 1].rotation = new Quaternion(0, 0, 0, 0);
        foodBsaketBlanks[1, 1].scale = new Vector2(1, 1);

        foodBsaketBlanks[1, 2].position = new Vector3(0, 0, 0);
        foodBsaketBlanks[1, 2].rotation = new Quaternion(0, 0, 0, 0);
        foodBsaketBlanks[1, 2].scale = new Vector2(1, 1);

        foodBsaketBlanks[2, 0].position = new Vector3(0, 0, 0);
        foodBsaketBlanks[2, 0].rotation = new Quaternion(0, 0, 0, 0);
        foodBsaketBlanks[2, 0].scale = new Vector2(1, 1);

        foodBsaketBlanks[2, 1].position = new Vector3(0, 0, 0);
        foodBsaketBlanks[2, 1].rotation = new Quaternion(0, 0, 0, 0);
        foodBsaketBlanks[2, 1].scale = new Vector2(1, 1);

        foodBsaketBlanks[2, 2].position = new Vector3(0, 0, 0);
        foodBsaketBlanks[2, 2].rotation = new Quaternion(0, 0, 0, 0);
        foodBsaketBlanks[2, 2].scale = new Vector2(1, 1);

        foodBsaketBlanks[3, 0].position = new Vector3(0, 0, 0);
        foodBsaketBlanks[3, 0].rotation = new Quaternion(0, 0, 0, 0);
        foodBsaketBlanks[3, 0].scale = new Vector2(1, 1);

        foodBsaketBlanks[3, 1].position = new Vector3(0, 0, 0);
        foodBsaketBlanks[3, 1].rotation = new Quaternion(0, 0, 0, 0);
        foodBsaketBlanks[3, 1].scale = new Vector2(1, 1);

        foodBsaketBlanks[3, 2].position = new Vector3(0, 0, 0);
        foodBsaketBlanks[3, 2].rotation = new Quaternion(0, 0, 0, 0);
        foodBsaketBlanks[3, 2].scale = new Vector2(1, 1);

    }

    public static void ChangeCursor(Texture2D cursor)
    {
        Cursor.SetCursor(cursor, new Vector2(cursor.width / 2, cursor.height / 2), CursorMode.Auto);
    }

    private static void ActivateOutline(Transform obj)
    {
        outline_objs[obj.name] = true;
        obj.GetComponent<Outline>().enabled = true;
    }

    private static void RemoveOutline(Transform obj)
    {
        outline_objs[obj.name] = false;
        obj.GetComponent<Outline>().enabled = false;
    }

    public static void ClearAllOutline()
    {
        if(outline_objects.Count!=0)
        {
            foreach (GameObject gameObject in outline_objects)
            {
                gameObject.GetComponent<Outline>().enabled = false;
            }
        }
        
    }

    private static string temp_name = "";
    private static Transform temp_object = null;
    private static void OutlineHandler()
    {
        if (outline_objs.Count != 0)
        {
            RaycastHit hit;

            var ray = current_camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                GameObject hit_obj = hit.collider.gameObject;

                if (hit_obj.tag == "sliced_food")
                {
                    //Debug.Log("hellodsadas");
                    hit_obj = hit_obj.transform.parent.gameObject;
                }
              
                if (outline_objs.ContainsKey(hit_obj.name))
                {
                    if (temp_name != hit_obj.name && temp_name != "")
                    {
                        if(temp_object)
                            RemoveOutline(temp_object);
                    }
                    temp_name = hit_obj.name;
                    temp_object = hit_obj.transform;

                    hit_obj.GetComponent<Outline>().enabled = true;
                    bool is_outline_on = outline_objs[hit_obj.name];
                    mouse_hovered_obj = hit_obj.transform;
                    if (!is_outline_on)
                        ActivateOutline(mouse_hovered_obj);
                }
                else
                {

                    if (mouse_hovered_obj)
                        RemoveOutline(mouse_hovered_obj);
                    mouse_hovered_obj = null;
                }
            }
            else
            {
                if (mouse_hovered_obj)
                    RemoveOutline(mouse_hovered_obj);
                mouse_hovered_obj = null;
            }
        }
    }

    public static void MouseHover()
    {
        OutlineHandler();
    }

    public static void InitializeAreaAnchors(List<GameObject> destinations)
    {
        area_anchors.Clear();
        foreach (var dest in destinations)
        {
            area_anchors.Add(dest.name, dest.transform.position);
        }
    }

    public static void InitializeObjOutlines(List<GameObject> objs)
    {
        outline_objects = objs;
        outline_objs.Clear();
        foreach (var obj in objs)
        {
            obj.GetComponent<Outline>().enabled = false;
            outline_objs.Add(obj.name, false);
        }
    }

    public static string MoveNavMeshAgent()
    {
        if (!mouse_hovered_obj) return "empty";
        string anchor_name = mouse_hovered_obj.name + "_Anchor";
        if (area_anchors.ContainsKey(anchor_name))
        {
            var destination = area_anchors[anchor_name];
            navMeshAgent.SetDestination(destination);
            //Debug.Log(mouse_hovered_obj.name);
            return anchor_name;
        }
        else
        {
            Debug.Log("cannot find destination!");
            return "empty";
        }
    }

   
}
