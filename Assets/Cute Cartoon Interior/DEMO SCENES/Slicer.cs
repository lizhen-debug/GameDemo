using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EzySlice;
using UnityEditor;

public class Slicer : MonoBehaviour
{
    public GameObject ground;
    public GameObject object_to_slice;
    public Material banana;

    public float movespeed = 5;

    //public Canvas strCanvas;

    /// ��ʼλ��
    private Vector3 StartPosition;

    /// ��갴�µ�֮ǰλ��
    private Vector3 previousPosition;

    /// ��갴��֮��Ļ�������
    private Vector3 offset;

    /// ���̧�������ʼλ�õ�λ��
    private Vector3 finalOffset;

    GameObject slice_plane;
    
    List<GameObject> gameObjects_to_slice= new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        gameObjects_to_slice.Add(object_to_slice);

        slice_plane = GameObject.CreatePrimitive(PrimitiveType.Plane);
        slice_plane.transform.parent = gameObject.transform;
        slice_plane.name = "slice plane";
        slice_plane.transform.position = transform.position;
        slice_plane.transform.rotation = Quaternion.Euler(-90, 90, 0);
        
        slice_plane.GetComponent<MeshRenderer>().enabled = false;
        slice_plane.GetComponent<MeshCollider>().enabled = false;
        //slice_plane.GetComponent<MeshRenderer>().material = (Material)AssetDatabase.LoadAssetAtPath("Assets/Brick Project Studio/Apartment Kit/Materials/Surface/White Wine.mat", typeof(Material));
        //slice_plane.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        //
        //slice_plane.GetComponent<MeshCollider>().convex = true;
        //slice_plane.GetComponent<MeshCollider>().isTrigger = true;
    }

    

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))

        {

            transform.Translate(0, 0, movespeed * Time.deltaTime, Space.World);

        }

        if (Input.GetKey(KeyCode.S))

        {

            transform.Translate(0, 0, movespeed * Time.deltaTime * (-1), Space.World);

        }

        if (Input.GetKey(KeyCode.A))

        {

            transform.Translate(movespeed * Time.deltaTime * (-1), 0, 0, Space.World);

        }

        if (Input.GetKey(KeyCode.D))

        {

            transform.Translate(movespeed * Time.deltaTime, 0, 0, Space.World);

        }
        if (Input.GetKey(KeyCode.Space))

        {

            transform.Translate(0, movespeed * Time.deltaTime, 0, Space.World);

        }
        if (Input.GetKey(KeyCode.C))

        {

            transform.Translate(0, movespeed * Time.deltaTime * (-1), 0, Space.World);

        }
        //if (Input.GetMouseButtonDown(0))
        //{
        //    StartPosition = Input.mousePosition;  //��¼��갴�µ�ʱ������λ��
        //    previousPosition = Input.mousePosition;  //��¼�µ�ǰ��һ֡�����λ��
        //}
        //if (Input.GetMouseButton(0))
        //{
        //    offset = Input.mousePosition - previousPosition; //��һ֡����λ�ü�ȥ��һ֡����λ�þ�������ƫ���� 
        //    previousPosition = Input.mousePosition; //�ٴμ�¼��ǰ����λ�ã��Ա���һ֡��offsetʹ�á�
        //    Vector3 Xoffset = new Vector3(-offset.x, 0, 0);//���˵������Y�᷽���ϵ�ƫ������ֻ����X���
        //    transform.Rotate(Vector3.Cross(Xoffset, Vector3.forward).normalized, offset.magnitude, Space.World);  //��ת
        //}
        if (Input.GetKeyDown(KeyCode.Q))
        {
            //foreach (GameObject obj in gameObjects_to_slice)
            //{ 
            //    print(obj.name);
            //}

            //foreach (GameObject obj in gameObjects_to_slice)
            //{
            SlicedHull slicedHull = object_to_slice.Slice(slice_plane.transform.position, slice_plane.transform.up);
            
            GameObject part1 = slicedHull.CreateUpperHull(object_to_slice, banana);
            GameObject part2 = slicedHull.CreateLowerHull(object_to_slice, banana);
                
                part1.AddComponent<MeshCollider>().convex = true;
                part1.AddComponent<Rigidbody>().useGravity=true;
                part1.GetComponent<Rigidbody>().AddForce(new Vector3(50, 50, 0));

            //part2.AddComponent<MeshCollider>().convex = true;
            //part2.AddComponent<Rigidbody>().useGravity = true;
            //part2.GetComponent<Rigidbody>().AddForce(new Vector3(-50, 0, -50));

            Destroy(object_to_slice);

            object_to_slice = part2;

            //}

        }
    }
}
