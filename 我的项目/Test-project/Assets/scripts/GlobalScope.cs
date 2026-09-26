using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class GlobalScope
{
    public static GameObject GetParentGameObjectByComponent(MonoBehaviour component)
    {
        Transform parentTransform = component.gameObject.transform.parent;
        if (parentTransform != null)
        {
            return parentTransform.gameObject;
        }
        else
        {
            return null;
        }
    }

    public static GameObject GetParentGameObjectByGameObject(GameObject gameObject)
    {
        Transform parentTransform = gameObject.transform.parent;
        if (parentTransform != null)
        {
            return parentTransform.gameObject;
        }
        else
        {
            return null;
        }
    }
}
