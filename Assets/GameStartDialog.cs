using UnityEngine;

public class GameStartDialog : DialogInstance
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    new void Start()
    {
        base.Start();
        
        DialogOpened?.Invoke();
        ShowCurrentNode();
    }

}
