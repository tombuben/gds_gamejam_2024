using UnityEngine;

public class GameStartDialog : DialogInstance
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        base.Start();
        
        DialogOpened?.Invoke();
        ShowCurrentNode();
    }

}
