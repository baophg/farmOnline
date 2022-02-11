using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zxc : MonoBehaviour
{
    public SkinnedMeshRenderer mesh;
    public Mesh clomesh;
    public Transform[] transforms;
    float yOffset = 0f;
    // Start is called before the first frame update
    void Start()
    {
        //for (var blendCount = 0; blendCount < ca.skinnedMesh[i].sharedMesh.blendShapeCount; blendCount++)
        //{
        //    ca.skinnedMesh[i].SetBlendShapeWeight(blendCount, bodyShapeWeight[blendCount]);
        //}
        //clomesh.vertices = vertices;
        clomesh.RecalculateNormals();
        mesh.sharedMesh = clomesh;
        //mesh.localPosition = new Vector3(tr.localPosition.x, yOffset, tr.localPosition.z);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
