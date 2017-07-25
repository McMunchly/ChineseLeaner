/***************************************************************************************/
/*
	Author:		Samuel Dassler
	Email:   	sammyd@samueldassler.com
	
	Description:
		Base class for all characters.
*/        	 
/***************************************************************************************/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Painter : MonoBehaviour
{
	public List<GameObject> m_paint = new List<GameObject>();
	public GameObject m_paintBlot;

	/*****************************************************************************/
	/*
		Description:
			Remove all paint blots.
			
		Parameters:
			- none
				
		Return:
			- none
	*/
	/*****************************************************************************/
	public void ClearPaint()
	{
		foreach(GameObject obj in m_paint)
		{
			Destroy(obj);
		}

		m_paint.Clear();
	}
	
	/*****************************************************************************/
	/*
		Description:
			Use this for initialization.
			
		Parameters:
			- none
				
		Return:
			- none
	*/
	/*****************************************************************************/
	void FixedUpdate()
	{
		if(Input.GetMouseButton(0))
		{
			m_paint.Add(Instantiate(m_paintBlot, Camera.main.ScreenToWorldPoint(Input.mousePosition) + Vector3.forward * 8, Quaternion.identity) as GameObject);
		}
	}

	/*****************************************************************************/
	/*
		Description:
			Use this for initialization.

		Parameters:
			- none

		Return:
			- none
	*/
	/*****************************************************************************/
	public void OnGUI()
	{
		if(GUI.Button(new Rect(Screen.width / 3, Screen.height - 100, 100, 100), "Clear Paint"))
		{
			ClearPaint();
		}
	}
}
