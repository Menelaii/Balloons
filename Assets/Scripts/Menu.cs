using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private GameOverMenu _gameOverMenu;
    [SerializeField] private Button _pauseButton;

    private void OnEnable()
    {
        _player.Died += _gameOverMenu.OnPlayerDied;
        _player.Died += OnPlayerDied;
    }

    private void OnDisable()
    {
        _player.Died -= _gameOverMenu.OnPlayerDied;
        _player.Died -= OnPlayerDied;
    }

    public void PauseOrUnpauseGame()
    {
        Time.timeScale = Time.timeScale == 1 ? 0 : 1;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }

    private void OnPlayerDied()
    {
        Time.timeScale = 0;
        _pauseButton.gameObject.SetActive(false);
    }
}