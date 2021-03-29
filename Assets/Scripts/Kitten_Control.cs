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

    public float MinWallDistance = 0.1f;

    public GameObject HeldCheese;
    public bool HoldingCheese = false;

    public List<GameObject> Mice = new List<GameObject>();

    public bool UpInput = false;
    public bool DownInput = false;
    public bool LeftInput = false;
    public bool RightInput = false;


    void Start()
    {
        KittenSit.SetActive(true);
        KittenWalk.SetActive(false);
        IsSitting = true;
        HoldingCheese = false;
        HeldCheese.SetActive(false);
    }

    void Update()
    {
        DirectionInputs();
        KittenDirections();
        CastRaycasts();
        KittenDirector();
        MoveKitten();
        AnimationControl();

        if (HeldCheese != null) HeldCheese.SetActive(HoldingCheese);
    }

    //Determines what directions the player is inputting
    void DirectionInputs()
    {
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) UpInput = true;
        else UpInput = false;
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)) DownInput = true;
        else DownInput = false;
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) LeftInput = true;
        else LeftInput = false;
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) RightInput = true;
        else RightInput = false;
    }

    //Determines what direction(s) the kitten is moving in
    void KittenDirections()
    {
        if (recovery > 0)
        {
            recovery -= Time.deltaTime;
        } else {
            if (UpInput && !DownInput)
        {
            MovingUp = true;
        }
        else MovingUp = false;

        if (DownInput && !UpInput)
        {
            MovingDown = true;
        }
        else MovingDown = false;

        if (LeftInput && !RightInput)
        {
            MovingLeft = true;
        }
        else MovingLeft = false;

        if (RightInput && !LeftInput)
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
    //public void PushedAway()
    //{
    //    recovery = 0.3f;
    //    AdjustedLocation = transform.position - PreviousLocation;
    //    AdjustedLocation *= Adjuster;
    //    transform.position -= AdjustedLocation;
    //    FreezeMovement();
    //}

    //void FreezeMovement()
    //{
    //    MovingDown = false;
    //    MovingUp = false;
    //    MovingLeft = false;
    //    MovingRight = false;
    //}

    //Checks to see what to do when colliding with something
    void OnCollisionEnter(Collision collision)
    {
        //If colliding with uncollected cheese
        if (collision.gameObject.CompareTag("Cheese") && !HoldingCheese)
        {
            HoldingCheese = true;
            Destroy(collision.gameObject);
            SFX_Manager.Instance.PlaySFX(0);
        }

        if (collision.gameObject.CompareTag("Mouse"))
        {
            Mouse_Control Mouse = collision.gameObject.GetComponent<Mouse_Control>();
            if (Mouse.IsFollowing == false && HoldingCheese == true)
            {
                MakeMouseFollow(Mouse);
                HoldingCheese = false;
                SFX_Manager.Instance.PlaySFX(1);
                MouseCount += 1;
            }
        }
    }

    //Makes the mouse follow something
    void MakeMouseFollow(Mouse_Control Mouse)
    {
        if (Mice.Count == 0) Mouse.Follow = this.gameObject;
        else Mouse.Follow = Mice[Mice.Count - 1];
        Mice.Add(Mouse.gameObject);
    }

    //Casts raycasts to determine if Kitten can move
    void CastRaycasts()
    {
        if (MovingUp) MovingUp = CastRay(Vector3.up);
        if (MovingDown) MovingDown = CastRay(Vector3.down);
        if (MovingLeft) MovingLeft = CastRay(Vector3.left);
        if (MovingRight) MovingRight = CastRay(Vector3.right);
    }

    //Casts individual rays
    bool CastRay(Vector3 direction)
    {
        RaycastHit hit;
        Physics.Raycast(transform.position, direction, out hit);
        if (hit.collider != null && hit.collider.gameObject != null)
        {
            if (hit.collider.gameObject.CompareTag("Wall"))
            {
                if (hit.collider.gameObject.GetComponent<Walls>().MiceToBreak > MouseCount && hit.distance <= MinWallDistance) return false;
            }
        }
        return true;
    }
}
