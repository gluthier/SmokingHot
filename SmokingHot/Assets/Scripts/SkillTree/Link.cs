using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class Link : MonoBehaviour
{
    public Skill start;
    public Skill end;
    private bool isActivated = false;

    public void activate()
    {
        isActivated = true;
    }

    public bool getActivated()
    {
        return isActivated;
    }
}
