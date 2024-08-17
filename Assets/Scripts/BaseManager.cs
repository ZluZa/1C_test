using System.Collections;
using UnityEngine;

public class BaseManager : MonoBehaviour
{
    public virtual IEnumerator Init()
    {
        yield return null;
    }
}