using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slingshot : MonoBehaviour
{
    static private Slingshot S;

    [Header("Set in Inspector")]

    public GameObject prefabProjectile;
    public float velocityMult = 8f;

    [Header("Set Dynamically")]
    public GameObject launchPoint;

    public Vector3 launchPos;
    public GameObject projectile;
    public bool aimingMode;
    private Rigidbody projectileRigidbody;

    static public Vector3 LAUNCH_POS
    {
        get
        {
            if (S == null) return Vector3.zero;
            return S.launchPos;
        }
    }

    //turns halo on and off to indicate mouse is over slingshot
    void Awake()
    {
        S = this;

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
        //if it's not in aming mode don't run this code
        if (!aimingMode) return;

        Vector3 mousePos2D = Input.mousePosition;
        mousePos2D.z = -Camera.main.transform.position.z;
        Vector3 mousePos3D = Camera.main.ScreenToWorldPoint(mousePos2D);

        //find the delta from LaunchPos to mousePos
        Vector3 mouseDelta = mousePos3D - launchPos;
        //limit mouse to radius of spherecollider
        float maxMagnitude = this.GetComponent<SphereCollider>().radius;

        if (mouseDelta.magnitude > maxMagnitude)
        {
            mouseDelta.Normalize();
            mouseDelta *= maxMagnitude;
        }

        //moves the projectile to the new position
        Vector3 projPos = launchPos + mouseDelta;
        projectile.transform.position = projPos;

        if (Input.GetMouseButtonUp(0))
        {
            //mouse has been release
            aimingMode = false;
            projectileRigidbody.isKinematic = false;
            projectileRigidbody.velocity = -mouseDelta * velocityMult;
            FollowCam.POI = projectile;
            projectile = null;

            MissionDemolition.ShotFired();
            ProjectileLine.S.poi = projectile;
        }

    }
}
