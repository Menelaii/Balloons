using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class ExplosionEffect : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particleSystem;

    public void Init(Color color)
    {
        var main = _particleSystem.main;
        main.startColor = color;
    }
}
