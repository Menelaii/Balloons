using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer))]
public class Balloon : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private ExplosionEffect _explosion;

    private float _fallSpeed;
    private int _reward;
    private int _damage;
    private Player _player;

    private void FixedUpdate()
    {
        _rigidbody.velocity = Vector2.zero;

        float gravityForceSpeed = Physics2D.gravity.y * Time.fixedDeltaTime;
        float additionalSpeed = gravityForceSpeed - _fallSpeed;
        float additionalForceMagnitude = _rigidbody.mass * additionalSpeed / Time.fixedDeltaTime;
        _rigidbody.AddForce(Vector3.up * additionalForceMagnitude);
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
        var explosion = Instantiate(_explosion, transform.position, Quaternion.identity);
        explosion.Init(_renderer.color);

        Destroy(gameObject);
    }
}
