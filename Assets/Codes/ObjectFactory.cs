using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFactory : MonoBehaviour
{

    private static List<GameObject> used = new List<GameObject>();
    private static List<GameObject> free = new List<GameObject>();
    

   
    public GameObject setObjectOnPos(Vector3 targetposition, Quaternion faceposition)
    {
        if (free.Count == 0)
        {
            GameObject aGameObject = Instantiate(Resources.Load("prefabs/Patrol")
                , targetposition, faceposition) as GameObject;
          
            used.Add(aGameObject);
        }
        else
        {
            used.Add(free[0]);
            free.RemoveAt(0);
            used[used.Count - 1].SetActive(true);
            used[used.Count - 1].transform.position = targetposition;
            used[used.Count - 1].transform.localRotation = faceposition;
        }
        return used[used.Count - 1];
    }

    public void freeObject(GameObject oj)
    {
        oj.SetActive(false);
        used.Remove(oj);
        free.Add(oj);
    }
}
