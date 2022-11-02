using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager
{
    public Action<Define.KeyboardEvent> KeyAction = null;
    public Action<Define.MouseEvent> MouseAction = null;

    bool _pressed = false;
    float _pressedTime = 0;
    public void OnUpdate()
    {
        if (KeyAction != null) //Input.anyKey && KeyAction != null
        {
            if (Input.GetKeyDown(KeyCode.G))
            {
                KeyAction.Invoke(Define.KeyboardEvent.Key_G);
            }
            else if (Input.GetKeyDown(KeyCode.Escape))
            {
                KeyAction.Invoke(Define.KeyboardEvent.Key_ESC);
            }
            //½ºÅ³
            else if (Input.GetKeyDown(KeyCode.W))
            {
                KeyAction.Invoke(Define.KeyboardEvent.Key_W);
            }
        }

        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (MouseAction != null)
        {
            if (Input.GetMouseButton(0))
            {
                if (!_pressed)
                {
                    MouseAction.Invoke(Define.MouseEvent.PointerDown);
                    _pressedTime = Time.time;
                }
                MouseAction.Invoke(Define.MouseEvent.Press);
                _pressed = true;
            }
            else
            {
                if (_pressed)
                {
                    if (Time.time < _pressedTime + 0.2f)
                        MouseAction.Invoke(Define.MouseEvent.Click);
                    MouseAction.Invoke(Define.MouseEvent.PointerUp);
                }
                _pressed = false;
                _pressedTime = 0;

                _pressed = false;
            }
        }
    }

    public void Clear()
    {
        KeyAction = null;
        MouseAction = null;
    }
}
