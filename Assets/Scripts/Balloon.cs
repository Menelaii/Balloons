using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class Balloon : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private ExplosionEffect _explosionEffect;

    private float _fallSpeed;
    private int _reward;
    private int _damage;

    public event Action<Balloon, int> PopedByPlayer;
    public event Action<Balloon, int> PopedByGround;

    private void Start()
    {
        _rigidbody.velocity = Vector3.down * _fallSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.TryGetComponent<Ground>(out Ground ground))
        {
            Pop();
            PopedByGround?.Invoke(this, _damage);
        }
    }

    public void Init(float fallSpeed, int reward, int damage, Color color)
    {
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
        PopedByPlayer?.Invoke(this, _reward);
    }

    public void Pop()
    {
        var explosionEffect = Instantiate(_explosionEffect, transform.position, Quaternion.identity);
        explosionEffect.SetColor(_renderer.color);

        Destroy(gameObject);
    }
}
