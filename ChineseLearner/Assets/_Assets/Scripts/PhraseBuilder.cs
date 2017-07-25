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

public enum PhraseType
{
	Basic,
	Shenme,
	XVerbX,
	XAdjX
}
public class PhraseBuilder : WordManager
{
	public int m_numberCeiling;	// the maximum amount for numbers;
	public bool m_useWeek;
	public bool m_useDay;
	public bool m_useTime;

	public GameObject[] m_specifiers;
	public GameObject[] m_pronouns;
	public GameObject[] m_possessed;
	public GameObject[] m_verbs;
	public GameObject[] m_measureWords;
	public GameObject[] m_dates;
	public GameObject[] m_timesOfDay;
	public GameObject[] m_numbers;
	public GameObject[] m_kind;
	public GameObject[] m_food;
	public GameObject[] m_drink;
	public GameObject[] m_animal;
	public GameObject[] m_place;
	public GameObject[] m_song;
	public GameObject[] m_adjective;

	public GameObject m_nian;
	public GameObject m_yue;
	public GameObject m_hao;
	public GameObject m_men;
	public GameObject m_de;
	public GameObject m_bu;
	public GameObject m_yao;
	public GameObject m_ma;
	public GameObject m_ye;
	public GameObject m_zai;
	public GameObject m_le;
	public GameObject m_he;
	public GameObject m_shenme;
	public GameObject m_qian;
	public GameObject m_bai;
	public GameObject m_shi;
	public GameObject m_dian;
	public GameObject m_fen;

	public GameObject m_specifier;
	public GameObject m_subject;
	public GameObject m_time;
	public GameObject m_verb;
	public GameObject m_object;

	/*****************************************************************************/
	/*
		Description:
			Create a new sentence.
			
		Parameters:
			- none
				
		Return:
			- none
	*/
	/*****************************************************************************/
	void MakePhrase()
	{

		ClearPhrase();

		int rando;
		int time = Random.Range(0, 3);

		int pronoun = Random.Range(0, 2);

		if(time == 1)
		{
			ChooseTime();
		}

		ChooseSubject(pronoun);

		// put ye in before verb sometimes to add the word also
		int ye = Random.Range(0, 4);

		if(ye == 1)
		{
			AddCharacter(m_ye);
		}

		ChooseVerb();

		// put zai in before verb sometimes to indicate action in progress
		int zai  = Random.Range(0, 2);

		if(zai == 1 && m_verb.GetComponent<Word>().m_noProgressZai == false)
		{
			AddCharacter(m_zai);
		}

		if(time == 2)
		{
			ChooseTime();
		}

		int yao = Random.Range(0, 2);

		if(zai == 0 && yao == 1)
		{
			AddCharacter(m_yao);
		}

		// choose if the sentence will be in negative form
		int negative = Random.Range(0, 2);

		if(m_verb.GetComponent<Word>().m_noNegative == true)
		{
			negative = 0;
		}

		if(negative == 1)
		{
			NegativeVerb();
		}

		// sentence is not negative so add the verb
		else
		{
			m_current.Add(m_verb);
		}

		// add le for completed action
		if(time != 0 && zai == 0 && ye == 0 && yao == 0 && m_time != null && m_time.GetComponent<Word>().m_complete != 0)
		{
			// grab a random number and add 0 -1 from the word to decide if le should be true or now
			int le = Random.Range(0, 2) + m_time.GetComponent<Word>().m_complete - 1;

			if(le != 0)
			{
				AddCharacter(m_le);
			}
		}
		// choose if the sentence will become a question
		int question = Random.Range(0, 3);

		// negative form prevents any questions
		if(negative == 1)
		{
			question = 0;
		}

		// this is xbux question form
		if(question == 1 && zai == 0)
		{
			if(m_verb.GetComponent<Word>().m_customNegative == null)
			{
				AddCharacter(m_bu);
				AddCharacter(m_verb);
			}
		}

		// choose if the sentence becomes a what question with 'shenme'
		int what = Random.Range(0, 2);

		 if(what == 1 && negative == 0 && zai == 0 && question == 0)
		{
			AddCharacter(m_shenme);
			question = 1;
		}

		// choose a noun
		else
		{
			m_object = ChooseNoun(m_verb.GetComponent<Word>().m_category);

			// choose to add an amount and a measure word
			if(question == 0 && m_object.GetComponent<Word>().m_measureWord != MeasureWord.NONE && m_verb.GetComponent<Word>().m_category != 0 && m_verb.GetComponent<Word>().m_noAmount == false)
			{
				ChooseNumber();
				ChooseMeasureWord(m_object);
			}
			m_object = AddCharacter(m_object);
		}
			
		// question in 'ma' form
		if(question == 2 && negative != 0)
		{
			AddCharacter(m_ma);
		}

		// align the completed phrase in the proper order
		for(int i = 0, j = 0, k = 0; i < m_current.Count; i++, k++)
		{
			if(i % 8 == 0 && i != 0)
			{
				k = 0;
				j++;
			}
			m_current[i].transform.position = transform.position + (transform.right * k * 2.5f) - transform.forward * .05f * i - (transform.up * j * 2.5f);
		}
	
		// speak sentence in order
		StartCoroutine(PlayPhrase(true));
	}

	/*****************************************************************************/
	/*
		Description:
			Pick the subject of the phrase.

		Parameters:
			- _category: which category of noun to pick from.

		Return:
			- The chosen noun.
	*/
	/*****************************************************************************/
	public GameObject ChooseNoun(int _category = -1)
	{
		int rando;
		GameObject obj;
		switch(_category)
		{
			case 0:
				rando = Random.Range(0, m_kind.Length);
				obj = m_kind[rando];
				break;
			case 1:
				rando = Random.Range(0, m_food.Length);
				obj = m_food[rando];
				break;
			case 2:
				rando = Random.Range(0, m_drink.Length);
				obj = m_drink[rando];
				break;
			case 3:
				rando = Random.Range(0, m_animal.Length);
				obj = m_animal[rando];
				break;
			case 4:
				rando = Random.Range(0, m_song.Length);
				obj = m_song[rando];
				break;
			case 5:
				rando = Random.Range(0, m_place.Length);
				obj = m_place[rando];
				break;
			default:
				rando = Random.Range(0, m_adjective.Length);
				obj = m_adjective[rando];
				break;
		}

		return obj;
	}

	/*****************************************************************************/
	/*
		Description:
			Pick the subject of the phrase.

		Parameters:
			- _pronoun: A bool that says if the subject is a pronoun.

		Return:
			- none
	*/
	/*****************************************************************************/
		public void ChooseSubject(int _pronoun)
	{
		if(_pronoun == 1)
		{
			//pick a random pronoun to be the subject
			m_subject = m_pronouns[Random.Range(0, m_pronouns.Length)];
		}

		else
		{
			int specifier = Random.Range(0, 2);

			if(specifier == 1)
			{
				m_specifier = AddCharacter(m_specifiers[Random.Range(0, m_specifiers.Length)]);
			}
				
			//m_subject = ChooseNoun(Random.Range(0, 6));
			m_subject = ChooseNoun(0);

			if(specifier == 1)
			{
				int number = Random.Range(0, 2);

				if(number == 1)
				{
					ChooseNumber();
				}

				ChooseMeasureWord(m_subject);
			}
		}

		AddCharacter(m_subject);

		// choose to add 'men' for plurality
		if(m_subject.GetComponent<Word>().m_plural == true)
		{
			int plural = Random.Range(0, 2);

			if(plural == 1)
			{
				AddCharacter(m_men);
			}
		}

		// choose to add 'de' for possession
		if(m_subject.GetComponent<Word>().m_possessive == true)
		{
			int possession = Random.Range(0, 3);

			if(possession == 1)
			{
				AddCharacter(m_de);

				int rando2 = Random.Range(0, m_possessed.Length);

				AddCharacter(m_possessed[rando2]);
			}
		}
	}

	/*****************************************************************************/
	/*
		Description:
			Specify a time of the phrase.

		Parameters:
			- none

		Return:
			- none
	*/
	/*****************************************************************************/
	public void ChooseTime()
	{
		// specify time of day
		if(m_useTime == true)
		{
			int hour = Random.Range(1, 13);
			bool pm = Random.value < .5f;

			switch(hour)
			{
				case 1: case 2: case 3: case 4: case 5: case 6: case 7:
					if(pm == false)
					{
						AddCharacter(m_timesOfDay[0]);
					}
					else
					{
						AddCharacter(m_timesOfDay[3]);
					}
				break;
				case 8: case 9: case 10: case 11:
					if(pm == false)
					{
						AddCharacter(m_timesOfDay[1]);
					}
					else
					{
						AddCharacter(m_timesOfDay[4]);
					}
				break;
				default:
					AddCharacter(m_timesOfDay[2]);
				break;
			}

			ChooseNumber(hour);
			AddCharacter(m_dian);
		}

		// add month and day
		if(m_useDay == true)
		{
			int year = Random.Range(1990, 2026);
			int month = Random.Range(1, 13);
			int day = Random.Range(1, 31);

			ChooseNumber(year, true);
			AddCharacter(m_nian);
			ChooseNumber(month);
			AddCharacter(m_yue);
			ChooseNumber(day);
			AddCharacter(m_hao);
		}

		// add week
		if(m_useWeek == true)
		{
			AddCharacter(m_dates[Random.Range(0, m_dates.Length)]);
		}
	}

	/*****************************************************************************/
	/*
		Description:
			Pick the action of the phrase.

		Parameters:
			- none

		Return:
			- none
	*/
	/*****************************************************************************/
	public void ChooseVerb()
	{
		int rand = Random.Range(0, m_verbs.Length);

		// certain pronouns like 'zhe' and 'na' only use shi for simplicity's sake
		if(m_subject.GetComponent<Word>().m_onlyUseShi == true)
		{
			rand = 0;
		}

		// pick a random verb
		m_verb = Instantiate(m_verbs[rand], Vector3.zero, Quaternion.identity) as GameObject;
	}

	/*****************************************************************************/
	/*
		Description:
			Pick the action of the phrase.

		Parameters:
			- none

		Return:
			- none
	*/
	/*****************************************************************************/
	public void NegativeVerb()
	{
		// take care of edge cases like 'you-meiyou'
		if(m_verb.GetComponent<Word>().m_customNegative != null)
		{
			m_current.Add(m_verb);
			AddCharacter(m_verb.GetComponent<Word>().m_customNegative);

		}

		// add 'bu' to make most verbs negative
		else
		{
			AddCharacter(m_bu);
			m_current.Add(m_verb);
		}
	}

	/*****************************************************************************/
	/*
		Description:
			Get a random or specified number.

		Parameters:
			- _number: a specific number, otherwise make a random one.

		Return:
			- none
	*/
	/*****************************************************************************/
	public void ChooseNumber(int _number = 0, bool _separateDigits = false)
	{
		
		if(_number == 0)
		{
			_number = Random.Range(1, m_numberCeiling + 1);
		}

		// split the digits up for proper formatting
		int thousands = _number / 1000;
		int hundreds = (_number - (thousands * 1000)) / 100;
		int tens = (_number - (thousands * 1000) - (hundreds * 100)) / 10;
		int ones = _number - (thousands * 1000) - (hundreds * 100) - (tens * 10);

		//print ("WOGOGOGLE  + " + _number + " : " + thousands + "  " + hundreds + "  " + tens + "   " + ones);

		// display each digit by itself
		if(_separateDigits == true)
		{
			AddCharacter(m_numbers[thousands]);
			AddCharacter(m_numbers[hundreds]);
			AddCharacter(m_numbers[tens]);
			AddCharacter(m_numbers[ones]);
		}

		// display as a single number
		else
		{
			if(thousands != 0)
			{
				AddCharacter(m_numbers[thousands]);
				AddCharacter(m_qian);
			}
			if(hundreds != 0)
			{
				AddCharacter(m_numbers[hundreds]);
				AddCharacter(m_bai);
			}

			if(tens != 0)
			{	// special case for ten so it says 'shi' and not 'yi shi'
				if(tens > 1)
				{
					AddCharacter(m_numbers[tens]);
				}

				AddCharacter(m_shi);
			}

			if(ones != 0)
			{
				AddCharacter(m_numbers[ones]);
			}
		}
	}

	/*****************************************************************************/
	/*
		Description:
			Get the correct classifer for the chosen subject.

		Parameters:
			- _subject: Which word to get the measure word from.

		Return:
			- none
	*/
	/*****************************************************************************/
	public void ChooseMeasureWord(GameObject _subject)
	{
		foreach(GameObject obj in m_measureWords)
		{
			if(obj.GetComponent<Word>().m_measureWord == _subject.GetComponent<Word>().m_measureWord)
			{
				AddCharacter(obj);
			}
		}
	}

	public GameObject AddCharacter(GameObject _character)
	{
		GameObject newCharacter = Instantiate(_character, Vector3.zero, Quaternion.identity) as GameObject;
		m_current.Add(newCharacter);

		return newCharacter;
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

		if(GUI.Button(new Rect(Screen.width / 2, Screen.height - 100, 100, 100), "New Phrase"))
		{
			m_hudManager.SetHUD(true, true);
			MakePhrase();
		}
	}

	/*****************************************************************************/
	/*
		Description:
			Clear out the current phrase.

		Parameters:
			- none

		Return:
			- none
	*/
	/*****************************************************************************/
	public void ClearPhrase()
	{
		foreach(GameObject obj in m_current)
		{
			Destroy(obj);
		}
		m_current.Clear();

		m_specifier = null;
		m_subject = null;
		m_time = null;
		m_verb = null;
	}
}
