using TMPro;
using UnityEngine;

public class ScoreBoard : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreUiText;
    int score;

    private void Start()
    {
        scoreUiText.text = 0.ToString();
    }

    public void SetScore(int amount)
    {
        score += amount;
        scoreUiText.text = score.ToString();
    }
}
