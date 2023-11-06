using UnityEngine;

public class MovementBehaviour : MonoBehaviour
{
    private float velocity;
    private Vector3 direction;
    private int rotationSpeed;
    private Rigidbody RB3D;
    private Rigidbody2D RB2D;

    private void Awake()
    {
        if (TryGetComponent(out Rigidbody rigidbody3d))
        {
            RB3D = rigidbody3d;
        }
        else if (TryGetComponent(out Rigidbody2D rigidbody2d))
        {
            RB2D = rigidbody2d;
        }
    }

    public void Init(float Vel, Vector3 Dir, int RotateSpeed)
    {
        velocity = Vel;
        direction = Dir;
        rotationSpeed = RotateSpeed;
    }

    public void Init(float Vel)
    {
        velocity = Vel;
    }

    public void Init(Vector3 Dir)
    {
        direction = Dir;
    }

    public void Init(int RotatSpeed)
    {
        rotationSpeed = RotatSpeed;
    }
    public void Move()
    {
        transform.position = transform.position + Time.deltaTime * velocity * direction;
    }

    public void Move(Vector3 dir)
    {
        transform.position = transform.position + Time.deltaTime * velocity * dir;
    }

    public void Move(float Vel)
    {
        transform.position = transform.position + Time.deltaTime * Vel * direction;
    }

    public void Move(float Vel, Vector3 Dir)
    {
        transform.position = transform.position + Time.deltaTime * Vel * Dir;
    }

    public void MoveRB()
    {
        RB2D.MovePosition(transform.position + Time.fixedDeltaTime * velocity * direction);
    }
    public void MoveRB(float Vel)
    {
        RB2D.MovePosition(transform.position + Time.fixedDeltaTime * Vel * direction);
    }
    public void MoveRB(Vector3 dir)
    {
        RB2D.MovePosition(transform.position + Time.fixedDeltaTime * velocity * dir);
    }

    public void MoveRB(float Vel, Vector3 dir)
    {
        RB2D.MovePosition(transform.position + Time.fixedDeltaTime * Vel * dir);
    }

    public void MoveRB3D()
    {
        RB3D.MovePosition(transform.position + Time.fixedDeltaTime * velocity * direction);
    }

    public void MoveRB3D(float vel)
    {
        RB3D.MovePosition(transform.position + Time.fixedDeltaTime * vel * direction);
    }

    public void MoveRB3D(Vector3 Dir)
    {
        RB3D.MovePosition(transform.position + Time.fixedDeltaTime * velocity * Dir);
    }

    public void MoveRB3D(float vel, Vector3 Dir)
    {
        RB3D.MovePosition(transform.position + Time.fixedDeltaTime * vel * Dir);
    }

    public void MoveVelocity(Vector3 Dir)
    {
        RB3D.velocity = Dir * velocity;
    }

    public void RotateRight()
    {
        transform.Rotate(new Vector3(0, rotationSpeed * Time.deltaTime, 0));
    }

    public void RotateLeft()
    {
        transform.Rotate(new Vector3(0, -rotationSpeed * Time.deltaTime, 0));
    }

    public void ChangeVelocity(float amount)
    {
        velocity = amount;
    }

    public void ChangeDirection(Vector3 newDir)
    {
        direction = newDir;
    }

    public float getVelocity()
    {
        return velocity;
    }

    public Vector3 getDirection()
    {
        return direction;
    }
}