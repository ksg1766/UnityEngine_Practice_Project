using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define
{
    public enum WorldObject
    {
        Unknown,
        Player,
        Monster,
    }

    public enum State
    {
        Die,
        Moving,
        Idle,
        Skill,
        Skill1,
    }

    public enum Layer
    {
        Monster = 8,
        Ground = 9,
        Block = 10,
    }

    public enum Scene
    {
        Unknown,
        Login,
        Lobby,
        Game,
        TestScene,
        SignUp
    }

    public enum Sound
    { 
        Bgm,
        Effect,
        MaxCount,
    }

    public enum UIEvent
    { 
        Click,
        Drag,
    }

    public enum MouseEvent
    {
        Press,
        PointerDown,
        PointerUp,
        Click,
        //Wheel,
    }

    public enum KeyboardEvent
    {
        Key_G,
        Key_I,
        Key_ESC,
        Key_W,
        Key_1,
        Key_2,
        Key_3,
    }

    public enum CameraMode
    {
        QuarterView,
    }
}