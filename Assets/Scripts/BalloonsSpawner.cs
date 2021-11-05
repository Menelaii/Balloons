using System.Collections;
using UnityEngine;

public class BalloonsSpawner : MonoBehaviour
{
    [SerializeField] private Balloon _template;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private Player _player;
    [SerializeField] private float _spawnDelay;
    [SerializeField] private float _spawningHorizontalSpread;
    [SerializeField] private int _maxReward;
    [SerializeField] private int _maxDamage;
    [SerializeField] private float _maxFallSpeed;
    [SerializeField] private float _minFallSpeed;
    [SerializeField] private int _balloonsToIncreaseMinSpeed;
    [Range(0,100)] [SerializeField] private int _increasePercent;

    private int _spawnCount;

    private void Start()
    {
        StartCoroutine(SpawnWith(_spawnDelay));
    }

    private void Spawn()
    {
        Vector3 spawnPosition = _spawnPoint.position;
        spawnPosition.x += Random.Range(-_spawningHorizontalSpread, _spawningHorizontalSpread);

        var balloon = Instantiate(_template, spawnPosition, Quaternion.identity);

        balloon.Init(_player, Random.Range(_minFallSpeed, _maxFallSpeed), Random.Range(0, _maxReward), Random.Range(0, _maxDamage), Random.ColorHSV());

        _spawnCount++;
        TryIncreaseMinFallSpeed();
    }

    private IEnumerator SpawnWith(float delay)
    {
        var waitForDelay = new WaitForSeconds(delay);
        while (true)
        {
            yield return waitForDelay;
            Spawn();
        }
    }

    private void TryIncreaseMinFallSpeed()
    {
        if(_spawnCount >= _balloonsToIncreaseMinSpeed)
        {
            _minFallSpeed = Mathf.Clamp(_minFallSpeed +  _minFallSpeed * _increasePercent / 100f, _minFallSpeed, _maxFallSpeed);
            _spawnCount = 0;
        }
    }
}
