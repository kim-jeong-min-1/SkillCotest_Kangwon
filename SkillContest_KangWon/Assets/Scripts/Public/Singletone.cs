using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singletone<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T inst;
    public static T Inst
    {
        get
        {
            if(inst == null)
            {
                inst = FindObjectOfType<T>();
            }
            return inst;
        }
    }

    protected virtual void Awake()
    {
        inst = FindObjectOfType<T>();
    }
}
