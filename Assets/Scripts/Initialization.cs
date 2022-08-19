using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class Initialization : MonoBehaviour
{
    // test button
    public List<GameObject> act_btn_arr = new List<GameObject>();
    public List<Camera> cam_arr = new List<Camera>();
    public List<GameObject> cam_btn_arr = new List<GameObject>();
  

    // UI object
    public RandomEventUI randomEventUI;

    // player object
    public Player player;

    // might useless
    public PrepareFoodQTE PrepareFoodQTE;
    public PickFood PickFood;

    public NavMeshAgent navMeshAgent;

    PlayerChangeInfo PlayerChangeInfo;
    void Start()
    {
        // initialize cam_arr & btns
        GlobalFunctions.cam_arr = cam_arr;
        for (int i = 0; i < cam_btn_arr.Count; i++)
        {
            int idx = i;
            cam_btn_arr[i].GetComponent<Button>().onClick.AddListener(() => GlobalFunctions.ChangeCamera(idx));
        }

        // initialize act_arr & btns
        GlobalFunctions.InitializeAction(player);

        for (int i = 0; i < act_btn_arr.Count; i++)
        {
            int idx = i;
            act_btn_arr[i].GetComponent<Button>().onClick.AddListener(() => GlobalFunctions.InvokeAction(idx));
        }

        // initialize UI
        GlobalFunctions.randomEventUI = randomEventUI;

        // initialize player
        GlobalFunctions.player = player;

        // initialize game manager
        GameManager.InitGameManager();

        // initialize AI
        GlobalFunctions.navMeshAgent = navMeshAgent;

        // might useless
        GlobalFunctions.PrepareFoodQTE= PrepareFoodQTE;
        GlobalFunctions.PickFood = PickFood;
        GlobalFunctions.InitFoodBasket();

        // read random event csv
        string random_event_file_path = Application.streamingAssetsPath + "\\data.csv";
        GlobalFunctions.random_event_dt = GlobalFunctions.ReadCSV(random_event_file_path);

        // read recipe csv
        string recipe_file_path = Application.streamingAssetsPath + "\\cook.csv";
        GlobalFunctions.ReadRecipeCSV(recipe_file_path);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
