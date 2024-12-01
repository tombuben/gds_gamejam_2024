using System;
using UnityEngine;

public class TrpaslikZarazenyVzemi : MonoBehaviour
{
    public DialogInstance zarazenyDialog;
    public DialogInstance vytazenyDialog;

    public GameObject ZatazenyGameObject;
    public GameObject VytazenyGameObject;
    
    // Update is called once per frame
    private void Start()
    {
        vytazenyDialog.gameObject.SetActive(false);
        zarazenyDialog.gameObject.SetActive(true);
        GlobalManager.Instance.SmudlaStartedFollowing += SmudlaStartedFollowing;
    }

    private void SmudlaStartedFollowing()
    {
        vytazenyDialog.gameObject.SetActive(true);
        zarazenyDialog.gameObject.SetActive(false);
        
        vytazenyDialog.DialogFinishedUnsuccessfully += Vytazeny;
        vytazenyDialog.DialogFinishedSuccessfully += Vytazeny;
    }

    private void Vytazeny()
    {
        ZatazenyGameObject.SetActive(false);
        VytazenyGameObject.SetActive(true);
        GlobalManager.Instance.StopSmudlaFollowing?.Invoke();
    }
}
