using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse_Control : MonoBehaviour
{
    public GameObject Follow; //Object to run towards
    public Vector3 FollowDirection = new Vector3(); //Tracks direction from mouse to cat
    public float FollowDistance = 0; //Tracks distance between mouse and cat

    public float MinRange = 3; //How close cat can get before running
    public float MaxRange = 5; //Distance to run to
    public bool Following = false; //Is the mouse running away?

    public float Speed = 1;
    public float SpeedMultiplier = 1.2f;


    void UpdateFollow()
    {
        if (Follow.GetComponent<Kitten_Control>() != null) Speed = Follow.GetComponent<Kitten_Control>().Speed * SpeedMultiplier;
        if (Follow.GetComponent<Mouse_Control>() != null) Speed = Follow.GetComponent<Mouse_Control>().Speed * SpeedMultiplier;

    }


    void Update()
    {
        FollowDistance = Vector3.Distance(transform.position, Follow.transform.position);
        if (FollowDistance < MinRange && Following) Following = false;
        if (FollowDistance > MaxRange && !Following) Following = true;
        if (Following)
        {
            FollowDirection = Follow.transform.position - transform.position;
            FollowDirection.Normalize(); //Alters FollowDirection to ONLY give direction, removing distance
            MoveTowardsFollow();
            print(FollowDirection);
        }
    }

    void MoveTowardsFollow()
    {
        Vector3 DirectionToMove = FollowDirection * Speed * Time.deltaTime;
        transform.position += DirectionToMove;
        RotateTowardsFollow(DirectionToMove);
    }

    void RotateTowardsFollow(Vector3 direction)
    {
        transform.LookAt(direction + transform.position);
    }
}
