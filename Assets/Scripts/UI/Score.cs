using UnityEngine;
using TMPro;


public class Score : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI ScoreText;

    private int KilledGnomesCount = 0;
    private int SavedGnomesCount = 0;

    private void Start()
    {
        GlobalManager globalManager = GlobalManager.Instance;

        globalManager.PlayerKilled += () => { KilledGnomesCount = 0; SavedGnomesCount = 0; UpdateScoreUI(); };
        globalManager.TrpaslikKilled += () => { KilledGnomesCount++; UpdateScoreUI(); };
        globalManager.TrpaslikKilled += () => { SavedGnomesCount++; UpdateScoreUI(); };
        globalManager.OnTogglePerspective += (is3D) => { UpdateScoreUI(); };
    }

    private void UpdateScoreUI()
    {
        if (GlobalManager.Instance.is3D)
        {
            ScoreText.text = $"SCORE: {SavedGnomesCount}/{KilledGnomesCount}";
        }
        else
        {
            ScoreText.text = $"SCORE: {KilledGnomesCount}";
        }
    }

}
