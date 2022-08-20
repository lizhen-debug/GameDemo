using UnityEngine;
using System.Collections;

public class DragObject : MonoBehaviour
{
    /// <summary>
    /// ��Ҫ�϶�������
    /// </summary>
    private Transform _dragGameObject;

    /// <summary>
    /// ��ȡ������Ҫ��ײ�Ĳ�
    /// </summary>
    private LayerMask _canDrag;

    /// <summary>
    /// ֱ�Ӵ��ⲿ����ò㣬�����
    /// </summary>
    public LayerMask canDrag2;

    /// <summary>
    /// �������λ�ú�cubeλ�ò�
    /// </summary>
    private Vector3 _offset;

    /// <summary>
    /// �Ƿ�����cube
    /// </summary>
    private bool _isClickCube;

    /// <summary>
    /// Ŀ��������Ļ����
    /// </summary>
    private Vector3 _targetScreenPoint;

    //�����϶���Χ����Сֵ�����ֵ
    public float xMin, xMax, yMin, yMax, zMin, zMax;

    // Use this for initialization
    private void Start()
    {
        _canDrag = 1 << LayerMask.NameToLayer("Food");
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (CheckGameObject())
            {
                _offset = _dragGameObject.transform.position -
                          Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y,
                              _targetScreenPoint.z));
            }
        }

        if (_isClickCube)
        {
            //��ǰ������ڵ���Ļ����
            Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, _targetScreenPoint.z);
            //�ѵ�ǰ������Ļ����ת������������
            Vector3 curWorldPoint = Camera.main.ScreenToWorldPoint(curScreenPoint);
            _dragGameObject.position = curWorldPoint + _offset;

            //�����϶���Χ
            _dragGameObject.GetComponent<Rigidbody>().transform.position = new Vector3(
                Mathf.Clamp(_dragGameObject.GetComponent<Rigidbody>().transform.position.x, xMin, xMax),
                Mathf.Clamp(_dragGameObject.GetComponent<Rigidbody>().transform.position.y, yMin, yMax),
                Mathf.Clamp(_dragGameObject.GetComponent<Rigidbody>().transform.position.z, zMin, zMax)
            );
        }

        if (Input.GetMouseButtonUp(0))
        {
            _isClickCube = false;
            if(_dragGameObject)
                _dragGameObject.GetComponent<Rigidbody>().isKinematic = false;
            //�ָ���ק�����Y��Ϊԭ��
            //_dragGameObject.GetComponent<Rigidbody>().transform.position = new Vector3(
            //    _dragGameObject.GetComponent<Rigidbody>().transform.position.x, 0,
            //    _dragGameObject.GetComponent<Rigidbody>().transform.position.z);
        }
    }

    /// <summary>
    /// ����Ƿ�����cbue
    /// </summary>
    /// <returns></returns>
    private bool CheckGameObject()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo, 100f, _canDrag))
        {
            _isClickCube = true;
            //�õ�������ײ��������
            _dragGameObject = hitInfo.collider.gameObject.transform;
            _targetScreenPoint = Camera.main.WorldToScreenPoint(_dragGameObject.position);
            _dragGameObject.GetComponent<Rigidbody>().isKinematic = true;
            return true;
        }

        return false;
    }
}