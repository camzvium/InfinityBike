﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;


[CreateAssetMenu(fileName = "TrackNode", menuName = "TrackNode", order = 1)]
public class TrackNode : ScriptableObject 
{
    public bool isLoopOpen;
    public List<Vector3> nodeList = new List<Vector3>();

    public void DeleteNode(int index)
    {
        if (GetNodeCount() > 0)
        {nodeList.RemoveAt(index);}
    }

    public void InsertNode(Vector3 toadd, int indexAhead)
    {
        /*
        if (indexAhead < 0)
        {
            indexAhead += nodeList.Count;
        }
        else if (indexAhead > nodeList.Count)
        {
            indexAhead -= nodeList.Count;
        }
        */

        ClampIndex(ref indexAhead);
        nodeList.Insert(indexAhead, toadd);
    }

    public void AddNode (Vector3 toadd)
	{

        RaycastHit hit;
        if (Physics.Raycast(toadd, Vector3.down, out hit, 100f))
        { nodeList.Add(hit.point + Vector3.up * 1.5f); }
        else if(Physics.Raycast(toadd, Vector3.up, out hit, 100f))
        { nodeList.Add(hit.point + Vector3.up * 1.5f); }


    }	
	
	public void SetNode (Vector3 toadd, int index)
	{	
		if(nodeList.Count != 0)
		{
            nodeList[index] = toadd;
		}	
	}

	public Vector3 GetNode (int index)
	{

		if (GetNodeCount () == 0) 
		{return Vector3.zero;}

        ClampIndex(ref index);
        return nodeList[index];
	}   

	public int GetNodeCount()
	{return nodeList.Count;}

    public void Save(string fileName)
    {
        Vector3Serialisable nodeListSerialisable = new Vector3Serialisable(nodeList, isLoopOpen);
        SaveLoad<Vector3Serialisable>.Save(nodeListSerialisable, fileName);
    }

    public void LoadFile(string fileName)
    {   
        Vector3Serialisable nodeListSerialisable = new Vector3Serialisable();
        SaveLoad<Vector3Serialisable>.Load(out nodeListSerialisable, fileName);
        nodeListSerialisable.SetValuesToNodeList(out nodeList, out isLoopOpen);
    }

    public void ClampIndex( ref int index)
    {
        if (isLoopOpen)
        {
            if (index >= nodeList.Count)
            { index = nodeList.Count - 1; }

            if (index < 0)
            { index = 0; }
        }
        else
        {
            while (index >= nodeList.Count)
            { index -= nodeList.Count; }
            while (index < 0)
            { index += nodeList.Count; }
        }
    }   

}

[Serializable]
public class Vector3Serialisable
{
    bool isLoopOpen;
    public List<float> x;
    public List<float> y;
    public List<float> z;
    
    public Vector3Serialisable(List<Vector3> vec, bool isLoopOpen)
    {
        x = new List<float>();
        y = new List<float>();
        z = new List<float>();

        foreach (Vector3 item in vec)
        {
            x.Add(item.x);
            y.Add(item.y);
            z.Add(item.z);
        }
        this.isLoopOpen = isLoopOpen;

    }
    public Vector3Serialisable()
    {
        x = new List<float>();
        y = new List<float>();
        z = new List<float>();
        this.isLoopOpen = false;
    }

    public void SetValuesToNodeList(out List<Vector3> nodeList, out bool isLoopOpen)
    {
        nodeList = new List<Vector3>();
        for (int i = 0; i < x.Count; i++)
        { nodeList.Add(new Vector3(x[i], y[i], z[i])); }
        isLoopOpen = this.isLoopOpen;
    }

}

[Serializable]
public class TrackNodeValues
{
    public bool isLoopOpen;
    public List<Vector3> nodeList = new List<Vector3>();

    public void Save(string fileName)
    {
        Vector3Serialisable nodeListSerialisable = new Vector3Serialisable(nodeList, isLoopOpen);
        SaveLoad<Vector3Serialisable>.Save(nodeListSerialisable, fileName);
    }

    public void LoadFile(string fileName)
    {
        Vector3Serialisable nodeListSerialisable = new Vector3Serialisable();
        SaveLoad<Vector3Serialisable>.Load(out nodeListSerialisable, fileName);
        nodeListSerialisable.SetValuesToNodeList(out nodeList, out isLoopOpen);
    }

}   