using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomEventUI : MonoBehaviour
{
    public GameObject origin_btn;
    public GameObject origin_msg;
    public GameObject background_pic;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject AddButton()
    {
        GameObject new_btn = Instantiate(origin_btn);
        new_btn.transform.parent = gameObject.transform;
        new_btn.tag = "clone";
        
        return new_btn;
    }

    public GameObject AddMessageBox()
    {
        GameObject new_msg = Instantiate(origin_msg);
        new_msg.transform.parent = gameObject.transform;
        new_msg.tag = "clone";

        return new_msg;
    }

    public void RemoveAllCloneObjectByTag(string tag_str)
    {
        foreach (GameObject gameObject in GameObject.FindGameObjectsWithTag(tag_str))
        {
            Destroy(gameObject);
        }
    }
}
