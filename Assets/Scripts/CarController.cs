using UnityEngine;

public class CarController : MonoBehaviour
{
    [Header("Wheel Colliders")]
    public WheelCollider FL;
    public WheelCollider FR;
    public WheelCollider RL;
    public WheelCollider RR;

    [Header("Wheel Meshes")]
    public Transform FL_Mesh;
    public Transform FR_Mesh;
    public Transform RL_Mesh;
    public Transform RR_Mesh;

    [Header("Car Settings")]
    public float motorForce = 2000f;
    public float brakeForce = 3000f;
    public float maxSteerAngle = 30f;

    private float moveInput;
    private float steerInput;
    private float brakeInput;

    private void Update()
    {
        moveInput = Input.GetAxis("Vertical");
        steerInput = Input.GetAxis("Horizontal");
        brakeInput = Input.GetKey(KeyCode.Space) ? 1f : 0f;

        UpdateWheelVisuals();
    }

    private void FixedUpdate()
    {
        Move();
        Steer();
        Brake();
    }

    void Move()
    {
        RL.motorTorque = moveInput * motorForce;
        RR.motorTorque = moveInput * motorForce;
    }

    void Steer()
    {
        float steerAngle = steerInput * maxSteerAngle;
        FL.steerAngle = steerAngle;
        FR.steerAngle = steerAngle;
    }

    void Brake()
    {
        float brakeTorque = brakeInput * brakeForce;

        FL.brakeTorque = brakeTorque;
        FR.brakeTorque = brakeTorque;
        RL.brakeTorque = brakeTorque;
        RR.brakeTorque = brakeTorque;
    }

    void UpdateWheelVisuals()
    {
        UpdateWheel(FL, FL_Mesh);
        UpdateWheel(FR, FR_Mesh);
        UpdateWheel(RL, RL_Mesh);
        UpdateWheel(RR, RR_Mesh);
    }

    void UpdateWheel(WheelCollider col, Transform mesh)
    {
        Vector3 pos;
        Quaternion rot;
        col.GetWorldPose(out pos, out rot);

        mesh.position = pos;
        mesh.rotation = rot;
    }
}