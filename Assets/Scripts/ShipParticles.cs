using UnityEngine;

public class ShipParticles : MonoBehaviour
{
    [SerializeField] private GameObject thrusterParticlesPreFab;
    [SerializeField] private GameObject deathParticleSystemPrefab;

    void Awake()
    {
        GetComponent<ShipEngines>().ThrustChanged += HandleThrustChanged;

        if (GetComponent<ShipHealth>() != null)
        {
            GetComponent<ShipHealth>().OnDie += HandleShipDeath;
        }
    }

    private void HandleThrustChanged(float thrust)
    {
        thrusterParticlesPreFab.SetActive(thrust > 0f);
    }

    private void HandleShipDeath()
    {
        Instantiate(deathParticleSystemPrefab, transform.position, Quaternion.identity);
    }
}
