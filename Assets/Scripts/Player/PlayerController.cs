using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
[RequireComponent(typeof(ConfigurableJoint))]
public class PlayerController : MonoBehaviour
{

    [SerializeField]
    private float speed = 5f;
    [SerializeField]
    private float lookSensitivity = 3f;
    private PlayerMotor motor;
    private ConfigurableJoint joint;
    [Header("Spring settings")]
    [SerializeField]
    private float jointSpring = 20;
    [SerializeField]
    private float jointMaxForce = 40;
    [SerializeField]
    private float thrusterForce = 1000f;
    void Start()
    {
        motor = GetComponent<PlayerMotor>();
        joint = GetComponent<ConfigurableJoint>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        ConfigureJoint(jointSpring);
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit _hit;
		if (Physics.Raycast (transform.position, Vector3.down, out _hit, 100f))
		{
			joint.targetPosition = new Vector3(0f, -_hit.point.y, 0f);
		} else
		{
			joint.targetPosition = new Vector3(0f, 0f, 0f);
		}

        float _xMov = Input.GetAxis("Horizontal");
        float _zMov = Input.GetAxis("Vertical");

        Vector3 _movHorizontal = transform.right * _xMov;
        Vector3 _movVertical = transform.forward * _zMov;

        Vector3 _velocity = (_movHorizontal + _movVertical) * speed;

        motor.Move(_velocity);

        //Calculate rotation as a 3D vector (turning around)
        float _yRot = Input.GetAxisRaw("Mouse X");

        Vector3 _rotation = new Vector3(0f, _yRot, 0f) * lookSensitivity;

        //Apply rotation
        motor.Rotate(_rotation);

        //Calculate camera rotation as a 3D vector (turning around)
        float _xRot = Input.GetAxisRaw("Mouse Y");

        float _cameraRotationX = _xRot * lookSensitivity;

        //Apply camera rotation
        motor.RotateCamera(_cameraRotationX);

        Vector3 _thrusterForce  = Vector3.zero;
        // Apply thruster force
        if(Input.GetButton("Jump")) {
            _thrusterForce = Vector3.up * thrusterForce;
            ConfigureJoint(0f);
        }
        else
        {
            ConfigureJoint(jointSpring);
        }

        motor.ApplyThruster(_thrusterForce);
    }

    private void ConfigureJoint(float _jointSpring) {
        joint.yDrive = new JointDrive { positionSpring = _jointSpring, maximumForce = jointMaxForce};

    }

}
