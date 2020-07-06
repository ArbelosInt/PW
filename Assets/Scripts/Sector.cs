using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sector : MonoBehaviour
{
    public List<GameObject> objects;
    public List<Vector3> trees;
    public List<GameObject> treeColliders;

    private void Awake()
    {
        objects = new List<GameObject>();
        trees = new List<Vector3>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Deer" || other.gameObject.tag == "Puma")
        {
            if(objects.Count == 0)
            {
                SpawnColliders();
            }

            objects.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Deer" || other.gameObject.tag == "Puma")
        {
            objects.Remove(other.gameObject);

            if(objects.Count == 0)
            {
                RemoveColliders();
            }
        }
    }

    public void SpawnColliders()
    {
        foreach(Vector3 tree in trees)
        {
            TreeColliderHandler.Instance.SpawnTree(tree, this);
        }
    }

    public void RemoveColliders()
    {
        for (int i = treeColliders.Count - 1; i >= 0; i--)
        {
            GameObject tree = treeColliders[i];
            treeColliders.RemoveAt(i);
            Destroy(tree);
        }
    }
}
