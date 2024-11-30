using System;
using UnityEngine;

public class GlobalManager : MonoBehaviour
{
    public static GlobalManager Instance;

    
    public Action<bool> togglePerspective; // true when transitioning to 3d
    public Action<bool, float> GroundedChanged; // true + fall speed when touched down, false when left ground
    public Action Jumped; // called when jumped
    
    
    public bool is3D;
    
    
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
            togglePerspective?.Invoke(is3D);
        }
    }
}
