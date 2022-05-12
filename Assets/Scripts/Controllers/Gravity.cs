using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour
{
    private int layerMask;

    private Vector3 height;
    private Vector3 extents;

    private RaycastHit[] hit;

    // Start is called before the first frame update
    void Start()
    {
        layerMask = 1 << LayerMask.NameToLayer("Ground");

        height = new Vector3(0, 1.1f, 0);
        extents = GetComponent<Collider>().bounds.extents;

        hit = new RaycastHit[5];
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Physics.Raycast(transform.position + height, Vector3.down, out hit[0], float.MaxValue, layerMask))
        {
            Physics.Raycast(transform.position + height + new Vector3(extents.x, 0, 0), Vector3.down, out hit[1], float.MaxValue, layerMask);
            Physics.Raycast(transform.position + height + new Vector3(-extents.x, 0, 0), Vector3.down, out hit[2], float.MaxValue, layerMask);
            Physics.Raycast(transform.position + height + new Vector3(0, 0, extents.z), Vector3.down, out hit[3], float.MaxValue, layerMask);
            Physics.Raycast(transform.position + height + new Vector3(0, 0, -extents.z), Vector3.down, out hit[4], float.MaxValue, layerMask);

            Debug.DrawRay(transform.position + height, Vector3.down, Color.green);
            Debug.DrawRay(transform.position + height + new Vector3(extents.x, 0, 0), Vector3.down, Color.green);
            Debug.DrawRay(transform.position + height + new Vector3(-extents.x, 0, 0), Vector3.down, Color.green);
            Debug.DrawRay(transform.position + height + new Vector3(0, 0, extents.z), Vector3.down, Color.green);
            Debug.DrawRay(transform.position + height + new Vector3(0, 0, -extents.z), Vector3.down, Color.green);

            float distance = (hit[0].distance + hit[1].distance + hit[2].distance + hit[3].distance + hit[4].distance) / 5.0f;

            //지면이 고르지 못할 때 떨림을 막기위해 간격을 넓게 함/중력 세기 감소
            if (distance > 1.2f)
            {
                transform.Translate(new Vector3(0, -4.9f, 0) * Time.deltaTime);
            }
            else if (distance < 1.0f)
            {
                transform.Translate(new Vector3(0, 4.9f, 0) * Time.deltaTime);
            }
        }
        else
        {
            //지면이 없으면 낙하
            transform.Translate(new Vector3(0, -4.9f, 0) * Time.deltaTime);
        }
    }
}
