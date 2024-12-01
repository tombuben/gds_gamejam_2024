using UnityEngine;

public class HatCollectableUI : MonoBehaviour
{
    void Start()
    {
        GlobalManager.Instance.OnHatPickedUp += () => { gameObject.SetActive(true); };
        GlobalManager.Instance.OnHatLost += () => { gameObject.SetActive(false); };
    }
}
