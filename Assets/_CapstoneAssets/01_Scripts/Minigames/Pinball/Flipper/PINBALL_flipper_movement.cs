using UnityEngine;

public class PINBALL_flipper_movement : MonoBehaviour
{
    /*[Tooltip("Added forward thrust to flip. Positive force if going right, negative force if going left.")]
    public float flipperForwardThrust;

    [Tooltip("Added upward to flip.")]
    public float flipperUpwardThrust;

    bool applyThrust;

    public Rigidbody2D pinballRB;

    private void Start()
    {
        joint = GetComponent<HingeJoint2D>();
        jointMotor = joint.motor;
    }

    void Update()
    {
        applyThrust = false;

        applyThrust |= Input.GetKeyDown(myFlipperKey);

        if (Input.GetKey(myFlipperKey))
        {
            jointMotor.motorSpeed = flipperMotorSpeed; //Mathf.Abs(jointMotor.motorSpeed);
            joint.motor = jointMotor;
        }
        else
        {
            jointMotor.motorSpeed = flipperMotorSpeed * -1; //Mathf.Abs(jointMotor.motorSpeed) * -1f;
            joint.motor = jointMotor;
        }
    }

    // Uses a trigger volume on the tip of the flipper instead of the main collider.
    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag.Trim().Equals("Pinball".Trim()))
        {
            if (pinballRB == null)
            {
                pinballRB = col.gameObject.GetComponent<Rigidbody2D>();
            }
            if (applyThrust)
            {
                Debug.Log("Applied thrust to pinball.");
                pinballRB.AddForce(transform.right * flipperForwardThrust, ForceMode2D.Impulse);
                pinballRB.AddForce(transform.up * flipperUpwardThrust, ForceMode2D.Impulse);
            }
        }
    }*/

    [Tooltip("Different hot spot trigger volumes on the flipper that will apply force onto the pinball in different directions.")]
    public PINBALL_flipper_hotspot[] flipperHotspots;

    public Rigidbody2D pinballRB;

    public KeyCode myFlipperKey;

    bool flipperInput;

    private void Update()
    {
        flipperInput = false;
        if (Input.GetKeyDown(myFlipperKey))
        {
            flipperInput = true;
        }
    }

    private void FixedUpdate()
    {
        if (flipperInput)
        {
            for (int i = 0; i < flipperHotspots.Length; i++)
            {
                if (flipperHotspots[i].pinballInHotspot)
                {
                    if (rightFlipper)
                    {
                        pinballRB.AddForce(transform.right * flipperHotspots[i].flipperForwardThrust, ForceMode2D.Impulse);
                    }
                    else // Swap directions depending on which flipper side this is on.
                    {
                        pinballRB.AddForce(transform.right * -1f * flipperHotspots[i].flipperForwardThrust, ForceMode2D.Impulse);
                    }
                    pinballRB.AddForce(transform.up * flipperHotspots[i].flipperUpwardThrust, ForceMode2D.Impulse);
                    // Try breaking and just applying on hotspot.
                    break;
                }
            }
        }

    }

    [Tooltip("True for Right Flipper, false for Left Flipper.")]
    public bool rightFlipper;


    // Hinge joints and motors:
    /*HingeJoint2D joint;
    JointMotor2D jointMotor;

    public float flipperMotorSpeed;

    private void Start()
    {
        joint = GetComponent<HingeJoint2D>();
        jointMotor = joint.motor;
    }

    void Update()
    {
        if (Input.GetKey(myFlipperKey))
        {
            jointMotor.motorSpeed = flipperMotorSpeed; //Mathf.Abs(jointMotor.motorSpeed);
            joint.motor = jointMotor;
        }
        else
        {
            jointMotor.motorSpeed = flipperMotorSpeed * -1; //Mathf.Abs(jointMotor.motorSpeed) * -1f;
            joint.motor = jointMotor;
        }
    }
    */

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag.Trim().Equals("Pinball".Trim()))
        {
            if (pinballRB == null)
            {
                pinballRB = col.gameObject.GetComponent<Rigidbody2D>();
            }
        }
    }
}