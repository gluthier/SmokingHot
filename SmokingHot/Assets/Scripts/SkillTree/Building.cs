using UnityEngine;
using System.Collections.Generic;

public class Building : MonoBehaviour
{
    public enum TYPE{
        MANUFACTURING,
        PUBLICITY,
        LOBBYING,
        POPULARITY
    }

    public TYPE type;
    public List<GameObject> models = new List<GameObject>();
}
