using UnityEngine;

public class VelocityComponent : MonoBehaviour
{
    [SerializeField] private CharacterController _characterController;
    public Vector3 Velocity = new Vector3();
    public Vector3 Direction {
        set { _direction = value; }
        get { return _direction; } 
    }

    [SerializeField] public float Speed { set; get; }
    [SerializeField] public float Acceleration { set; get; }

    Vector3 _hVelocity = new Vector3();
    Vector3 _vVelocity = new Vector3();
    private Vector3 _direction = new Vector3();

    public bool IsGrounded = false;

    bool isApplyingForce = false;

    public void SetDirection(Vector2 direction)
    {
        _direction = new Vector3(direction.x, 0, direction.y);
    }

    public void AccelerateTowardDirection()
    {
        _hVelocity = Vector3.Lerp(_hVelocity, _direction * Speed, Acceleration * Time.deltaTime);
    }

    public void Update()
    {
        AccelerateTowardDirection();

        if (IsGrounded && Velocity.y < 0 && !isApplyingForce)
        {
            StickToGround();
        }

        if (isApplyingForce)
        {
            isApplyingForce = false;
        }

        ApplyGravity();
        UpdateVelocity();
        _characterController.Move(Velocity * Time.deltaTime);

        IsGrounded = _characterController.isGrounded;
    }

    public void UpdateVelocity()
    {
        Velocity.x = _hVelocity.x + _vVelocity.x;
        Velocity.y = _vVelocity.y;
        Velocity.z = _hVelocity.z + _vVelocity.z;
    }

    public void RotateDirectionToward(Vector2 toward, Vector3 forward, Vector3 right)
    {
        var newDirection = new Vector3(toward.x, 0, toward.y);
        newDirection.Normalize();

        RotateDirectionToward(newDirection, forward, right);
    }

    public void RotateDirectionToward(Vector3 toward, Vector3 forward, Vector3 right)
    {
        _direction = toward.z * forward + toward.x * right;
    }

    public Quaternion GetDirectionRotation()
    {
        return Quaternion.LookRotation(_direction);
    }

    public void ApplyVerticalForce(float force, Vector3 direction)
    {
        isApplyingForce = true;
        _vVelocity = force * direction;
    }

    public void ApplyGravity()
    {
        _vVelocity -= Physics.gravity.y * Vector3.down * Time.deltaTime;
    }

    public void StickToGround()
    {
        _vVelocity.y = -2f;
    }

    public float GetHorizontalVelocityMagnitude()
    {
        return _hVelocity.magnitude;
    }

    public float GetVerticalVelocityMagnitude()
    {
        return _vVelocity.magnitude;
    }

    public void ResetHorizontalVelocity()
    {
        _hVelocity = Vector3.zero;
        _direction = Vector3.zero;
    }

    public void ResetVerticalVelocity()
    {
        _vVelocity = Vector3.zero;
    }

    public void ResetVelocity()
    {
        ResetHorizontalVelocity();
        ResetVerticalVelocity();
    }

    private void OnGUI()
    {
        GUILayout.BeginArea(new Rect(0, 80, 200, 200));
        GUILayout.TextArea("Is grounded: " + IsGrounded);
        GUILayout.TextArea("Is grounded (CC): " + _characterController.isGrounded);
        GUILayout.TextArea("Velocity (RAW): " + Velocity);
        GUILayout.TextArea("Velocity (CC): " + _characterController.velocity);
        GUILayout.EndArea();
    }
}
