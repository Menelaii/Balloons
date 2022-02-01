using UnityEngine;
using UnityEngine.UI;

public class ScoreView : MonoBehaviour
{
    [SerializeField] private Text _text;

    private Player _player;

    public void Init(Player player)
    {
        _player = player;
    }

    public void OnScoreChanged(int score)
    {
        _text.text = $"Score: {score}";
    }

    public void OnPlayerDied()
    {
        _text.text += $"\nMax Score: {_player.MaxScore}";
    }
}
