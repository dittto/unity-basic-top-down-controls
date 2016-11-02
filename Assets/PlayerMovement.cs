using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class PlayerMovement : MonoBehaviour
{
    public float speedMultiplier = 0.1f;
    public float jumpForce = 5f;
    private Rigidbody rigidBody;
    private float groundOffset;
    private bool canJump = false;

    public void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        groundOffset = GetComponent<Collider>().bounds.extents.y;
    }

	public void Update()
    {
        if (Input.GetButtonDown("Jump") && IsOnGround()) {
            canJump = true;
        }

        ChangeParentGameObjectBasedOnGround();
    }

    public void FixedUpdate()
    {
        Vector3 newPos = transform.position + (new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * speedMultiplier);
        rigidBody.MovePosition(newPos);
        
        if (canJump) {
            rigidBody.AddForce(0, jumpForce, 0, ForceMode.Impulse);
            canJump = false;
        }
    }
    
    private bool IsOnGround()
    {
        return GetGroundGameObject() != null;
    }

    private GameObject GetGroundGameObject()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, groundOffset + 0.2f)) {
            return hit.collider.gameObject;
        }

        return null;
    }

    private void ChangeParentGameObjectBasedOnGround()
    {
        GameObject ground = GetGroundGameObject();
        if (ground == null) {
            return;
        }
        
        if (transform.parent == null || transform.parent.gameObject != ground) {
            transform.parent = ground.transform;
        }
    }
}
