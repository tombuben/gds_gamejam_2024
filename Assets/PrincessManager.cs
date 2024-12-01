using UnityEngine;

public class PrincessManager : MonoBehaviour
{
    public DialogInstance NoHurtDialog;
    public DialogInstance HurtDwarvesDailog;
    public DialogInstance ApologizedToAllDialog;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        NoHurtDialog.gameObject.SetActive(true);
        HurtDwarvesDailog.gameObject.SetActive(false);
        ApologizedToAllDialog.gameObject.SetActive(false);
        
        GlobalManager.Instance.TrpaslikKilled += TrpaslikKilled;
        
        NoHurtDialog.DialogFinishedSuccessfully += ShowEndScreen;
        NoHurtDialog.DialogFinishedUnsuccessfully += ShowEndScreen;
    }

    private void TrpaslikKilled()
    {
        NoHurtDialog.gameObject.SetActive(false);
        HurtDwarvesDailog.gameObject.SetActive(true);
        ApologizedToAllDialog.gameObject.SetActive(false);
        
        GlobalManager.Instance.TrpaslikKilled -= TrpaslikKilled;
        GlobalManager.Instance.TrpaslikApologized += TrpaslikApologized;
        
        HurtDwarvesDailog.DialogFinishedSuccessfully += GoTo3D;
        HurtDwarvesDailog.DialogFinishedUnsuccessfully += GoTo3D;
    }

    private void GoTo3D()
    {
        GlobalManager.Instance.GoTo3D(true);
    }

    private void TrpaslikApologized()
    {
        if (GlobalManager.Instance.killCount <= GlobalManager.Instance.apologizeCount)
        {
            GlobalManager.Instance.TrpaslikApologized -= TrpaslikApologized;
            
            NoHurtDialog.gameObject.SetActive(false);
            HurtDwarvesDailog.gameObject.SetActive(false);
            ApologizedToAllDialog.gameObject.SetActive(true);
            
            
            ApologizedToAllDialog.DialogFinishedSuccessfully += ShowEndScreen;
            ApologizedToAllDialog.DialogFinishedUnsuccessfully += ShowEndScreen;
        }
    }

    private void ShowEndScreen()
    {
        GlobalManager.Instance.GameWon?.Invoke();
        GlobalManager.Instance.GoTo3D(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
