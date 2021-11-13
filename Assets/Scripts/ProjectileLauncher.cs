using UnityEngine;

public class ProjectileLauncher : MonoBehaviour
{
    [SerializeField] private Rigidbody projectilePreFab;
    [SerializeField] private Transform weaponMountPoint;
    [SerializeField] private float fireForce = 300f;

    private void Awake()
    {
        GetComponent<ShipInput>().OnFire += HandleFire;
    }

    private void HandleFire()
    {
        var spawnedProjectile = Instantiate(projectilePreFab, weaponMountPoint.position, weaponMountPoint.rotation);
        spawnedProjectile.AddForce(spawnedProjectile.transform.forward * fireForce);
    }
}
