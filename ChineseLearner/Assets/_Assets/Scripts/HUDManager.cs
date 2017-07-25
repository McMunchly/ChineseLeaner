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
using UnityEngine.SceneManagement;

public class HUDManager : MonoBehaviour
{
	public GameObject m_pinyinBlock;
	public GameObject m_scriptBlock;

	public WordManager m_manager;

	public bool m_lockBlockers;

	/*****************************************************************************/
	/*
		Description:
			Display HUD.

		Parameters:
			- _character: should the scriptBLocker be on or off
			- _pinyin: should the pinyin blocker be on or off

		Return:
			- none
	*/
	/*****************************************************************************/
	public void SetHUD(bool _character, bool _pinyin)
	{
		if(m_lockBlockers == false)
		{
			m_scriptBlock.SetActive(_character);
			m_pinyinBlock.SetActive(_pinyin);
		}
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
	void OnGUI()
	{
		if(GUI.Button(new Rect(0, Screen.height - 100, 100, 100), "Block Pinyin"))
		{
			SetHUD(m_scriptBlock.activeInHierarchy, !m_pinyinBlock.activeInHierarchy);
		}

		if(GUI.Button(new Rect(0, Screen.height - 200, 100, 100), "Block Script"))
		{
			SetHUD(!m_scriptBlock.activeInHierarchy, m_pinyinBlock.activeInHierarchy);
		}

		if(GUI.Button(new Rect(0, Screen.height / 2 - Screen.height / 4, 100, 100), "Return to Menu"))
		{
			SceneManager.LoadScene(0);
		}

		if(GUI.Button(new Rect(0, Screen.height - 300, 100, 50), "Lock: " + m_lockBlockers))
		{
			m_lockBlockers = !m_lockBlockers;
		}

		if(GUI.Button(new Rect(Screen.width - 100, Screen.height - 150, 100, 50), "Delete Phrases"))
		{
			PlayerPrefs.DeleteAll();
		}
	}
}
