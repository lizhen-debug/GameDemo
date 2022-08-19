using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;
using UnityEditor;

public class GameManager : MonoBehaviour
{
    // ���������ñ���
    public Texture2D main_scene_cursor_texture;
    public List<GameObject> main_scene_anchors;
    public List<GameObject> main_scene_outline_objects;

    // �����������ñ���
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
        // ���������
        GlobalFunctions.current_camera = GlobalFunctions.cam_arr[0];
        // �����ת��
        GlobalFunctions.ChangeCamera(0);
        // ����ָ����ʽ
        GlobalFunctions.ChangeCursor(main_scene_cursor_texture);
        // ȡ��ȫ��������ʾ
        GlobalFunctions.ClearAllOutline();
        // ���ÿɸ��������б�
        GlobalFunctions.InitializeObjOutlines(main_scene_outline_objects);
        // ����MeshAgentê��
        GlobalFunctions.InitializeAreaAnchors(main_scene_anchors);
    }
    private void CookingConfig()
    {
        // ���������
        GlobalFunctions.current_camera = GlobalFunctions.cam_arr[1];
        // �����ת��
        GlobalFunctions.ChangeCamera(1);
        // ����ָ����ʽ
        GlobalFunctions.ChangeCursor(cooking_cursor_texture);
        // ȡ��ȫ��������ʾ
        GlobalFunctions.ClearAllOutline();
        // ���ÿɸ��������б�
        GlobalFunctions.InitializeObjOutlines(cooking_outline_objects);
        // ����MeshAgentê��
        //GlobalFunctions.InitializeAreaAnchors(cooking_anchors);
        
    }
}

