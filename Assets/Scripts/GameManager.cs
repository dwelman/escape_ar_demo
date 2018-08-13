using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	Dictionary<string, int> stars = new Dictionary<string, int>();
	// Use this for initialization
	void Start()
	{

	}

	public void InitStars()
	{
		for (int i = 0; i < 10; i++)
		{
			string starName = "star_" + i.ToString();
			if (PlayerPrefs.HasKey(starName))
			{
				stars[starName] = PlayerPrefs.GetInt(starName);
			}
			else
			{
				PlayerPrefs.SetInt(starName, 0);
			}
		}
	}

	public void AddStar(string name)
	{
		PlayerPrefs.SetInt(name, 1);
	}

	//You can use this to clear progress
	public void ClearStars()
	{
		PlayerPrefs.SetInt("star_0", 0);
		PlayerPrefs.SetInt("star_1", 0);
		PlayerPrefs.SetInt("star_2", 0);
		PlayerPrefs.SetInt("star_3", 0);
		PlayerPrefs.SetInt("star_4", 0);
		PlayerPrefs.SetInt("star_5", 0);
		PlayerPrefs.SetInt("star_6", 0);
		PlayerPrefs.SetInt("star_7", 0);
		PlayerPrefs.SetInt("star_8", 0);
		PlayerPrefs.SetInt("star_9", 0);
	}
}
