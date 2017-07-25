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

public class PaintManager : FlashCardManager
{
	public Painter m_painter;

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
	public override void OnGUI()
	{
		base.OnGUI();

		if(GUI.Button(new Rect(Screen.width / 2, Screen.height - 100, 100, 100), "New Word"))
		{
			DisplayNextCard();
			m_painter.ClearPaint();
		}
	}
}
