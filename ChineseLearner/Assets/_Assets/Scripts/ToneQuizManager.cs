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

public class ToneQuizManager : WordManager
{
	public List<GameObject> m_characters = new List<GameObject>();

	public int m_currentTone;

	public bool m_result;
	public string m_winner;
	public int rando;

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
	void DisplayNextCard()
	{
		m_result = false;

		foreach(GameObject obj in m_current)
		{
			Destroy(obj);
		}
		m_current.Clear();

		int rando2 = rando;

		// never do the same one twice in a row
		do
		{
			rando = Random.Range(0, m_characters.Count);
		}
		while(rando == rando2);

		GameObject newCharacter = Instantiate(m_characters[rando], Vector3.zero, Quaternion.identity) as GameObject;
		m_current.Add(newCharacter);
		m_currentTone = newCharacter.GetComponent<Word>().m_tone;

		StartCoroutine(PlayPhrase());
	}

	public void CheckAnswer(int _chosenTone)
	{
		m_hudManager.SetHUD(false, false);

		m_questions++;

		if(m_currentTone == _chosenTone)
		{
			m_winner = "CORRECT!";
			m_numberCorrect++;
		}
		else
		{
			m_winner = "WRONG";
		}

		m_result = true;
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

		GUI.Box(new Rect(Screen.width / 2, 0, 100, 25), m_numberCorrect + " / " + m_questions);
		if(m_result == false)
		{
			if(GUI.Button(new Rect(Screen.width / 4, Screen.height - 250, 100, 100), " 0 -Neutral"))
			{
				CheckAnswer(0);
			}
			if(GUI.Button(new Rect(Screen.width / 4 + 150, Screen.height - 250, 100, 100), "1 - Flat"))
			{
				CheckAnswer(1);
			}
			if(GUI.Button(new Rect(Screen.width / 4 + 300, Screen.height - 250, 100, 100), "2 - Rising"))
			{
				CheckAnswer(2);
			}
			if(GUI.Button(new Rect(Screen.width / 4 + 450, Screen.height - 250, 100, 100), "3 - V"))
			{
				CheckAnswer(3);
			}
			if(GUI.Button(new Rect(Screen.width / 4 + 600, Screen.height - 250, 100, 100), "4 - Falling"))
			{
				CheckAnswer(4);
			}
		}

		else
		{
			if(GUI.Button(new Rect(Screen.width / 2, Screen.height - 250, 100, 100), m_winner + " - " + m_currentTone))
			{
				m_hudManager.SetHUD(true, true);
				DisplayNextCard();
			}
		}
	}
}
