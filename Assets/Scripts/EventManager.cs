using UnityEngine;
using System.Collections;
using System;

public class EventManager : MonoBehaviour 
{
    public delegate void ChangedAction();
    public static event ChangedAction OnChanged;

   
}