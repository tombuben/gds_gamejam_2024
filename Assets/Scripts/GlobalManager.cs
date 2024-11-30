using System;
using UnityEngine;

public class GlobalManager : MonoBehaviour
{
    public static GlobalManager Instance;

    public bool is3D;
    public Action togglePerspective;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        Instance = this;
        is3D = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            is3D = !is3D;
            togglePerspective?.Invoke();
        }
    }
}
