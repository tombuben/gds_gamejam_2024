using UnityEngine;

public class EnableIn3D : MonoBehaviour
{
    public bool disableIn3D;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        OnTogglePerspective(GlobalManager.Instance.is3D);
        GlobalManager.Instance.OnTogglePerspective += OnTogglePerspective; 
    }

    private void OnTogglePerspective(bool is3D)
    {
        gameObject.SetActive(disableIn3D ? !is3D : is3D);
    }
}
