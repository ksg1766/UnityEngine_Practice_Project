using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniCamController : MonoBehaviour
{
    [SerializeField]
    GameObject _player = null;

    [SerializeField]
    Vector3 _cameraDirection = new Vector3(0.0f, 100.0f, 0.0f);

    public void SetPlayer(GameObject player) { _player = player; }

    void Start()
    {
        transform.position = _player.transform.position + _cameraDirection;
    }

    void LateUpdate()
    {
        transform.position = _player.transform.position + _cameraDirection;
        transform.LookAt(_player.transform.position);
    }
}
