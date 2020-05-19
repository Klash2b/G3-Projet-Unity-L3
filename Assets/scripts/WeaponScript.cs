using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Launch projectile
/// </summary>
public class WeaponScript : MonoBehaviour
{
    //--------------------------------
    // 1 - Designer variables
    //--------------------------------

    public float speed = 55f;

    /// <summary>
    /// Projectile prefab for shooting
    /// </summary>
    public Transform shotPrefab;

    /// <summary>
    /// Cooldown in seconds between two shots
    /// </summary>
    public float shootingRate = 3.5f;

    //--------------------------------
    // 2 - Cooldown
    //--------------------------------

    public float shootCooldown;

    void Start()
    {
        shootCooldown = 3.5f;
    }

    void Update()
    {
        if (shootCooldown > 0)
        {
            shootCooldown -= Time.deltaTime;
        }
    }

    //--------------------------------
    // 3 - Shooting from another script
    //--------------------------------

    /// <summary>
    /// Create a new projectile if possible
    /// </summary>
    public void Attack(Vector3 direction)
    {
        if (CanAttack)
        {
            float angle;
            shootCooldown = shootingRate;

            // Create a new shot
            var shotTransform = Instantiate(shotPrefab) as Transform;

            // Assign position
            shotTransform.position = transform.position;

            shotTransform.GetComponent<Rigidbody2D>().velocity = direction.normalized * speed;
            angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            shotTransform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            // Make the weapon shot always towards it
        }
    }

    /// <summary>
    /// Is the weapon ready to create a new projectile?
    /// </summary>
    public bool CanAttack
    {
        get
        {
            return shootCooldown <= 0f;
        }
    }
}