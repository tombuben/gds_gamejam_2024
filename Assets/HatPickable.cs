using System;
using Unity.VisualScripting;
using UnityEngine;

public class HatPickable : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        GlobalManager.Instance.PickedUpHat = true;
        GlobalManager.Instance.OnHatPickedUp?.Invoke();
        Destroy(gameObject);
    }
}
