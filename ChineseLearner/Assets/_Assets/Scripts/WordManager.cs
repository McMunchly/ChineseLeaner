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

public class WordManager : MonoBehaviour
{
	public HUDManager m_hudManager;

	public List<GameObject> m_current = new List<GameObject>();

	public int m_questions;
	public int m_numberCorrect;

	/*****************************************************************************/
	/*
		Description:
			Play audio of current phrase.

		Parameters:
			- _save: should the phrase be saved to player prefs

		Return:
			- none
	*/
	/*****************************************************************************/
	public IEnumerator PlayPhrase(bool _save = false)
	{
		string phrase = "";
		string hanPhrase = "";

		int phraseNum = PlayerPrefs.GetInt("phraseCount");

		for(int i = 0; i < m_current.Count; i++)
		{              
			m_current[i].GetComponent<AudioSource>().Play();
			yield return new WaitForSeconds(m_current[i].GetComponent<AudioSource>().clip.length);
			//print(m_current[i].GetComponent<AudioSource>().clip.name);

			string str = m_current[i].name;
			string[] strArr;

			strArr = str.Split(new char[]{'_'});
			//print(strArr[0]);
			phrase += strArr[0] + " ";

			hanPhrase += m_current[i].GetComponent<Word>().m_character;

		}

		if(_save == true)
		{
			PlayerPrefs.SetString("Phrase" + phraseNum, "\n" + phrase + "\n" + hanPhrase + "\n");
			//PlayerPrefs.SetString("HanPhrase" + phraseNum, hanPhrase);
			PlayerPrefs.SetInt("phraseCount", phraseNum + 1);
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
	void Start()
	{
	
	}
	
	/*****************************************************************************/
	/*
	Description:
	Display HUD.

	Parameters:
	- none

	Return:
	- none
	*/
	/*****************************************************************************/
	public virtual void OnGUI()
	{
		if(GUI.Button(new Rect(Screen.width / 4, Screen.height - 100, 100, 100), "Spoken"))
		{
			StartCoroutine(PlayPhrase());
		}
	}
}
