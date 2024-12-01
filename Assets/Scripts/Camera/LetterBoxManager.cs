using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LetterBoxManager : MonoBehaviour
{
    public RectTransform leftLetterbox;
    public RectTransform rightLetterbox;
    public RectTransform dialogWindow;
    public RectTransform score;
    public float duration = 1f;
    public float letterboxWidth = 160;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GlobalManager.Instance.OnTogglePerspective += TogglePerspective;
        GlobalManager.Instance.GameWon += EndScreen;
    }

    private void TogglePerspective(bool is3D)
    {
        if (is3D)
        {
            DOTween.To(() => leftLetterbox.sizeDelta.x, amount =>
            {
                leftLetterbox.sizeDelta = new Vector2(amount, leftLetterbox.sizeDelta.y);
            }, 0.0f, duration);

            DOTween.To(() => rightLetterbox.sizeDelta.x, amount =>
            {
                rightLetterbox.sizeDelta = new Vector2(amount, leftLetterbox.sizeDelta.y);
            }, 0.0f, duration);

            DOTween.To(() => dialogWindow.sizeDelta.x, amount =>
            {
                dialogWindow.sizeDelta = new Vector2(amount, dialogWindow.sizeDelta.y);
            }, 1220, duration);

            DOTween.To(() => score.position.x, amount =>
            {
                score.position = new Vector2(amount, score.position.y);
            }, 10, duration);
        }
        else
        {
            DOTween.To(() => leftLetterbox.sizeDelta.x, amount =>
            {
                leftLetterbox.sizeDelta = new Vector2(amount, leftLetterbox.sizeDelta.y);
            }, letterboxWidth, duration);

            DOTween.To(() => rightLetterbox.sizeDelta.x, amount =>
            {
                rightLetterbox.sizeDelta = new Vector2(amount, leftLetterbox.sizeDelta.y);
            }, letterboxWidth, duration);

            DOTween.To(() => dialogWindow.sizeDelta.x, amount =>
            {
                dialogWindow.sizeDelta = new Vector2(amount, dialogWindow.sizeDelta.y);
            }, 950, duration);

            DOTween.To(() => score.position.x, amount =>
            {
                score.position = new Vector2(amount, score.position.y);
            }, 250, duration);
        }
    }

    private void EndScreen()
    {
        StartCoroutine(EndCurtains());
    }

    public IEnumerator EndCurtains()
    {
        yield return new WaitForSeconds(3f);
        
        DOTween.To(() => leftLetterbox.sizeDelta.x, amount =>
        {
            leftLetterbox.sizeDelta = new Vector2(amount, leftLetterbox.sizeDelta.y);
        }, 640, 2f);

        DOTween.To(() => rightLetterbox.sizeDelta.x, amount =>
        {
            rightLetterbox.sizeDelta = new Vector2(amount, leftLetterbox.sizeDelta.y);
        }, 640, 2f);
        
        
        yield return new WaitForSeconds(2.5f);
        SceneManager.LoadScene("Credits");
    }
}
