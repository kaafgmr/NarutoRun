using UnityEngine;

public class AddForce : MonoBehaviour
{
    private Rigidbody RB;
    private void Start()
    {
        RB = GetComponent<Rigidbody>();
    }

    void Update()
    {
        RB.AddForce(Vector3.back * -15);
    }
}
