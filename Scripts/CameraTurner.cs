using UnityEngine;

public class CameraTurner : MonoBehaviour
{
    public float speed;
    public float zoomSpeed;
    public Vector3 pivot;
    private int rotateDirection = 0;
    private int zoomDirection = 0;
    private int verticalRotateDirection = 0;
    private float criticalDistance = 2;
    private float furthestDistance = 15;
	private double vCritTop = 12;
	private double vCritBot = -8;
    private Vector3 camRotComparisonVector = new Vector3Int(0, 0, 0);


    // Update is called once per frame
    void Update()
    {
        HandleKeyInput();
        if (rotateDirection != 0)
        {
            transform.RotateAround(pivot, Vector3.up, rotateDirection * speed * Time.deltaTime);
            
        }

        if (ableToRotateVertical(verticalRotateDirection))
		{
            //Debug.Log("rotating");
			transform.RotateAround(pivot, getRotateAroundVector(), verticalRotateDirection * speed * Time.deltaTime);
		}
        if (zoomDirection == 1 && Vector3.Magnitude(transform.position-pivot) > criticalDistance)
        {
            transform.position = Vector3.MoveTowards(transform.position, pivot, zoomSpeed * zoomDirection * Time.deltaTime);
        }
        if (zoomDirection == -1 && Vector3.Magnitude(transform.position - pivot) < furthestDistance)
        {
            transform.position = Vector3.MoveTowards(transform.position, pivot, zoomSpeed * zoomDirection * Time.deltaTime);
        }

    }

    void HandleKeyInput()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            rotateDirection = -1;
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            rotateDirection = 0;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            rotateDirection = 1;
        }
        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            rotateDirection = 0;
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            verticalRotateDirection = -1;
        }
        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            verticalRotateDirection = 0;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            verticalRotateDirection = 1;
        }
        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            verticalRotateDirection = 0;
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            zoomDirection = 1;
        }
        if (Input.GetKeyUp(KeyCode.N))
        {
            zoomDirection = 0;
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            zoomDirection = -1;
        }
        if (Input.GetKeyUp(KeyCode.M))
        {
            zoomDirection = 0;
        }
    }

    public int getCameraRegion(float x, float z)
    {
        //float x = Camera.main.transform.position.x;
        //float z = Camera.main.transform.position.z;
        if (x > 0 && z / x < 1 && z / x >= -1)
        {
            return 1;
        }
        else if (z > 0 && (z / x >= 1 || z / x < -1))
        {
            return 2;
        }
        else if (x < 0 && z / x < 1 && z / x >= -1)
        {
            return 3;
        }
        else
        {
            return 4;
        }
    }

    public bool isCameraFaceUp()
    {
        return true;
    }

    public Vector3 crossProduct(Vector3 a, Vector3 b)
    {
        Vector3 ret = new Vector3(0, 0, 0);
        ret.x = a.y * b.z - a.z * b.y;
        ret.y = a.z * b.x - a.x * b.z;
        ret.z = a.x * b.y - a.y * b.x;
        return ret;
    }

    public Vector3 getRotateAroundVector()
    {
        Vector3 camPos = Camera.main.transform.position;
        return crossProduct(Vector3.up, pivot - camPos);
    }

    public bool ableToRotateVertical(int rotDirection)
    {
        Vector3 direction;
        if (rotDirection == 1)
        {
            direction = Vector3.up;
        }
        else
        {
            direction = Vector3.down;
        }
        double angle = Vector3.Angle(Camera.main.transform.position - pivot, direction);
        if (angle < 10)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

}
