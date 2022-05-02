using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct ProjectileAttackTemplate{
    public GameObject Prefab => _projectilePrefab;
    public int Number => _numberOfProjectiles;
    public float Speed => _projectileSpeed;
    public float Radius => _spawnRadius;
    public float spawnDegreesIn360 => _spawnDegreesIn360;

    private GameObject _projectilePrefab;
    private int _numberOfProjectiles;
    private float _projectileSpeed;
    private float _spawnRadius;
    private float _spawnDegreesIn360;

    public ProjectileAttackTemplate(GameObject projectilePrefab,int numberOfProjectiles = 1, float projectileSpeed = 15f, float spawnRadius = 0.2f, float spawnDegreesIn360 = 360f){
        _projectilePrefab = projectilePrefab;
        _numberOfProjectiles = numberOfProjectiles;
        _projectileSpeed = projectileSpeed;
        _spawnRadius = spawnRadius;
        _spawnDegreesIn360 = spawnDegreesIn360;
    }
}
