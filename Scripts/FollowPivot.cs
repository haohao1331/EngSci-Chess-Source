using UnityEngine;

public class FollowPivot : MonoBehaviour
{
    public Transform pivot;
    public Vector3 offset;

    // Update is called once per frame
    void Update()
    {
        transform.position = pivot.position + offset;
        //Debug.Log(pivot.position);

    }
}
