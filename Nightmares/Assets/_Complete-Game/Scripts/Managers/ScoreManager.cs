using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace CompleteProject
{
	public class ScoreManager : MonoBehaviour
	{
		private const string STAT_INFIX_KILL = " KILLED : ";
		public static int score;                // The player's score.
		private static Dictionary<string, int> killStatDictionary;
		Text text;                                      // Reference to the Text component.



		void Awake()
		{
			killStatDictionary = new Dictionary<string, int>();
			// Set up the reference.
			text = GetComponent<Text>();
			// Reset the score.
			score = 0;
		}


		void Update()
		{
			// Set the displayed text to be the word "Score" followed by the score value.
			text.text = "Score: " + score;
		}

		public static void AddKillStat(string statName, int kills = 1)
		{
			if (killStatDictionary.ContainsKey(statName))
			{
				killStatDictionary[statName] += kills;
			}
			else
			{
				killStatDictionary.Add(statName, kills);
			}
		}


		public static string GetKillStatString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (KeyValuePair<string, int> kvp in killStatDictionary)
			{
				stringBuilder.Append(kvp.Key.ToUpper()).Append(STAT_INFIX_KILL).Append(kvp.Value).AppendLine();
			}
			return stringBuilder.ToString();
		}
	}
}