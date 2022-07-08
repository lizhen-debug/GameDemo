using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Initialization : MonoBehaviour
{
    public List<GameObject> act_btn_arr = new List<GameObject>();

    public List<GameObject> cam_arr = new List<GameObject>();
    public List<GameObject> cam_btn_arr = new List<GameObject>();

    public RandomEventUI randomEventUI;
    public PrepareFoodQTE PrepareFoodQTE;
    public PickFood PickFood;

    public Player player;
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
        GlobalFunctions.randomEventUI = randomEventUI;
        GlobalFunctions.PrepareFoodQTE= PrepareFoodQTE;
        GlobalFunctions.PickFood = PickFood;
        for (int i = 0; i < act_btn_arr.Count; i++)
        {
            int idx = i;
            act_btn_arr[i].GetComponent<Button>().onClick.AddListener(() => GlobalFunctions.InvokeAction(idx));
        }

        string csv_file_path = Application.streamingAssetsPath + "\\data.csv";
        GlobalFunctions.random_event_dt = GlobalFunctions.ReadCSV(csv_file_path);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
