using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    private const string _maxScoreKey = "maxScore";
    private const int _defaultMaxScore = 0;

    [SerializeField] private int _playerHealth;
    [SerializeField] private BalloonsSpawner _spawner;
    [SerializeField] private Button _pauseButton;
    [SerializeField] private Button _restartButton;
    [SerializeField] private GameOverPanel _gameOverPanel;
    [SerializeField] private ScoreView _scoreView;

    private Player _player;

    private void Awake()
    {
        _player = new Player(_playerHealth, LoadMaxScore());
        _spawner.Init(_player);
        _scoreView.Init(_player);
    }

    private void OnEnable()
    {
        _player.Died += OnPlayerDied;
        _player.ScoreChanged += _scoreView.OnScoreChanged;
        _player.Died += _scoreView.OnPlayerDied;

        _pauseButton.onClick.AddListener(OnPauseButtonClick);
        _restartButton.onClick.AddListener(OnRestartButtonClick);
    }

    private void OnDisable()
    {
        _player.Died -= OnPlayerDied;
        _player.ScoreChanged -= _scoreView.OnScoreChanged;
        _player.Died -= _scoreView.OnPlayerDied;
        SaveMaxScore();

        _pauseButton.onClick.RemoveListener(OnPauseButtonClick);
        _restartButton.onClick.RemoveListener(OnRestartButtonClick);
    }

    private void Start()
    {
        StartCoroutine(_spawner.SpawnWithDelay());
    }

    private void OnPauseButtonClick()
    {
        Time.timeScale = Time.timeScale == 1 ? 0 : 1;
    }

    private void OnRestartButtonClick()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }

    private int LoadMaxScore()
    {
        return PlayerPrefs.GetInt(_maxScoreKey, _defaultMaxScore);
    }

    private void SaveMaxScore()
    {
        PlayerPrefs.SetInt(_maxScoreKey, _player.MaxScore);
    }

    private void OnPlayerDied()
    {
        Time.timeScale = 0;
        _pauseButton.gameObject.SetActive(false);
        _gameOverPanel.gameObject.SetActive(true);
    }
}
