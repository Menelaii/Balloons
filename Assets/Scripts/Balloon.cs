using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer))]
public class Balloon : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private ExplosionEffect _explosionEffect;

    private float _fallSpeed;
    private int _reward;
    private int _damage;
    private Player _player;

    private void FixedUpdate()
    {
        _rigidbody.velocity = Vector3.down * _fallSpeed * Time.fixedDeltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.TryGetComponent<Ground>(out Ground ground))
        {
            Pop();
            _player.TakeDamage(_damage);
        }
    }

    public void Init(Player player, float fallSpeed, int reward, int damage, Color color)
    {
        _player = player;
        _fallSpeed = fallSpeed;
        _reward = reward;
        _damage = damage;
        _renderer.color = color;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (Time.timeScale == 0)
            return;

        Pop();
        _player.TakeReward(_reward);
    }

    public void Pop()
    {
        var explosionEffect = Instantiate(_explosionEffect, transform.position, Quaternion.identity);
        explosionEffect.SetColor(_renderer.color);

        Destroy(gameObject);
    }
}
