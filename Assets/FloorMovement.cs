using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class FloorMovement : MonoBehaviour {

    private float startPos;
    public float endPos = 10f;
    public float speedModifier = 0.1f;
    private float t = 0f;
    private Rigidbody rigidBody;

	public void Start ()
    {
        startPos = transform.position.x;
        rigidBody = GetComponent<Rigidbody>();
	}

    public void FixedUpdate()
    {
        if (t <= 1) {
            MoveFloor();
        }
        else {
            SwapMovementValues();
            t = 0f;
        }
	}

    private void MoveFloor()
    {
        Vector3 pos = transform.position;
        pos.x = Mathf.Lerp(startPos, endPos, t);
        rigidBody.MovePosition(pos);

        t += speedModifier * Time.fixedDeltaTime;
    }

    private void SwapMovementValues()
    {
        float temp = startPos;
        startPos = endPos;
        endPos = temp;
    }
}
