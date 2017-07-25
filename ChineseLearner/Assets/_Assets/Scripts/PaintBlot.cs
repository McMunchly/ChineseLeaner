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

public class PaintBlot : MonoBehaviour
{
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
	void Start()
	{

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
	void OnMouseOver()
	{
		if(Input.GetAxis("Fire2") != 0.0f)
		{
			Destroy(gameObject);
		}
	}
}
