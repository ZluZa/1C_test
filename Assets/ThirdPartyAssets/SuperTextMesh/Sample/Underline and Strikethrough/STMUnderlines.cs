﻿//Copyright (c) 2017-2021 Kai Clavier [kaiclavier.com] Do Not Distribute
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

[RequireComponent(typeof(SuperTextMesh))]
public class STMUnderlines : MonoBehaviour 
{

	public SuperTextMesh superTextMesh;

	//this HAS to be public or it gets forgotten when scene starts
	[HideInInspector] public List<Image> allStrikethrus = new List<Image>();
	[HideInInspector] public List<Image> allUnderlines = new List<Image>();
	
	[Header("Underline & Strikethru")]
	public GameObject lineUiPrefab;
	[Header("Underline")]
	public Vector3 underlineDistance = new Vector3(0.02f,-0.15f,0f);
	public float underlineWidth = 0.666667f;
	public float underlineThickness = 0.1f;
	[Header("Strikethru")]
	[Range(0f,1f)]
	public float strikethruHeight = 0.3f;
	public float strikethruWidth = 0.666667f;
	public float strikethruThickness = 0.1f;
	public Color strikethruColor = Color.white;

	void Reset()
	{
		superTextMesh = GetComponent<SuperTextMesh>();
	}
	void OnEnable()
	{
		superTextMesh.OnRebuildEvent += ClearUnderlines;
		superTextMesh.OnRebuildEvent += ClearStrikethrus;
		superTextMesh.OnCustomEvent += DoEvent;
	}
	void OnDisable()
	{
		superTextMesh.OnRebuildEvent -= ClearUnderlines;
		superTextMesh.OnRebuildEvent -= ClearStrikethrus;
		superTextMesh.OnCustomEvent -= DoEvent;
	}
	[ContextMenu("DebugReset")]
	public void DebugReset()
	{
		allStrikethrus = new List<Image>();
		allUnderlines = new List<Image>();
	}

	private Vector3 pos;
	private Vector3 rawPos;
	public void DoEvent(string s, STMTextInfo info)
	{
		pos = info.Middle + transform.localPosition;
		rawPos = info.BottomLeftVert + transform.localPosition;

		if(s == "underline")
		{
			Vector3 cornerPos = info.pos + transform.localPosition + new Vector3((info.TopRightVert.x - info.pos.x) / 2f,0f,-0.01f) + underlineDistance; //align to row
//			Debug.Log(cornerPos);
			Image newLine = Instantiate(lineUiPrefab).GetComponent<Image>();
			newLine.transform.SetParent(transform);
			newLine.transform.localPosition = cornerPos;
			//resize to letter
			Vector3 newScale = new Vector3(underlineWidth, underlineThickness, 1f);
			newLine.transform.localScale = newScale;
			STMColorData data;
			if (superTextMesh.data.colors.TryGetValue("rev", out data))
			{
				newLine.color = data.color;
			}
			allUnderlines.Add(newLine);
		}
		else if(s == "strike" || s == "strikethrough")
		{
			Vector3 cornerPos = info.pos + transform.localPosition + new Vector3((info.TopRightVert.x - info.pos.x) / 2f, info.size.y * strikethruHeight, -0.01f); //align to row
			Image newLine = Instantiate(lineUiPrefab).GetComponent<Image>();
			newLine.transform.SetParent(transform);
			newLine.transform.localPosition = cornerPos;
			Vector3 newScale = new Vector3(strikethruWidth, strikethruThickness, 1f);
			newLine.transform.localScale = newScale;
			newLine.color = strikethruColor;
			allStrikethrus.Add(newLine);
		}
	}

	public void ClearUnderlines()//destroy all link objects that were generated
	{ 
		for(int i=0; i<allUnderlines.Count; i++)
		{
			DestroyImmediate(allUnderlines[i].gameObject);
		}
		allUnderlines.Clear();
	}
	public void ClearStrikethrus()//destroy all link objects that were generated
	{ 
		for(int i=0; i<allStrikethrus.Count; i++)
		{
			DestroyImmediate(allStrikethrus[i].gameObject);
		}
		allStrikethrus.Clear();
	}
}
