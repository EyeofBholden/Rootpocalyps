using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class rootSegment : MonoBehaviour
{
    // Prefab reference.
    public GameObject root;
    public GameObject bombRoot;

    // GameObject references.
    public GameObject parent;
    public Dictionary<int, GameObject> children;
    //public List<GameObject> children;

    // Other members.
    public float length;
    public bool spawnEnabled;
    public int spawnTimerMin;
    public int spawnTimerMax;
    public int spawnTimer;
    public int spawnRestartTime;
    public float oneChildProb;
    public float twoChildProb;
    public float threeChildProb;
    public float childDirMaxAngle;
    public float childDirAngleBias;
    public float rootProb;
    public float bombRootProb;
    private int clearCount = 0;
    private int permaTimer = 30;

    void Start()
    {
        Vector3 pos = gameObject.transform.position;
        length = 0.8f;
        spawnEnabled = (pos.x > -8.0 && pos.x < 8.0 && pos.y > -4.5f) ? true : false;
        spawnTimerMin = 25;
        spawnTimerMax = 125;
        spawnRestartTime = 50;
        //spawnTimer = 10;
        oneChildProb = 0.8f;
        twoChildProb = 0.15f;
        threeChildProb = 0.05f;
        childDirMaxAngle = 60.0f;
        childDirAngleBias = childDirMaxAngle;
        rootProb = 0.85f;
        bombRootProb = 0.15f;
        permaTimer = 30;
        children = new Dictionary<int, GameObject>();
        if (gameObject.tag == "permaroot"){
            Spawn(3);
        }
        spawnTimer = (int)Random.Range((float)spawnTimerMin, (float)spawnTimerMax);
    }

    void Update()
    {


    
    }


    public void RestartGrowing()
    {
        spawnTimer = spawnRestartTime;
        spawnEnabled = true;
    }

    public void StopGrowing()
    {
        spawnEnabled = false;
        if (children != null)
        {
            foreach (int key in children.Keys)
            {
                GameObject child = children[key];
                rootSegment childRS = child.GetComponent<rootSegment>();
                childRS.StopGrowing();
            }
        }
    }

    public void RemoveChild(int childID)
    {
        children.Remove(childID);
    }

    private void FixedUpdate()
    {
        if (spawnTimer > 0) spawnTimer -= 1;
        if (permaTimer > 0) permaTimer -= 1;
        if (spawnTimer == 0 && spawnEnabled)
        {
            spawnEnabled = false;
            Spawn();
        }
        
        if (children.Count == 0 && gameObject.tag == "permaroot" && permaTimer == 0){
            Spawn(clearCount + 3);
            clearCount ++;
            if (clearCount > 7){
                clearCount = 7;
            }
            permaTimer = 30;
        }
    }

    private void Spawn(int forcedspawn = 0)
    {
        int numChildren = forcedspawn;
        if (root == null || bombRoot == null)
        {
            RestartGrowing();
            return;
        }

        // Obtain position of current root.
        Vector3 pos = gameObject.transform.position;

        // Obtain rotation of current root.
        Vector3 rot = gameObject.transform.eulerAngles;

        // Obtain direction of current root.
        Vector3 dir = gameObject.transform.up;

        // Compute position of next roots.
        Vector3 childPos = pos + (dir * length);

        // Generate number of children.
        if (forcedspawn == 0){
            numChildren = GetNumberOfChildren();
        }
        // Instantiate children.
        for (int i = 0; i < numChildren; i++)
        {
            // Generate rotation of child.
            Vector3 childRot = GetChildRotation(rot);

            // Create child.
            GameObject child = createRoot(childPos, childRot, forcedspawn);

            // Set parent to this.
            rootSegment childRS = child.GetComponent<rootSegment>();
            childRS.parent = gameObject;

            // Store reference.
            int childID = child.GetInstanceID();
            children.Add(child.GetInstanceID(), child);
        }

        //print("spawned");
    }

    private int GetNumberOfChildren()
    {
        float val = Random.Range(0.0f, 1.0f);
        if (val < oneChildProb)
        {
            return 1;
        }
        else if (val < oneChildProb + twoChildProb)
        {
            return 2;
        }
        else
        {
            return 3;
        }
    }

    private Vector3 GetChildRotation(Vector3 parentRot)
    {
        float childDirMin = -childDirMaxAngle;
        float childDirMax = childDirMaxAngle;

        // Get angle from world "up" direction to parent direction.
        Vector3 worldUp = new Vector3(0.0f, 1.0f, 0.0f);
        Vector3 parentDir = gameObject.transform.up;
        Vector3 zAxis = new Vector3(0.0f, 0.0f, 1.0f);
        float angleFromUp = Vector3.SignedAngle(worldUp, parentDir, zAxis);

        // If parent is greater than horizontal,
        // bias the random generation of the child direction towards world "down" direction.
        if (Mathf.Abs(angleFromUp) < 90.0f)
        {
            if (angleFromUp < 0.0f)
            {
                childDirMin -= childDirAngleBias;
                childDirMax -= childDirAngleBias;
            }
            else
            {
                childDirMin += childDirAngleBias;
                childDirMax += childDirAngleBias;
            }
        }

        // Generate child direction.
        float relativeRotation = Random.Range(childDirMin, childDirMax);
        return new Vector3(parentRot.x, parentRot.y, parentRot.z + relativeRotation);
    }

    private GameObject createRoot(Vector3 rootPos, Vector3 rootRot, int perma = 0)
    {
        float val = Random.Range(0.0f, 1.0f);
        if (val < rootProb || perma != 0)
        {
            return Instantiate(root, rootPos, Quaternion.Euler(rootRot));
        }
        else
        {
            return Instantiate(bombRoot, rootPos, Quaternion.Euler(rootRot));
        }
    }

    private void OnDestroy()
    {
        if (parent != null)
        {
            rootSegment parentRS = parent.GetComponent<rootSegment>();
            parentRS.RemoveChild(gameObject.GetInstanceID());
            parentRS.RestartGrowing();
        }

        StopGrowing();
    }
}
