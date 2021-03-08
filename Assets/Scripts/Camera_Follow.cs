using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Follow : MonoBehaviour
{
    public GameObject ObjectToFollow;
    public float SpeedAdjuster = 0.9f;
    public float Speed = 0.5f;

    void Start()
    {
        if (ObjectToFollow.GetComponent<Kitten_Control>() != null) Speed = ObjectToFollow.GetComponent<Kitten_Control>().Speed * SpeedAdjuster;
        if (ObjectToFollow.GetComponent<Mouse_Control>() != null) Speed = ObjectToFollow.GetComponent<Mouse_Control>().Speed * SpeedAdjuster;
    }

    void Update()
    {
        Vector3 NewPosition = new Vector3(ObjectToFollow.transform.position.x, ObjectToFollow.transform.position.y, transform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, NewPosition, Speed * Time.deltaTime);
    }
}
