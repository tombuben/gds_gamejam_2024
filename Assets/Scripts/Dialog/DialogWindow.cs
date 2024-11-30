using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using Unity.VisualScripting;
using UnityEngine.TextCore.Text;

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

    public void ShowText(Characters character, string text, Action<DialogOptions> callBack,
        string option1text = null, string option2text = null, string option3text = null)
    {
        gameObject.SetActive(true);
        GlobalManager.Instance.OnDialogShown.Invoke();

        // main text
        var characterColor = GetCharacterColor(character);
        MainText.text = $"<color=#{characterColor.ToHexString()}>{character}</color> {text}";

        // TODO: show the sprite of the character speaking
        // ...  

        // options
        Option1Text.text = option1text;
        Option2Text.text = option2text;
        Option3Text.text = option3text;
        Option1.gameObject.SetActive(option1text != null && option1text != string.Empty);
        Option2.gameObject.SetActive(option2text != null && option2text != string.Empty);
        Option3.gameObject.SetActive(option3text != null && option3text != string.Empty);

        Option1.Select();

        if (!Option1.gameObject.activeSelf && !Option2.gameObject.activeSelf && !Option3.gameObject.activeSelf)
        {
            Option2Text.text = "Continue";
            Option2.gameObject.SetActive(true);
            Option2.Select();
        }

        // callback
        Callback = callBack;
    }

    private void OptionSelectedCallback(DialogOptions dialogOption)
    {
        gameObject.SetActive(false);

        if (Callback != null)
        {
            Callback?.Invoke(dialogOption);
        }
        GlobalManager.Instance.OnDialogClosed.Invoke();
    }

    private Color GetCharacterColor(Characters character)
    {
        // note: colors and names of characters are not yet final, this is just to test the code for now 
        switch (character)
        {
            case Characters.Hero:
                return Color.blue;
            case Characters.Princess:
                return Color.yellow;
            default:
                return Color.red;
        }
    }
}

public enum DialogOptions
{
    Option1, Option2, Option3
}

public enum Characters
{
    Hero, Princess, Gnome1, Gnome2, Gnome3, Gnome4, Gnome5, Gnome6, Gnome7
}
