using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walls : MonoBehaviour
{
    public int MiceToBreak = 1;
    public Kitten_Control Player;

    void OnCollisionStay(Collision collision)
    {
        print("Collided");
        if (collision.gameObject == Player.gameObject && Player.MouseCount >= MiceToBreak)
        {
            print("Tries to destroy");
            DestroyWall();
        } else
        {
            Player.PushedAway();
        }
    }
    

    void DestroyWall()
    {
        print("Destroys");
        Destroy(gameObject);
    }
}
