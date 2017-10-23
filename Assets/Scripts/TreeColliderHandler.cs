using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeColliderHandler : MonoBehaviour
{
    public GameObject treeColliderObj;

    public List<Vector3> trees;

    public List<GameObject> Sectors;

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

        if(Sectors != null)
        {
            for(int i = Sectors.Count - 1; i >= 0; i--)
            {
                Destroy(Sectors[i]);
            }
        }

        Sectors = new List<GameObject>();
        trees = new List<Vector3>();
        Terrain terrain = A.GetComponent<Terrain>();
        TerrainData terrainData = terrain.terrainData;

        GenerateSectors(A.GetComponent<Terrain>());
        GenerateSectors(B.GetComponent<Terrain>());
        GenerateSectors(C.GetComponent<Terrain>());
        GenerateSectors(D.GetComponent<Terrain>());

        foreach (TreeInstance tree in terrainData.treeInstances)
        {
            Vector3 m_tree_A = new Vector3(tree.position.x * terrainData.size.x, tree.position.y * terrainData.size.y, tree.position.z * terrainData.size.z) + A.GetComponent<Terrain>().GetPosition();
            trees.Add(m_tree_A);
            Vector3 m_tree_B = new Vector3(tree.position.x * terrainData.size.x, tree.position.y * terrainData.size.y, tree.position.z * terrainData.size.z) + B.GetComponent<Terrain>().GetPosition();
            trees.Add(m_tree_B);
            Vector3 m_tree_C = new Vector3(tree.position.x * terrainData.size.x, tree.position.y * terrainData.size.y, tree.position.z * terrainData.size.z) + C.GetComponent<Terrain>().GetPosition();
            trees.Add(m_tree_C);
            Vector3 m_tree_D = new Vector3(tree.position.x * terrainData.size.x, tree.position.y * terrainData.size.y, tree.position.z * terrainData.size.z) + D.GetComponent<Terrain>().GetPosition();
            trees.Add(m_tree_D);
        }

        foreach(GameObject sector in Sectors)
        {
            for(int i = trees.Count - 1; i >= 0; i--)
            {
                if(sector.GetComponent<BoxCollider>().bounds.Contains(trees[i]))
                {
                    trees[i] = sector.transform.InverseTransformPoint(trees[i]);
                    sector.GetComponent<Sector>().trees.Add(trees[i]);
                    trees.RemoveAt(i);
                }
            }
        }

        //terrain = B.GetComponent<Terrain>();
        //Debug.Log(terrain.GetPosition());
        //terrainData = terrain.terrainData;

        //foreach (TreeInstance tree in terrainData.treeInstances)
        //{
        //    MyTree m_tree = new MyTree();
        //    m_tree.treePos = new Vector3(tree.position.x * terrainData.size.x, tree.position.y * terrainData.size.y, tree.position.z * terrainData.size.z) + B.GetComponent<Terrain>().GetPosition();
        //    trees.Add(m_tree);
        //}

        //terrain = C.GetComponent<Terrain>();
        //Debug.Log(terrain.GetPosition());
        //terrainData = terrain.terrainData;

        //foreach (TreeInstance tree in terrainData.treeInstances)
        //{
        //    MyTree m_tree = new MyTree();
        //    m_tree.treePos = new Vector3(tree.position.x * terrainData.size.x, tree.position.y * terrainData.size.y, tree.position.z * terrainData.size.z) + C.GetComponent<Terrain>().GetPosition();
        //    trees.Add(m_tree);
        //}

        //terrain = D.GetComponent<Terrain>();
        //Debug.Log(terrain.GetPosition());
        //terrainData = terrain.terrainData;

        //foreach (TreeInstance tree in terrainData.treeInstances)
        //{
        //    MyTree m_tree = new MyTree();
        //    m_tree.treePos = new Vector3(tree.position.x * terrainData.size.x, tree.position.y * terrainData.size.y, tree.position.z * terrainData.size.z) + D.GetComponent<Terrain>().GetPosition();
        //    trees.Add(m_tree);
        //}
    }

    public void SpawnTree(Vector3 treePos, Sector sector)
    {
        GameObject treeCollider = Instantiate(treeColliderObj, Vector3.zero, Quaternion.identity);
        treeCollider.transform.SetParent(sector.transform);
        treeCollider.transform.localPosition = treePos;

        if(sector.treeColliders == null)
        {
            sector.treeColliders = new List<GameObject>();
        }

        sector.treeColliders.Add(treeCollider);
    }

    public void GenerateSectors(Terrain terrain)
    {
        float sectorX;
        float sectorZ;
        float sectorY;

        sectorX = terrain.terrainData.size.x / 4;
        sectorY = 50f;
        sectorZ = terrain.terrainData.size.z / 4;


        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                GameObject sector = new GameObject();

                sector.transform.localScale = new Vector3(sectorX, sectorY, sectorZ);
                sector.transform.position = terrain.GetPosition() + new Vector3(sectorX * j + (sectorX * 0.5f), terrain.GetPosition().y + (sectorY * 0.5f), sectorZ * i + (sectorZ * 0.5f));
                sector.AddComponent<BoxCollider>();
                sector.GetComponent<Collider>().isTrigger = true;
                sector.name = "Sector";
                sector.transform.SetParent(terrain.gameObject.transform);
                sector.AddComponent<Sector>();
                sector.layer = 12;
                Sectors.Add(sector);
            }
        }
    }
}
