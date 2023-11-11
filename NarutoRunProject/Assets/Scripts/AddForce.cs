using UnityEngine;

public class AddForce : MonoBehaviour
{
    [SerializeField] private float force = -15f;
    private Rigidbody RB;
    private void Start()
    {
        RB = GetComponent<Rigidbody>();
    }

    void Update()
    {
        RB.AddForce(Vector3.back * force);
    }
}
