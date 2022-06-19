using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using TMPro;


public class PrepareFoodQTE : MonoBehaviour
{
    public GameObject food;
    public GameObject knife;
    public GameObject total_score_text;

    float game_time = 0;
    public bool is_active;
    List<GameObject> food_instance_array = new List<GameObject>();
    List<bool> is_cut = new List<bool>();
    int total_score = 0;

    // Start is called before the first frame update
    void Start()
    {
        is_active = false;
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        game_time += Time.deltaTime;
        
        knife.GetComponent<RectTransform>().SetPositionAndRotation(new Vector3(650 + 960, 140, 0), new Quaternion(0, 0, 0, 0));
        knife.transform.SetSiblingIndex(33);
        
        for (int i = 0; i < food_instance_array.Count; i++)
        {
            if (game_time >= float.Parse(GlobalFunctions.food_qte_dt.Rows[i][1].ToString()))
            {
                food_instance_array[i].GetComponent<RectTransform>().SetPositionAndRotation(new Vector3(food_instance_array[i].GetComponent<RectTransform>().position.x + Time.deltaTime * float.Parse(GlobalFunctions.food_qte_dt.Rows[i][3].ToString()), 140, 0), new Quaternion(0, 0, 0, 0));
                if (!is_cut[i])
                {
                    food_instance_array[i].GetComponent<Image>().enabled = true;
                }
                
            }
            if(food_instance_array[i].GetComponent<RectTransform>().position.x>=850+960)
            {
                food_instance_array[i].GetComponent<Image>().enabled = false;
            }
        }
        if (Input.GetMouseButtonDown(0))
        {
            for (int i = 0; i < food_instance_array.Count; i++)
            {
                if (knife.GetComponent<RectTransform>().position.x >= food_instance_array[i].GetComponent<RectTransform>().position.x - float.Parse(GlobalFunctions.food_qte_dt.Rows[i][2].ToString()) && knife.GetComponent<RectTransform>().position.x <= food_instance_array[i].GetComponent<RectTransform>().position.x + float.Parse(GlobalFunctions.food_qte_dt.Rows[i][2].ToString()))
                {
                    food_instance_array[i].GetComponent<Image>().enabled = false;
                    is_cut[i] = true;
                    total_score += 1;
                }
            }
        }

        total_score_text.GetComponent<TextMeshProUGUI>().text = total_score.ToString();
    }

    public void ActiveQTE()
    {
        gameObject.SetActive(true);
        game_time = 0;
        is_active = true;

        string csv_file_path = Application.streamingAssetsPath + "\\PotatoQTE.csv";
        GlobalFunctions.food_qte_dt = GlobalFunctions.ReadCSV(csv_file_path);


        for (int i = 0; i < 30; i++)
        {
            GameObject food_instance = Instantiate(food);
            food_instance.GetComponent<RectTransform>().parent = food.GetComponent<RectTransform>().parent;
            food_instance.GetComponent<RectTransform>().position = food.GetComponent<RectTransform>().position;
            food_instance.tag = "PrepareFood";
            food_instance.GetComponent<Image>().enabled = false;
            
            food_instance_array.Add(food_instance);
            is_cut.Add(false);
            
        }
        food.GetComponent<Image>().enabled = false;

       
    }
    public void DeactiveQTE()
    {
        foreach (GameObject gameObject in GameObject.FindGameObjectsWithTag("PrepareFood"))
        {
            Destroy(gameObject);
        }
        gameObject.SetActive(false);
        is_active = false;
    }

   
}
