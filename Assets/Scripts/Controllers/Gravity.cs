using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour
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
        if(Physics.SphereCast(transform.position + new Vector3(0, 0.7f, 0), 0.2f, Vector3.down, out RaycastHit hit, float.MaxValue, layerMask))
        {
            if (hit.distance > 0.5f)
            {
                transform.Translate(new Vector3(0, -9.8f, 0) * Time.deltaTime);
            }
            else if (hit.distance < 0.4f)
            {
                transform.Translate(new Vector3(0, 9.8f, 0) * Time.deltaTime);
            }
        }
        else
        {
            transform.Translate(new Vector3(0, -9.8f, 0) * Time.deltaTime);
        }
    }
}
