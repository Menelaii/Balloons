using System.Collections;
using UnityEngine;

public class BalloonsSpawner : MonoBehaviour
{
    [SerializeField] private Balloon _template;
    [SerializeField] private float _spawnDelay;
    [SerializeField] private int _maxReward;
    [SerializeField] private int _maxDamage;
    [SerializeField] private float _maxFallSpeed;
    [SerializeField] private float _minFallSpeed;
    [SerializeField] private int _balloonsToIncreaseFallSpeed;
    [SerializeField] private float _borderIndent;
    [Range(0, 100)] [SerializeField] private int _increasePercent;

    private int _spawnCount;
    private float _currentMaxFallSpeed;
    private Vector2 _leftScreenBorder;
    private Vector2 _rightScreenBorder;
    private Player _player;

    public void Init(Player player)
    {
        _player = player;

        _currentMaxFallSpeed = _minFallSpeed;

        Camera camera = Camera.main;
        _leftScreenBorder = camera.ViewportToWorldPoint(new Vector3(0, 1, camera.nearClipPlane));
        _rightScreenBorder = camera.ViewportToWorldPoint(new Vector3(1, 1, camera.nearClipPlane));
    }

    public IEnumerator SpawnWithDelay()
    {
        var waitForDelay = new WaitForSeconds(_spawnDelay);
        while (true)
        {
            yield return waitForDelay;
            Spawn();
        }
    }

    private void Spawn()
    {
        var balloon = Instantiate(_template, GetNextSpawnPosition(), Quaternion.identity);

        var fallSpeed = Random.Range(_minFallSpeed, _currentMaxFallSpeed);
        var reward = Random.Range(0, _maxReward);
        var damage = Random.Range(0, _maxDamage);
        var color = Random.ColorHSV();
        balloon.Init(fallSpeed, reward, damage, color);

        balloon.PopedByGround += _player.OnBalloonPopedByGround;
        balloon.PopedByPlayer += _player.OnBalloonPopedByPlayer;

        _spawnCount++;
        TryIncreaseCurrentMaxFallSpeed();
    }

    private void TryIncreaseCurrentMaxFallSpeed()
    {
        if(_spawnCount >= _balloonsToIncreaseFallSpeed)
        {
            _currentMaxFallSpeed = Mathf.Clamp(_currentMaxFallSpeed +  _currentMaxFallSpeed * _increasePercent / 100f, _minFallSpeed, _maxFallSpeed);
            _spawnCount = 0;
        }
    }

    private Vector2 GetNextSpawnPosition()
    {
        Vector2 spawnPosition;
        spawnPosition.x = Random.Range(_leftScreenBorder.x + _borderIndent, _rightScreenBorder.x - _borderIndent);
        spawnPosition.y = _leftScreenBorder.y;

        return spawnPosition;
    }
}
