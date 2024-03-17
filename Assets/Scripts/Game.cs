using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Game : PersistableObject
{
    public PersistableObject prefab;
    List<PersistableObject> objects;
    public PersistentStorage storage;

    public KeyCode createKey = KeyCode.C;
    public KeyCode newGameKey = KeyCode.N;
    public KeyCode saveGameKey = KeyCode.S;
    public KeyCode loadGameKey = KeyCode.L;


    void Awake() {
        objects = new List<PersistableObject>();    
    }

    void Update()
    {
        if (Input.GetKeyDown(createKey))
        {
            CreateObject();
        } else if (Input.GetKeyDown(newGameKey))
        {
            BeginNewGame();
        } else if (Input.GetKeyDown(saveGameKey))
        {
            storage.Save(this);
        } else if (Input.GetKeyDown(loadGameKey))
        {
            BeginNewGame();
            storage.Load(this);
        }
    }

    void BeginNewGame() 
    {
        for(int i = 0; i < objects.Count; i++)
        {
            Destroy(objects[i].gameObject);
        }
        objects.Clear();
    }

    void CreateObject()
    {
        PersistableObject o = Instantiate(prefab);
        Transform t = o.transform;
        t.localPosition = Random.insideUnitSphere * 5;
        t.localRotation = Random.rotation;
        t.localScale = Vector3.one * Random.Range(0.1f, 1f);
        objects.Add(o);
    }

    public override void Save (GameDataWriter writer) {
		writer.Write(objects.Count);
		for (int i = 0; i < objects.Count; i++) {
			objects[i].Save(writer);
		}
	}

    public override void Load (GameDataReader reader) {
		int count = reader.ReadInt();
		for (int i = 0; i < count; i++) {
			PersistableObject o = Instantiate(prefab);
			o.Load(reader);
			objects.Add(o);
		}
	}
}