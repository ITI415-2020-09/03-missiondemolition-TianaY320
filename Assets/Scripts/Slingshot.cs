using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slingshot : MonoBehaviour
{
    public GameObject launchPoint;

    //turns halo on and off to indicate mouse is over slingshot
    void Awake()
    {
        //finds gameObject launch point and gets transform
        Transform launchPointTrans = transform.Find("LaunchPoint");
        launchPoint = launchPointTrans.gameObject;
        //tells the game to ignore launch point
        launchPoint.SetActive(false);
    }

    //Both MouseEnter and MouseExit shows if mouse
    //is over the slingshot
    void OnMouseEnter()
    {
        //print("Slingshot:OnMouseEnter()"); 
        launchPoint.SetActive(true);
    }

    void OnMouseExit()
    {
        //print("Slingshot:OnMouseExit()");
        launchPoint.SetActive(false);
    }
}
