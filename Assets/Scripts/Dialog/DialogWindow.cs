using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using Unity.VisualScripting;

public class DialogWindow : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI MainText;

    [SerializeField] Button Option1;
    [SerializeField] TextMeshProUGUI Option1Text;
    [SerializeField] Button Option2;
    [SerializeField] TextMeshProUGUI Option2Text;
    [SerializeField] Button Option3;
    [SerializeField] TextMeshProUGUI Option3Text;

    Action<DialogOptions> Callback;
    
    private void Start()
    {
        Option1.onClick.AddListener(() => OptionSelectedCallback(DialogOptions.Option1));
        Option2.onClick.AddListener(() => OptionSelectedCallback(DialogOptions.Option2));
        Option3.onClick.AddListener(() => OptionSelectedCallback(DialogOptions.Option3));
    }

    public void ShowText(string characterName, string text, Action<DialogOptions> callBack,
        string option1text = null, string option2text = null, string option3text = null)
    {
        gameObject.SetActive(true);

        // main text
        var characterColor = GetCharacterColor(characterName);
        MainText.text = $"<color={characterColor.ToHexString()}>{characterName}</color> {text}";

        // TODO: show the sprite of the character speaking
        // ...  

        // options
        Option1Text.text = option1text;
        Option2Text.text = option2text;
        Option3Text.text = option3text;
        Option1.gameObject.SetActive(option1text != null || option1text != string.Empty);
        Option2.gameObject.SetActive(option2text != null || option2text != string.Empty);
        Option3.gameObject.SetActive(option3text != null || option3text != string.Empty);

        if (!Option1.gameObject.activeSelf && !Option2.gameObject.activeSelf && !Option3.gameObject.activeSelf)
        {
            option3text = "Continue";
            Option3.gameObject.SetActive(true);
        }
        
        // callback
        Callback = callBack;
    }

    private void OptionSelectedCallback(DialogOptions dialogOption)
    {
        if (Callback != null)
        {
            Callback(dialogOption);
        }

        gameObject.SetActive(false);
    }

    private Color GetCharacterColor(string characterName)
    {
        // note: colors and names of characters are not yet final, this is just to test the code for now 
        switch (characterName)
        {
            case "Princess":
                return Color.red;
            default:
                return Color.black;
        }
    }
}

public enum DialogOptions
{
    Option1, Option2, Option3
}
