using Unity.VisualScripting;
using UnityEngine;

public class HatFetchQuest : MonoBehaviour
{
    public DialogInstance FirstDialog;
    public DialogInstance NoHatDialog;
    public DialogInstance WithHatDialog;

    public GameObject hatless;
    public GameObject withHat;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        FirstDialog.gameObject.SetActive(true);
        NoHatDialog.gameObject.SetActive(false);
        WithHatDialog.gameObject.SetActive(false);
        hatless.SetActive(true);
        withHat.SetActive(false);

        FirstDialog.DialogFinishedSuccessfully += UpdateHat;
        FirstDialog.DialogFinishedUnsuccessfully += UpdateHat;
        GlobalManager.Instance.OnHatPickedUp += UpdateHat;
    }

    private void UpdateHat()
    {
        if (GlobalManager.Instance.PickedUpHat)
        {
            FirstDialog.gameObject.SetActive(false);
            NoHatDialog.gameObject.SetActive(false);
            WithHatDialog.gameObject.SetActive(true);
            
            FirstDialog.DialogFinishedSuccessfully -= UpdateHat;
            FirstDialog.DialogFinishedUnsuccessfully -= UpdateHat;
            WithHatDialog.DialogFinishedSuccessfully += UpdateHatVisuals;
            WithHatDialog.DialogFinishedUnsuccessfully += UpdateHatVisuals;
        }
        else
        {
            FirstDialog.gameObject.SetActive(false);
            NoHatDialog.gameObject.SetActive(true);
            WithHatDialog.gameObject.SetActive(false);
        }
    }

    private void UpdateHatVisuals()
    {
        Debug.Log("Update hat visuals");
        GlobalManager.Instance.PickedUpHat = false;
        GlobalManager.Instance.OnHatLost?.Invoke();
        hatless.SetActive(false);
        withHat.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
