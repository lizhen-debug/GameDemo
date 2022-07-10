using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class knife_move : MonoBehaviour
{
    float timer = 0;
    int hit_frame_defined = 10;
    int hit_frame;
    float val;
    // Start is called before the first frame update
    void Start()
    {
        hit_frame = hit_frame_defined;
    }

    // Update is called once per frame
    void Update()
    {

        if (hit_frame > 0 && val > -0.1 && val < 0.1)
        {
            Debug.Log("if: " + hit_frame);
            hit_frame--;
        }
        else
        {
            Debug.Log("else: " + hit_frame);
            timer += Time.fixedDeltaTime;
            val = Mathf.Sin(timer);
            hit_frame = hit_frame_defined;
            gameObject.transform.position += new Vector3(0, Mathf.Sin(timer), 0);
        }
       
        if (Input.GetKeyDown("space"))
        {
            CutVegetable();
        }
    }

    void CutVegetable()
    {
        gameObject.transform.position += new Vector3(-1, 0, 0);
    }
}
