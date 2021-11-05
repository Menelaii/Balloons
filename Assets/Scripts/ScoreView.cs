using UnityEngine;
using UnityEngine.UI;

public class ScoreView : MonoBehaviour
{
    [SerializeField] private Text _text;
    [SerializeField] private Player _player;

    private void OnEnable()
    {
        _player.ScoreChanged += OnScoreChanged;
        _player.Died += OnPlayerDied;
    }

    private void OnDisable()
    {
        _player.ScoreChanged -= OnScoreChanged;
        _player.Died -= OnPlayerDied;
    }

    private void OnScoreChanged(int score)
    {
        _text.text = $"Score: {score}";
    }

    private void OnPlayerDied()
    {
        _text.text += $"\nMax Score: {_player.MaxScore}";
    }
}
