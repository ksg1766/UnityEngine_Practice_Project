using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterGravity : MonoBehaviour
{
    private int layerMask;

    private Vector3 height;
    private float radius;
    private Vector3 width;

    private RaycastHit[] hit;

    // Start is called before the first frame update
    void Start()
    {
        layerMask = 1 << LayerMask.NameToLayer("Ground");

        height = new Vector3(0, 0.55f, 0);

        hit = new RaycastHit[5];
    }

    // Update is called once per frame
    void Update()
    {
        if (Physics.Raycast(transform.position + height, Vector3.down, out hit[0], float.MaxValue, layerMask))
        {
            if (name == "Knight")
            {
                radius = GetComponent<CapsuleCollider>().radius;
                Physics.Raycast(transform.position + height + new Vector3(radius, 0, 0), Vector3.down, out hit[1], float.MaxValue, layerMask);
                Physics.Raycast(transform.position + height + new Vector3(-radius, 0, 0), Vector3.down, out hit[2], float.MaxValue, layerMask);
                Physics.Raycast(transform.position + height + new Vector3(0, 0, radius), Vector3.down, out hit[3], float.MaxValue, layerMask);
                Physics.Raycast(transform.position + height + new Vector3(0, 0, -radius), Vector3.down, out hit[4], float.MaxValue, layerMask);

                Debug.DrawRay(transform.position + height, Vector3.down, Color.green);
                Debug.DrawRay(transform.position + height + new Vector3(radius, 0, 0), Vector3.down, Color.green);
                Debug.DrawRay(transform.position + height + new Vector3(-radius, 0, 0), Vector3.down, Color.green);
                Debug.DrawRay(transform.position + height + new Vector3(0, 0, radius), Vector3.down, Color.green);
                Debug.DrawRay(transform.position + height + new Vector3(0, 0, -radius), Vector3.down, Color.green);
            }
            else if(name == "Spider")
            {
                width = GetComponent<BoxCollider>().size;
                Physics.Raycast(transform.position + height + new Vector3(width.x/2, 0, 0), Vector3.down, out hit[1], float.MaxValue, layerMask);
                Physics.Raycast(transform.position + height + new Vector3(-width.x/2, 0, 0), Vector3.down, out hit[2], float.MaxValue, layerMask);
                Physics.Raycast(transform.position + height + new Vector3(0, 0, width.z/2), Vector3.down, out hit[3], float.MaxValue, layerMask);
                Physics.Raycast(transform.position + height + new Vector3(0, 0, -width.z/2), Vector3.down, out hit[4], float.MaxValue, layerMask);

                Debug.DrawRay(transform.position + height, Vector3.down, Color.green);
                Debug.DrawRay(transform.position + height + new Vector3(width.x / 2, 0, 0), Vector3.down, Color.green);
                Debug.DrawRay(transform.position + height + new Vector3(-width.x / 2, 0, 0), Vector3.down, Color.green);
                Debug.DrawRay(transform.position + height + new Vector3(0, 0, width.z / 2), Vector3.down, Color.green);
                Debug.DrawRay(transform.position + height + new Vector3(0, 0, -width.z / 2), Vector3.down, Color.green);
            }   

            float distance = (hit[0].distance + hit[1].distance + hit[2].distance + hit[3].distance + hit[4].distance) / 5.0f;

            //������ ������ ���� �� ������ �������� ������ �а� �� / �߷� ���� ����
            if (distance > 0.6f)
            {
                transform.Translate(new Vector3(0, -4.9f, 0) * Time.deltaTime);
            }
            else if (distance < 0.5f)
            {
                transform.Translate(new Vector3(0, 4.9f, 0) * Time.deltaTime);
            }
        }
        else
        {
            //������ ������ ����
            transform.Translate(new Vector3(0, -4.9f, 0) * Time.deltaTime);
        }
    }
}