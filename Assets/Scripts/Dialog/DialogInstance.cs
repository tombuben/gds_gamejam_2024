using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DialogInstance : MonoBehaviour
{
    [SerializeField] int DialogStartIndex;
    private int CurrentDialogIndex;

    [SerializeField] bool DoUpdate = false;

    [SerializeField] List<DialogNode> DialogNodes;

    private DialogWindow DialogWindow;

    public Action DialogOpened;
    
    public Action DialogFinishedSuccessfully;
    public Action DialogFinishedUnsuccessfully;

    private void Start()
    {
        CurrentDialogIndex = DialogStartIndex;

        DialogWindow = GlobalManager.Instance.DialogWindow;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out CharacterController cc))
        {
            DoUpdate = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out CharacterController cc))
        {
            DoUpdate = false;
        }
    }

    private void Update()
    {
        if (!DoUpdate)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            DialogOpened?.Invoke();
            ShowCurrentNode();
        }
    }

    protected virtual void Callback(DialogOptions dialogOptions)
    {
        DialogNode node = DialogNodes[CurrentDialogIndex];

        switch (dialogOptions)
        {
            case DialogOptions.Option1:
                CurrentDialogIndex = node.Option1NextNodeIndex;
                break;
            case DialogOptions.Option2:
                CurrentDialogIndex = node.Option2NextNodeIndex;
                break;
            case DialogOptions.Option3:
                CurrentDialogIndex = node.Option3NextNodeIndex;
                break;
        }


        if (CurrentDialogIndex == -1) // successfull end of the dialog
        {
            DialogFinishedSuccessfully?.Invoke();
        }
        else if (CurrentDialogIndex == -2) // unsuccessfull end of the dialog
        {
            DialogFinishedUnsuccessfully?.Invoke();
        }
        else
        {
            ShowCurrentNode();
        }
    }

    private void ShowCurrentNode()
    {
        if (CurrentDialogIndex < 0 || CurrentDialogIndex >= DialogNodes.Count)
        {
            return;
        }

        DialogNode node = DialogNodes[CurrentDialogIndex];

        DialogWindow.ShowText(
            character: node.character,
            text: node.text,
            callBack: Callback,
            option1text: node.option1Text,
            option2text: node.option2Text,
            option3text: node.option3Text
            );
    }
}

[Serializable]
public class DialogNode
{
    public Characters character;
    public string text;
    public string option1Text;
    public int Option1NextNodeIndex;
    public string option2Text;
    public int Option2NextNodeIndex;
    public string option3Text;
    public int Option3NextNodeIndex;
}