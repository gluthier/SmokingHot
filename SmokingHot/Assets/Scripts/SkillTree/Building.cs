using UnityEngine;
using System.Collections.Generic;

public class Building : MonoBehaviour
{
    public enum TYPE{
        CIGARETTE,
        PUBLICITY,
        LOBBYING,
        REPUTATION
    }

    public TYPE type;
    public List<GameObject> models = new List<GameObject>();
}
