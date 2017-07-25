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

public class Word : MonoBehaviour
{
	public string m_character;			// this word's chinese character
	 
	public GrammarType m_grammarType;
	public int m_category;

	public MeasureWord m_measureWord;
	public bool m_randomize; 
	public bool m_plural;				// can this word be pluralized
	public bool m_possessive;			// can this word possess something
	public bool m_onlyUseShi;			// does this word only work with the verb shi
	public bool m_descriptor;			// is this word followed by an adjective instead of a noun
	public bool m_noProgressZai;		// does this word exclude the zai being used for in progress
	public bool m_noNegative;			// does this work prevent any negatives in the sentence
	public bool m_noAmount;				// does this word prevent an amount to be specified
	public int m_complete;				// can this word indicate an action's completeness 0 - no, 1 - possibly, 2 - always complete
	public GameObject m_customNegative;	// does this word have a custom negative instead of bu

	public int m_tone;

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
	void Awake()
	{
		if(m_randomize == true)
		{
			RandomizeCategory();
		}

		if(m_descriptor == true)
		{
			m_category = 200;
		}
	}
	
	/*****************************************************************************/
	/*
		Description:
			Choose a random category for certain Actions.
			
		Parameters:
			- none
				
		Return:
			- none
	*/
	/*****************************************************************************/
	void RandomizeCategory()
	{
		m_category = Random.Range(0, 4);
	}
}

public enum GrammarType
{
	Pronoun,
	Verb,
	Adverb,
	Noun,
	Number,
	Adjective,
	MeasureWord,
	Particle,
	Time,
	Specifier,
	Conjunction
};

public enum MeasureWord
{
	NONE,
	个,
	杯,
	盘,
	只
};
