using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using Unity.VisualScripting;
using UnityEngine.TextCore.Text;
using UnityEngine.Splines;
using UnityEngine.EventSystems;

public class DialogWindow : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI MainText;

    [SerializeField] Button Option1;
    [SerializeField] TextMeshProUGUI Option1Text;
    [SerializeField] Button Option2;
    [SerializeField] TextMeshProUGUI Option2Text;
    [SerializeField] Button Option3;
    [SerializeField] TextMeshProUGUI Option3Text;

    [SerializeField] Image LeftCharacterImage;
    [SerializeField] Image RightCharacterImage;

    Action<DialogOptions> Callback;

    private void Awake()
    {
        LeftCharacterImage.gameObject.SetActive(false);
        RightCharacterImage.gameObject.SetActive(false);
    }

    private void Start()
    {
        Option1.onClick.AddListener(() => OptionSelectedCallback(DialogOptions.Option1));
        Option2.onClick.AddListener(() => OptionSelectedCallback(DialogOptions.Option2));
        Option3.onClick.AddListener(() => OptionSelectedCallback(DialogOptions.Option3));
    }

    private void Update()
    {
        if (EventSystem.current.currentSelectedGameObject == null)
        {
            Option2.Select();
        }
    }

    public void ShowText(CharacterEnum character, Sprite characterSprite, string text, Action<DialogOptions> callBack,
        string option1text = null, string option2text = null, string option3text = null)
    {
        gameObject.SetActive(true);
        GlobalManager.Instance.OnDialogShown?.Invoke();

        // main text
        var characterColor = GetCharacterColor(character);
        MainText.text = $"<color=#{characterColor.ToHexString()}>{GetCharacterName(character)}</color> {text}";

        // show sprite
        ShowSprite(character, characterSprite);

        // options
        Option1Text.text = option1text;
        Option2Text.text = option2text;
        Option3Text.text = option3text;
        Option1.gameObject.SetActive(option1text != null && option1text != string.Empty);
        Option2.gameObject.SetActive(option2text != null && option2text != string.Empty);
        Option3.gameObject.SetActive(option3text != null && option3text != string.Empty);

        if (!Option1.gameObject.activeSelf && !Option2.gameObject.activeSelf && !Option3.gameObject.activeSelf)
        {
            Option2Text.text = "Continue";
            Option2.gameObject.SetActive(true);
        }
        
        Option2.Select();

        // callback
        Callback = callBack;
    }

    private void OptionSelectedCallback(DialogOptions dialogOption)
    {
        GlobalManager.Instance.OnDialogClosed?.Invoke();
        gameObject.SetActive(false);
        
        Debug.Log ($"Option Selected Callback: {dialogOption}");
        Callback?.Invoke(dialogOption);

    }

    private Color GetCharacterColor(CharacterEnum character)
    {
        // note: colors and names of characters are not yet final, this is just to test the code for now 
        switch (character)
        {
            case CharacterEnum.Hero:
                return Color.blue;
            case CharacterEnum.Princess:
                return Color.magenta;
            default:
                return Color.green;
        }
    }

    private void ShowSprite(CharacterEnum character, Sprite sprite)
    {
        if (character == CharacterEnum.Hero)
        {
            LeftCharacterImage.gameObject.SetActive(true);
            RightCharacterImage.gameObject.SetActive(false);
        }
        else
        {
            LeftCharacterImage.gameObject.SetActive(false);
            RightCharacterImage.gameObject.SetActive(sprite != null);

            RightCharacterImage.sprite = sprite;
        }
    }

    private string GetCharacterName(CharacterEnum character)
    {
        if (character == CharacterEnum.Princess)
        {
            return "Snow White";
        }
        else
        {
            return character.ToString();
        }

    }
}

public enum DialogOptions
{
    Option1, Option2, Option3
}

public enum CharacterEnum
{
    Hero, Princess, Profa, Smudla, Bambule, Cmunda, Drimal, Kejchal, Stydlin
}
