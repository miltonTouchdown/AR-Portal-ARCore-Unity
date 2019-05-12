using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class InterdimensionalTransport : MonoBehaviour {

    public Material[] materials;

	void Start ()
    {
        SetMaterials(false);
    }

    private void SetMaterials(bool fullRender)
    {
        var stencilTest = fullRender ? CompareFunction.NotEqual : CompareFunction.Equal;

        foreach (var mat in materials)
        {
            mat.SetInt("_StencilTest", (int)stencilTest);
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.name != "Main Camera")
            return;

        // Outside of other world
        if(transform.position.z > other.transform.position.z)
        {
            foreach(var mat in materials)
            {
                mat.SetInt("_StencilTest", (int)CompareFunction.Equal);
            }
        // Inside other dimension
        }
        else
        {
            foreach(var mat in materials)
            {
                mat.SetInt("_StencilTest", (int)CompareFunction.NotEqual);
            }
        }
    }

    void OnDestroy()
    {
        SetMaterials(true);
    }

    void Update () {
		
	}
}
