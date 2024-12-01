using UnityEngine;

public class HatCollectableUI : MonoBehaviour
{
    void Start()
    {
        gameObject.SetActive(false);

        GlobalManager.Instance.OnHatPickedUp += () => { gameObject.SetActive(true); };
        GlobalManager.Instance.OnHatLost += () => { gameObject.SetActive(false); };
    }
}
