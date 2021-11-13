using System;
using UnityEngine;

[RequireComponent(typeof(ShipInput))]
public class ShipEngines : MonoBehaviour
{
    [SerializeField] private float speed = 8f;
    [SerializeField] private float turnSpeed = 150f;

    private ShipInput shipInput;
    private float lastThrust = float.MinValue;

    public event Action<float> ThrustChanged = delegate { };

    private void Awake()
    {
        shipInput = GetComponent<ShipInput>();
    }

    void Update()
    {
        if (lastThrust != shipInput.Vertical)
        {
            ThrustChanged(shipInput.Vertical);
        }

        lastThrust = shipInput.Vertical;

        transform.position += Time.deltaTime * shipInput.Vertical * transform.forward * speed;
        transform.Rotate(Vector3.up * shipInput.Horizontal * turnSpeed * Time.deltaTime);
    }
}
