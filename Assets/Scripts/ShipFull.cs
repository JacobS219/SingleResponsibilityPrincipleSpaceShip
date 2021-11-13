using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using UnityEngine;

public class ShipFull : MonoBehaviour
{
    [SerializeField] private float speed = 8f;
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private float turnSpeed = 150f;
    [SerializeField] private float fireForce = 600f;
    [SerializeField] private Transform weaponMountPoint;
    [SerializeField] private Rigidbody projectilePreFab;
    [SerializeField] private GameObject thrusterParticles;
    [SerializeField] private GameObject deathParticleSystemPrefab;

    private int _health;

    private void Awake()
    {
        _health = maxHealth;
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            FireWeapon();
        }

        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");
        transform.position += Time.deltaTime * vertical * transform.forward * speed;
        transform.Rotate(Vector3.up * horizontal * turnSpeed * Time.deltaTime);

        thrusterParticles.SetActive(vertical > 0);
    }

    private void FireWeapon()
    {
        var spawnedProjectile = Instantiate(projectilePreFab, weaponMountPoint.position, weaponMountPoint.rotation);
        spawnedProjectile.AddForce(spawnedProjectile.transform.forward * fireForce);
    }

    private void TakeDamage(int damage)
    {
        _health -= damage;
        if (_health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Instantiate(deathParticleSystemPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        var projectile = collision.collider.GetComponent<Projectile>();
        if (this.projectilePreFab != null)
        {
            TakeDamage(projectile.Damage);
        }
    }
}
