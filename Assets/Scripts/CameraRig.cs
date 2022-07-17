using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRig : MonoBehaviour
{
    //public int numCameras = 4;
    //public bool renderInTexture = true;
    public GameObject exampleCube;

    // Start is called before the first frame update
    void Start() 
    {
        createMultiDisplay();
        //createDemoScene(5, 5.0f);
        //createCameras();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("w"))
        {
            this.transform.position += 0.1f * this.transform.forward;
        }
        if (Input.GetKey("s"))
        {
            this.transform.position -= 0.1f * this.transform.forward;
        }
        if (Input.GetKey("a"))
        {
            this.transform.position -= 0.1f * this.transform.right;
        }
        if (Input.GetKey("d"))
        {
            this.transform.position += 0.1f * this.transform.right;
        }
        if (Input.GetKey("q"))
        {
            this.transform.position += 0.1f * this.transform.up;
        }
        if (Input.GetKey("y"))
        {
            this.transform.position -= 0.1f * this.transform.up;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            this.transform.position = new Vector3( 0,0,0 );
        }
    }

    // rotY, rotX, lensshift

    float[,] cameraData = { { 0.0f, 0.0f, -0.5f }, { 0.0f, -90.0f, -0.5f }, { 0.0f, 90.0f, -0.5f }, { 90.0f, 0.0f, -0.5f },
                            { 0.0f, 0.0f, +0.5f }, { 0.0f, -90.0f, +0.5f }, { 0.0f, 90.0f, +0.5f }, { 90.0f, 0.0f, +0.5f }};

    /*void createCameras()
	{
        for (int i=0; i<numCameras; i++)
		{
            Camera camera = new Camera();
            //camera.transform.parent = transform;

            camera.transform.Rotate(new Vector3( cameraData[i,0], cameraData[i, 1], 0.0f ));
            camera.usePhysicalProperties = true;
            camera.fieldOfView = 90;
            camera.lensShift = new Vector2(0.0f, cameraData[i, 3]);
        }
    }*/

    void createMultiDisplay()
	{
        Debug.Log(Display.displays.Length);
        for (int i=1; i< Display.displays.Length; i++)
            Display.displays[i].Activate();
    }

    void createDemoScene(int n, float size)
    {
        for (int x = -n; x < n; x++)
        {
            for (int y = -n; y < n; y++)
            {
                for (int z = -n; z < n; z++)
                {
                    GameObject cube = Instantiate(exampleCube, new Vector3(x * size, y * size, z * size), Quaternion.identity); ; //CreatePrimitive(PrimitiveType.Cube);
                    Rigidbody rb = cube.GetComponent<Rigidbody>();
                    rb.angularVelocity = new Vector3( x,y,z );
                    //cube.transform.position = new Vector3(x * size, y * size, z * size);
                    //Rigidbody rb = cube.AddComponent(typeof(Rigidbody)) as Rigidbody;
                    //rb.AddTorque(new Vector3(1.0f, 0.0f, 0.0f), ForceMode.Impulse);
                }
            }
        }
    }
}
