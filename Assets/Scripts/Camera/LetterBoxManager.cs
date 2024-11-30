using DG.Tweening;
using UnityEngine;

public class LetterBoxManager : MonoBehaviour
{
    public RectTransform leftLetterbox;
    public RectTransform rightLetterbox;

    public float duration = 1f;
    public float letterboxWidth = 160;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GlobalManager.Instance.TogglePerspective += TogglePerspective;
    }

    private void TogglePerspective(bool is3D)
    {
        if (is3D)
        {
            DOTween.To(()=>leftLetterbox.sizeDelta.x,amount=>
            {
                leftLetterbox.sizeDelta = new Vector2(amount, leftLetterbox.sizeDelta.y);
            },0.0f,duration);

            DOTween.To(()=>rightLetterbox.sizeDelta.x,amount=>
            {
                rightLetterbox.sizeDelta = new Vector2(amount, leftLetterbox.sizeDelta.y);
            },0.0f,duration);
        }
        else
        {
            
            DOTween.To(()=>leftLetterbox.sizeDelta.x,amount=>
            {
                leftLetterbox.sizeDelta = new Vector2(amount, leftLetterbox.sizeDelta.y);
            },letterboxWidth,duration);

            DOTween.To(()=>rightLetterbox.sizeDelta.x,amount=>
            {
                rightLetterbox.sizeDelta = new Vector2(amount, leftLetterbox.sizeDelta.y);
            },letterboxWidth,duration);
        }
    }
}
