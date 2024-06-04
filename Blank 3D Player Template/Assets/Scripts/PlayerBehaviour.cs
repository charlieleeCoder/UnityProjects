using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    // Can be affected by enemies? Status effects?
    public float moveSpeed = 10f;
    public float rotateSpeed = 75f;
    public float jumpVelocity = 5f;
    public float bulletSpeed = 100f;

    // Movement
    private float _verticalInput;
    private float _horizontalInput;
    private Rigidbody _rigidBody;
    private CapsuleCollider _capsuleCollider;

    // Jumping
    public float distanceToGroundBuffer = 0.1f;
    public LayerMask groundLayer;

    private bool _isJumping;

    // Shooting
    public GameObject bullet;
    
    private bool _isShooting;

    // Start is called before the first frame update
    void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();
        _capsuleCollider = GetComponent<CapsuleCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        // Movement
        _verticalInput = Input.GetAxis("Vertical") * moveSpeed;
        _horizontalInput = Input.GetAxis("Horizontal") * rotateSpeed;
        _isJumping |= Input.GetKeyDown(KeyCode.Space);

        // Shooting
        _isShooting |= Input.GetMouseButtonDown(0);

    }

    // Frame-rate independent update call
    private void FixedUpdate()
    {
        // Jump logic
        if(IsGrounded() && _isJumping)
        {
            _rigidBody.AddForce(Vector3.up * jumpVelocity, ForceMode.Impulse);
        }

        _isJumping = false;

        // Shooting logic
        if (_isShooting)
        {
            GameObject newBullet = Instantiate(bullet, this.transform.position + new Vector3(1,0,0),
                this.transform.rotation);

            Rigidbody bulletRigidBody = newBullet.GetComponent<Rigidbody>();

            bulletRigidBody.velocity = this.transform.forward * bulletSpeed;
        }

        _isShooting = false;
        
        // Left and right rotation
        Vector3 rotation = Vector3.up * _horizontalInput;

        // Turning 
        Quaternion angleRotation = Quaternion.Euler(rotation * Time.fixedDeltaTime);

        // Move forwards and backwards
        _rigidBody.MovePosition(this.transform.position + // Current pos
                                this.transform.forward * _verticalInput * Time.fixedDeltaTime); // Degree of transformation

        // Rotate
        _rigidBody.MoveRotation(_rigidBody.rotation * angleRotation);

    }

    // Check if grounded
    private bool IsGrounded()
    {
        Vector3 capsuleBottom = new Vector3(_capsuleCollider.bounds.center.x,
            _capsuleCollider.bounds.min.y, _capsuleCollider.bounds.center.z);

        bool grounded = Physics.CheckCapsule(_capsuleCollider.bounds.center, capsuleBottom, 
            distanceToGroundBuffer, groundLayer, QueryTriggerInteraction.Ignore);

        return grounded;
    }
}
