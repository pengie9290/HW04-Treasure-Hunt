using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kitten_Control : MonoBehaviour
{
    public GameObject KittenSit;
    public GameObject KittenWalk;
    public bool IsSitting = true;
    public float Speed = 1f;

    float UpRotation = 180;
    float DownRotation = 0;
    float LeftRotation = 270;
    float RightRotation = 90;

    float UpLeftRotation = 225;
    float UpRightRotation = 135;
    float DownLeftRotation = 315;
    float DownRightRotation = 45;

    float NeutralRotation = 0f;

    bool MovingUp = false;
    bool MovingDown = false;
    bool MovingLeft = false;
    bool MovingRight = false;

    public int MouseCount = 0;

    Vector3 PreviousLocation;
    Vector3 AdjustedLocation;
    public float Adjuster = 3f;
    private float recovery = 0;


    void Start()
    {
        KittenSit.SetActive(true);
        KittenWalk.SetActive(false);
        IsSitting = true;
    }

    void Update()
    {
        KittenDirections();
        KittenDirector();
        MoveKitten();
        AnimationControl();
    }

    //Determines what direction(s) the kitten is moving in
    void KittenDirections()
    {
        if (recovery > 0)
        {
            recovery -= Time.deltaTime;
        } else {
            if (Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.DownArrow))
        {
            MovingUp = true;
        }
        else MovingUp = false;

        if (Input.GetKey(KeyCode.DownArrow) && !Input.GetKey(KeyCode.UpArrow))
        {
            MovingDown = true;
        }
        else MovingDown = false;

        if (Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow))
        {
            MovingLeft = true;
        }
        else MovingLeft = false;

        if (Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow))
        {
            MovingRight = true;
        }
        else MovingRight = false;
    }
    }

    //Directs the kitten's rotations
    void KittenDirector()
    {
        //When Kitten is moving upwards
        if (MovingUp)
        {
            //Rotate Kitten Up-Left?
            if (MovingLeft)
            {
                RotateKitten(UpLeftRotation);
            }
            //Rotate Kitten Up-Right?
            else if (MovingRight)
            {
                RotateKitten(UpRightRotation);
            }
            //Rotate Kitten Straight Up
            else
            {
                RotateKitten(UpRotation);
            }
        }

        //When Kitten is moving downwards
        if (MovingDown)
        {
            //Rotate Kitten Down-Left?
            if (MovingLeft)
            {
                RotateKitten(DownLeftRotation);
            }
            //Rotate Kitten Down-Right?
            else if (MovingRight)
            {
                RotateKitten(DownRightRotation);
            }
            //Rotate Kitten Straight Down
            else
            {
                RotateKitten(DownRotation);
            }
        }
        //Rotate Kitten Straight Left
        if (MovingLeft && !MovingUp && !MovingDown)
        {
            RotateKitten(LeftRotation);
        }
        //Rotate Kitten Straight Right
        if (MovingRight && !MovingUp && !MovingDown)
        {
            RotateKitten(RightRotation);
        }
        //Return Kitten to Neutral State
        if (!MovingUp && !MovingDown && !MovingLeft && !MovingRight)
        {
            RotateKitten(NeutralRotation);
        }
    }

    //Makes the kitten move
    void MoveKitten()
    {
        PreviousLocation = transform.position;
        if (MovingUp) transform.position = new Vector3(transform.position.x, transform.position.y + Speed * Time.deltaTime, transform.position.z);
        if (MovingDown) transform.position = new Vector3(transform.position.x, transform.position.y - Speed * Time.deltaTime, transform.position.z);
        if (MovingLeft) transform.position = new Vector3(transform.position.x - Speed * Time.deltaTime, transform.position.y, transform.position.z);
        if (MovingRight) transform.position = new Vector3(transform.position.x + Speed * Time.deltaTime, transform.position.y, transform.position.z);
    }

    //Makes the kitten rotate
    void RotateKitten(float newRotation)
    {
        transform.eulerAngles = new Vector3(
            transform.eulerAngles.x,
            transform.eulerAngles.y,
            newRotation);
    }

    //Makes the right animation play
    void AnimationControl()
    {
        if (MovingUp || MovingDown || MovingLeft || MovingRight)
        {
            KittenSit.SetActive(false);
            KittenWalk.SetActive(true);
        }
        else
        {
            KittenSit.SetActive(true);
            KittenWalk.SetActive(false);
        }
    }

    //Pushes kitten away from walls
    public void PushedAway()
    {
        recovery = 0.3f;
        AdjustedLocation = transform.position - PreviousLocation;
        AdjustedLocation *= Adjuster;
        transform.position -= AdjustedLocation;
        FreezeMovement();
    }

    void FreezeMovement()
    {
        MovingDown = false;
        MovingUp = false;
        MovingLeft = false;
        MovingRight = false;
    }
}
