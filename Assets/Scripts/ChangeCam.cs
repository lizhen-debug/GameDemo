using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeCam : MonoBehaviour
{
    public List<GameObject> cam_arr = new List<GameObject>();
    public List<GameObject> cam_btn_arr = new List<GameObject>();

    void Start()
    {
        GlobalFunctions.cam_arr = cam_arr;
        
        for (int i = 0; i < cam_btn_arr.Count; i++)
        {
            int idx = i;
            cam_btn_arr[i].GetComponent<Button>().onClick.AddListener(() => GlobalFunctions.ChangeCamera(idx));
        }


    }

}
