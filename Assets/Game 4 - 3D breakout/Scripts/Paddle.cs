using UnityEngine;
using System.Collections;

public enum paddleType { left,right };


public class Paddle : MonoBehaviour {
    [SerializeField]
    public paddleType thisPaddleType;
    public float moveSpeed = 15;
		
	void Update () {
        float moveInput = 0.0f;
        switch (thisPaddleType)
        {
            case paddleType.left:
                moveInput = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
                break;
            case paddleType.right:
                moveInput = Input.GetAxis("Horizontal2") * Time.deltaTime * moveSpeed;
                break;
            default:
                break;
        }

        transform.position += new Vector3(moveInput, 0, 0);

        float max = 14.0f;
        if (transform.position.x <= -max || transform.position.x >= max)
        {
            float xPos = Mathf.Clamp(transform.position.x, -max, max); //Clamp between min -5 and max 5
            transform.position = new Vector3(xPos, transform.position.y, transform.position.z);
        }
	}

    void OnCollisionExit(Collision collisionInfo ) {
        //Add X velocity..otherwise the ball would only go up&down
        Rigidbody rigid = collisionInfo.rigidbody;
        float xDistance = rigid.position.x - transform.position.x;
        rigid.velocity = new Vector3(rigid.velocity.x + xDistance/2, rigid.velocity.y, rigid.velocity.z);
    }
}
