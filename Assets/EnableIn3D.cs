using UnityEngine;

public class EnableIn3D : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameObject.SetActive(GlobalManager.Instance.is3D);
        GlobalManager.Instance.OnTogglePerspective += OnTogglePerspective; 
    }

    private void OnTogglePerspective(bool is3D)
    {
        gameObject.SetActive(is3D);
    }
}
