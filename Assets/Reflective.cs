using System;
using System.Reflection;
using UnityEngine;

public class Reflective : MonoBehaviour
{
    void Start()
    {
        Assembly asm = Assembly.GetExecutingAssembly();
        foreach(Type t in asm.GetTypes())
        {
            if(t != this.GetType())
            {
                foreach(var pi in t.GetProperties())
                {
                    Debug.Log(t.Name + " member: " + pi.Name);
                }
            }
        }
    }
}
