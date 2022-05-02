using UnityEngine;

public static class AttackLibrary{

    #region Projectile Attacks
    public static ProjectileAttackTemplate OneForward() => new ProjectileAttackTemplate(
        projectilePrefab: _defaultProjectilePrefab,
        numberOfProjectiles: 1,
        projectileSpeed: 9f,
        spawnRadius:1f,
        spawnDegreesIn360: 360
    );
    
    #endregion
    private static GameObject _defaultProjectilePrefab;
    private static GameObject _homingProjectilePrefab;
    private static GameObject _arcProjectilePrefab;

    public static void Initialize(GameObject defaultProjectilePrefab, GameObject homingProjectilePrefab, GameObject arcProjectilePrefab){
        _defaultProjectilePrefab = defaultProjectilePrefab;
        _homingProjectilePrefab = homingProjectilePrefab;
        _arcProjectilePrefab = arcProjectilePrefab;
    }
}