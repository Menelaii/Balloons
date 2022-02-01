using System;

public class Player
{
    private int _health;
    private int _score;

    public event Action Died;
    public event Action<int> ScoreChanged;

    public Player(int health, int maxScore)
    {
        _health = health;
        MaxScore = maxScore;
    }

    public int MaxScore { get; private set; }

    public void OnBalloonPopedByPlayer(Balloon balloon, int reward)
    {
        balloon.PopedByPlayer -= OnBalloonPopedByPlayer;

        TakeReward(reward);
    }

    public void OnBalloonPopedByGround(Balloon balloon, int damage)
    {
        balloon.PopedByGround -= OnBalloonPopedByGround;

        TakeDamage(damage);
    }

    private void TakeReward(int reward)
    {
        _score += reward;
        ScoreChanged?.Invoke(_score);
    }

    private void TakeDamage(int damage)
    {
        _health -= damage;
        TryDie();
    }

    private void TryDie()
    {
        if(_health <= 0)
        {
            TryUpdateMaxScore();
            Died?.Invoke();
        }
    }

    private void TryUpdateMaxScore()
    {
        if (_score > MaxScore)
            MaxScore = _score;
    }
}
