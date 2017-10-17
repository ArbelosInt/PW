using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeColliderHandler : MonoBehaviour
{
    public struct MyTree
    {
        public Vector3 treePos;
    }

    public GameObject treeColliderObj;
    public List<GameObject> treeColliders;

    public List<MyTree> trees;

    public List<GameObject> deer;

    public float detectionRadius;

    bool initialized = false;

    private void Update()
    {
        if(initialized)
        {
            SpawnColliders();
            RemoveColliders();
        }
    }

    public static TreeColliderHandler Instance
    {
        get
        {
            return _instance;
        }
    }

    private static TreeColliderHandler _instance;

    private void Awake()
    {
        if(_instance == null)
        {
            _instance = this;
        }
    }

    public void InitializeHandler(GameObject A, GameObject B, GameObject C, GameObject D)
    {
        trees = new List<MyTree>();
        Terrain terrain = A.GetComponent<Terrain>();
        Debug.Log(terrain.GetPosition());
        TerrainData terrainData = terrain.terrainData;

        foreach(TreeInstance tree in terrainData.treeInstances)
        {
            MyTree m_tree = new MyTree();
            m_tree.treePos = new Vector3(tree.position.x * terrainData.size.x, tree.position.y * terrainData.size.y, tree.position.z * terrainData.size.z) + A.GetComponent<Terrain>().GetPosition();
            trees.Add(m_tree);
        }

        terrain = B.GetComponent<Terrain>();
        Debug.Log(terrain.GetPosition());
        terrainData = terrain.terrainData;

        foreach (TreeInstance tree in terrainData.treeInstances)
        {
            MyTree m_tree = new MyTree();
            m_tree.treePos = new Vector3(tree.position.x * terrainData.size.x, tree.position.y * terrainData.size.y, tree.position.z * terrainData.size.z) + B.GetComponent<Terrain>().GetPosition();
            trees.Add(m_tree);
        }

        terrain = C.GetComponent<Terrain>();
        Debug.Log(terrain.GetPosition());
        terrainData = terrain.terrainData;

        foreach (TreeInstance tree in terrainData.treeInstances)
        {
            MyTree m_tree = new MyTree();
            m_tree.treePos = new Vector3(tree.position.x * terrainData.size.x, tree.position.y * terrainData.size.y, tree.position.z * terrainData.size.z) + C.GetComponent<Terrain>().GetPosition();
            trees.Add(m_tree);
        }

        terrain = D.GetComponent<Terrain>();
        Debug.Log(terrain.GetPosition());
        terrainData = terrain.terrainData;

        foreach (TreeInstance tree in terrainData.treeInstances)
        {
            MyTree m_tree = new MyTree();
            m_tree.treePos = new Vector3(tree.position.x * terrainData.size.x, tree.position.y * terrainData.size.y, tree.position.z * terrainData.size.z) + D.GetComponent<Terrain>().GetPosition();
            trees.Add(m_tree);
        }

        initialized = true;
        treeColliders = new List<GameObject>();
    }

    public void SpawnColliders()
    {
        foreach(GameObject deerObj in deer)
        {
            foreach(MyTree tree in trees)
            {
                if (Vector3.Distance(deerObj.transform.position, tree.treePos) <= detectionRadius)
                {
                    if (!CheckColliderAlreadyExists(tree.treePos))
                    {
                        GameObject treeCol = Instantiate(treeColliderObj, tree.treePos, Quaternion.identity);
                        treeColliders.Add(treeCol);
                    }
                }
            }
        }
    }

    public void RemoveColliders()
    {
        if (treeColliders.Count > 0)
        {
            bool remove = true;

            for (int i = treeColliders.Count - 1; i >= 0; i--)
            {
                foreach (GameObject deerObj in deer)
                {
                    if (Vector3.Distance(deerObj.transform.position, treeColliders[i].transform.position) <= detectionRadius)
                    {
                        remove = false;
                        break;
                    }
                }

                if(remove)
                {
                    GameObject tree = treeColliders[i];
                    treeColliders.RemoveAt(i);
                    Destroy(tree);
                }
            }
        }
    }

    public bool CheckColliderAlreadyExists(Vector3 treePos)
    {
        foreach(GameObject treeCol in treeColliders)
        {
            if (treePos == treeCol.transform.position)
            {
                return true;
            }
        }

        return false;
    }
}
