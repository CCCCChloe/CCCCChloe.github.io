using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using UnityEngine.UI;

public class myPlayerController : MonoBehaviour
{
    public GameObject camera;
    private float x, y, z;
    private float yaw, pitch, roll;
    private GameObject b_sphere, y_sphere, r_sphere;
    public GameObject[] charaArray;
    public Vector3[] positionArray = new Vector3[3];
    private Vector3 temp;
    private Vector3 size;
    private Vector3 blue, yellow, red;
    static private Vector3 ballsize;
    private float percentage;
    string path;
    private static string id;
    private GameObject globalObject;
    private List<GameObject> newobject = new List<GameObject>();
    private float speed = 1.5f;
    static private Boolean hasCharaInScene = false;
    private int counter = 0;
    private string condition;
    private StreamWriter writer;
    public float heightPara;

    //public InputField mainInputField;


    // Start is called before the first frame update
    void Start()
    {
        b_sphere = GameObject.Find("Sphere_blue");
        y_sphere = GameObject.Find("Sphere_yellow");
        r_sphere = GameObject.Find("Sphere_red");

        
        //Find the global object
        globalObject = GameObject.Find("globalObject");
        // Fetch the ID and condition using prefab
        id = PlayerPrefs.GetString("ID");
        condition = PlayerPrefs.GetString("condition");
        Debug.Log("condition:" + condition);
        Debug.Log("***ID=" + id);
        path = string.Format(@"C:\Users\tp24269\Desktop\data\{0}.txt", id);

        //Write to the csv file
        writer = new StreamWriter(path, true);
        writer.WriteLine("{0}", condition);
        for (int j = 0; j < positionArray.Length; j++)
        {
            writer.WriteLine("{0} ", charaArray[j]);
        }
        blue = b_sphere.transform.position;
        yellow = y_sphere.transform.position;
        red = r_sphere.transform.position;
        writer.WriteLine("{0}, {1} , {2} ", blue, yellow, red);
    }

    // Update is called once per frame
    void Update()
    {
        //open a csv file
        x = camera.transform.position.x;
        y = camera.transform.position.y;
        z = camera.transform.position.z;
        yaw = camera.transform.rotation.z;
        pitch = camera.transform.rotation.y;
        roll = camera.transform.rotation.x;
        //The order for writing into the data file 
        string timestamp = DateTime.Now.ToString();
       

        //write to csv
        writer.WriteLine("{0}, {1},  {2}, {3} , {4} , {5} , {6} ", timestamp, x, y, z, yaw, pitch, roll);

        //close the file
        //writer.Close();


        //Debug.Log("YAW = " + yaw);

        if (condition == "Condition A")
        {
            //Debug.Log("***Condition A***");
            if (Input.GetKeyDown("1")) {
                charaAppear();
                percentage = y / heightPara;
                counter++;
            }
            if (Input.GetKeyDown("2")) {
                newobject.Clear();
                charaAppear();
                percentage = y / (heightPara*2);
                counter++;
            }
            if (Input.GetKeyDown("3"))
            {
                newobject.Clear();
                charaAppear();
                percentage = y / (heightPara/2);
                counter++;
            }
        }
        else if (condition == "Condition B") {
            //Debug.Log("***Condition B***");
            if (Input.GetKeyDown("1"))
            {
                charaAppear();
                percentage = y / heightPara;
                counter++;
            }
            if (Input.GetKeyDown("2"))
            {
                newobject.Clear();
                charaAppear();
                percentage = y / (heightPara/2);
                counter++;
            }
            if (Input.GetKeyDown("3"))
            {
                newobject.Clear();
                charaAppear();
                percentage = y / (heightPara*2);
                counter++;
            }
        }

        if (Input.GetKeyDown("s"))
        {
            y = camera.transform.position.y;
            Debug.Log("y = " + y);
        }

        if (hasCharaInScene)
            grow(percentage);

        if (Input.GetKeyDown("d"))
        {
            hasCharaInScene = false;

        }

        if (!hasCharaInScene)
        {
            grow(0);
            
            if (counter != 0 && newobject[0].transform.localScale.y <= ballsize.y)
            {
                b_sphere.gameObject.SetActive(true);
                y_sphere.gameObject.SetActive(true);
                r_sphere.gameObject.SetActive(true);
                
            }
            if (counter != 0 && newobject[0].transform.localScale.y == 0)
            {
                for (int i = 0; i < positionArray.Length; i++)
                {
                    Destroy(newobject[i]);
                }
            }
            //newobject.Clear();
        }

    }

    private void charaAppear()
    {
        //if (Input.GetKeyDown("space"))
        //{
        b_sphere.gameObject.SetActive(false);
        y_sphere.gameObject.SetActive(false);
        r_sphere.gameObject.SetActive(false);

        for (int i = positionArray.Length - 1; i > 0; i--)
        {
            int index = UnityEngine.Random.Range(0, i + 1); //random integer index such that 0 <= index <= 1
                                                            //switch
            if (i != index)
            {
                temp = positionArray[i];
                positionArray[i] = positionArray[index];
                positionArray[index] = temp;
            }
        }
        for (int j = 0; j < positionArray.Length; j++)
        {
            newobject.Add(Instantiate(charaArray[j], positionArray[j], charaArray[j].transform.rotation));
            ballsize = b_sphere.transform.localScale;
            newobject[j].transform.localScale = ballsize;


            //size.x = (charaArray[j].transform.localScale.x * percentage);
            //size.y = (charaArray[j].transform.localScale.y * percentage);
            //size.z = (charaArray[j].transform.localScale.z * percentage);
            //newobject[j].transform.localScale = size;
        }
        //}


        hasCharaInScene = true;

    }

    private void grow(float percentage)
    {
        size = charaArray[0].transform.localScale * percentage;


        for (int i = 0; i < newobject.Count; i++)
        {
            //newobject[i].transform.localScale = Vector3.Lerp(newobject[i].transform.localScale, size, speed * Time.deltaTime);
            if (i <= 1)
            {
                newobject[i].transform.localScale = Vector3.Lerp(newobject[i].transform.localScale, size, speed * Time.deltaTime);
            } else
            {
                newobject[i].transform.localScale = Vector3.Lerp(newobject[i].transform.localScale, size * 1.2f, speed * Time.deltaTime);
            }

        }

        //newobject[0].transform.localScale = Vector3.Lerp(newobject[0].transform.localScale, size, speed * Time.deltaTime);
        //newobject[1].transform.localScale = Vector3.Lerp(newobject[1].transform.localScale, size, speed * Time.deltaTime);
        //newobject[2].transform.localScale = Vector3.Lerp(newobject[2].transform.localScale, size * 1.2f, speed * Time.deltaTime);

    }

    private void OnDestroy()
    {
        writer.Close();
    }

}
