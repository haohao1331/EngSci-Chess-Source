using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FaceCamera : MonoBehaviour
{
    public Transform piece;

    //Vector3 cameraPosition = new Vector3();
    //Vector3 piecePosition = new Vector3();
    //Vector3 dir = new Vector3();
    

    void Update()
    {
        //cameraPosition = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, Camera.main.transform.position.z);
        //piecePosition = new Vector3(piece.position.x, piece.position.y, piece.position.z);
        //dir = cameraPosition - piecePosition;
        //Debug.Log(dir);
        transform.LookAt(Camera.main.transform.position);

    }
}
