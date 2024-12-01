using UnityEngine;

public class DeleteOnDialogCompletion : MonoBehaviour
{
    public DialogInstance dialog;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        dialog.DialogFinishedSuccessfully += DialogFinishedSuccessfully;
    }

    private void DialogFinishedSuccessfully()
    {
        gameObject.SetActive(false);
    }
}
