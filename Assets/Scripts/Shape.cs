using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shape : PersistableObject
{
    MeshRenderer meshRenderer;

    void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    public int ShapeId {
        get { return shapeId; }
        set 
        { 
            if(shapeId == int.MinValue && value != int.MinValue) 
            {
                shapeId = value;
            } else {
                Debug.LogError("Not allowed to change shapeId.");
            
            }
        }
    }
    int shapeId = int.MinValue;

    public int MaterialId { get; private set; }
    public void SetMaterial (Material material, int materialId) {
        meshRenderer.material = material;
        MaterialId = materialId;
    }

    Color color;
    public void SetColor (Color color)
    {
        this.color = color;
        meshRenderer.material.color = color;
    }

    public override void Save (GameDataWriter writer) {
		base.Save(writer);
		writer.Write(color);
	}

	public override void Load (GameDataReader reader) {
		base.Load(reader);
		SetColor(reader.Version > 0 ? reader.ReadColor() : Color.white);
	}
}
