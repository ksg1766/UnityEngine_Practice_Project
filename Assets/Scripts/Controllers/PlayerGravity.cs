using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGravity : MonoBehaviour
{
    private int layerMask;

    // Start is called before the first frame update
    void Start()
    {
        layerMask = 1 << LayerMask.NameToLayer("Ground");
    }

    // Update is called once per frame
    void Update()
    {
        if(Physics.SphereCast(transform.position + new Vector3(0, 0.9f, 0), 0.3f, Vector3.down, out RaycastHit hit, float.MaxValue, layerMask))
        {
            //지면이 고르지 못할 때 떨림을 막기위해 간격을 넓게 함
            if (hit.distance > 0.75f)
            {
                transform.Translate(new Vector3(0, -4.9f, 0) * Time.deltaTime);
            }
            else if (hit.distance < 0.45f)
            {
                transform.Translate(new Vector3(0, 4.9f, 0) * Time.deltaTime);
            }
        }
        else
        {
            transform.Translate(new Vector3(0, -4.9f, 0) * Time.deltaTime);
        }
    }
}
