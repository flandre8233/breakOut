using UnityEngine;
using System.Collections;

public enum ballType { left, right };
public class Ball : MonoBehaviour {

    public float maxVelocity = 20;
    public float minVelocity = 15;
    Transform leftkillZone;
    public ballType thisball_type;


    void Awake () {
        leftkillZone = GameObject.FindGameObjectsWithTag("leftkillzone")[0].transform;
        //GetComponent<Rigidbody>().velocity = new Vector3(0, 0, -18);

        switch (thisball_type)
        {
            case ballType.left:
                GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 18);
                break;
            case ballType.right:


                break;
            default:
                break;
        }
    }

    void Update()
    {


        //Make sure we stay between the MAX and MIN speed.
        float totalVelocity = Vector3.Magnitude(GetComponent<Rigidbody>().velocity);
        if (totalVelocity > maxVelocity)
        {
            float tooHard = totalVelocity / maxVelocity;
            GetComponent<Rigidbody>().velocity /= tooHard;
        }
        else if (totalVelocity < minVelocity)
        {
            float tooSlowRate = totalVelocity / minVelocity;
            GetComponent<Rigidbody>().velocity /= tooSlowRate;
        }

        switch (thisball_type)
        {
            case ballType.left:
                gameObject.GetComponent<Renderer>().material.color = Color.red;
                break;
            case ballType.right:
                gameObject.GetComponent<Renderer>().material.color = Color.yellow;


                break;
            default:
                break;
        }
        //Is the ball below -3? Then we're game over.
        if (transform.position.z <= leftkillZone.position.z)
        {
            BreakoutGame.SP.LostBall();
            Destroy(gameObject);
        }
    }
}
