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

public class FlashCardManager : WordManager
{
	public List<GameObject> m_characters = new List<GameObject>();

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
		DisplayNextCard();
	}
	
	/*****************************************************************************/
	/*
		Description:
			Show the next word.
			
		Parameters:
			- none
				
		Return:
			- none
	*/
	/*****************************************************************************/
	public void DisplayNextCard()
	{
		foreach(GameObject obj in m_current)
		{
			Destroy(obj);
		}
		m_current.Clear();

		int rando = Random.Range(0, m_characters.Count);

		GameObject newCharacter = Instantiate(m_characters[rando], transform.position, Quaternion.identity) as GameObject;
		m_current.Add(newCharacter);
		m_characters.RemoveAt(rando);

		StartCoroutine(PlayPhrase());
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
	public override void OnGUI()
	{
		base.OnGUI();

		if(GUI.Button(new Rect(Screen.width / 2, Screen.height - 100, 100, 100), "New Word"))
		{
			DisplayNextCard();
		}
	}
}
