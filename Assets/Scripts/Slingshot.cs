using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slingshot : MonoBehaviour
{
    [Header("Set in Inspector")]

    public GameObject prefabProjectile;
    public float velocityMult = 8f;

    [Header("Set Dynamically")]
    public GameObject launchPoint;

    public Vector3 launchPos;
    public GameObject projectile;
    public bool aimingMode;
    private Rigidbody projectileRigidbody;

    //turns halo on and off to indicate mouse is over slingshot
    void Awake()
    {
        //finds gameObject launch point and gets transform
        Transform launchPointTrans = transform.Find("LaunchPoint");
        launchPoint = launchPointTrans.gameObject;
        //tells the game to ignore launch point
        launchPoint.SetActive(false);
        launchPos = launchPointTrans.position;
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

    void OnMouseDown()
    {
        //pressed button over slingshot
        aimingMode = true;
        //Create projectile
        projectile = Instantiate(prefabProjectile) as GameObject;
        //start at launchpoint
        projectile.transform.position = launchPos;
        //sets Kinematic as true
        projectile.GetComponent<Rigidbody>().isKinematic = true;

        projectileRigidbody = projectile.GetComponent<Rigidbody>();
        projectileRigidbody.isKinematic = true;
    }

    void Update()
    {
        if (!aimingMode) return;

    }
}
