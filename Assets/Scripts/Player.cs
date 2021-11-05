using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private int _health;
    [SerializeField] private MaxScore _maxScore;

    private int _score;

    public event Action Died;
    public event Action<int> ScoreChanged;

    public int MaxScore => _maxScore.Value;

    public void TakeDamage(int damage)
    {
        _health -= damage;
        TryDie();
    }

    public void TakeReward(int reward)
    {
        _score += reward;
        ScoreChanged?.Invoke(_score);
    }

    private void TryDie()
    {
        if(_health <= 0)
        {
            _maxScore.TryUpdateValue(_score);
            Died?.Invoke();
        }
    }
}
