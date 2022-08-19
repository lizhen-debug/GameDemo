using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;
using UnityEditor;

public class GameManager : MonoBehaviour
{
    // 主场景配置变量
    public Texture2D main_scene_cursor_texture;
    public List<GameObject> main_scene_anchors;
    public List<GameObject> main_scene_outline_objects;

    // 做饭场景配置变量
    public Texture2D cooking_cursor_texture;
    public List<GameObject> cooking_anchors;
    public List<GameObject> cooking_outline_objects;

    private static List<Action> ExecuteGameStateCongig = new List<Action>();

    // Start is called before the first frame update
    void Start()
    {
        ExecuteGameStateCongig.Add(MainSceneConfig);
        ExecuteGameStateCongig.Add(CookingConfig);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public static void InitGameManager()
    {
        ChangeGameState(GlobalFunctions.GameState.MainScene);
    }
    public static void ChangeGameState(GlobalFunctions.GameState gameState)
    {
        GlobalFunctions.current_state = gameState;
        ExecuteGameStateCongig[((int)gameState)].Invoke();
    }
    private void MainSceneConfig()
    {
        // 配置摄像机
        GlobalFunctions.current_camera = GlobalFunctions.cam_arr[0];
        // 摄像机转换
        GlobalFunctions.ChangeCamera(0);
        // 配置指针样式
        GlobalFunctions.ChangeCursor(main_scene_cursor_texture);
        // 取消全部高亮显示
        GlobalFunctions.ClearAllOutline();
        // 配置可高亮物体列表
        GlobalFunctions.InitializeObjOutlines(main_scene_outline_objects);
        // 配置MeshAgent锚点
        GlobalFunctions.InitializeAreaAnchors(main_scene_anchors);
    }
    private void CookingConfig()
    {
        // 配置摄像机
        GlobalFunctions.current_camera = GlobalFunctions.cam_arr[1];
        // 摄像机转换
        GlobalFunctions.ChangeCamera(1);
        // 配置指针样式
        GlobalFunctions.ChangeCursor(cooking_cursor_texture);
        // 取消全部高亮显示
        GlobalFunctions.ClearAllOutline();
        // 配置可高亮物体列表
        GlobalFunctions.InitializeObjOutlines(cooking_outline_objects);
        // 配置MeshAgent锚点
        //GlobalFunctions.InitializeAreaAnchors(cooking_anchors);
        
    }
}

