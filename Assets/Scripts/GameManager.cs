using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	public static GameManager instance;

	Dictionary<string, int> stars = new Dictionary<string, int>();

	[SerializeField]
	private Text starCountText;

	// Use this for initialization
	void Start()
	{
		instance = this;
	}

	public void InitStars()
	{
		for (int i = 0; i < 10; i++)
		{
			string starName = "star_" + i.ToString();
			if (PlayerPrefs.HasKey(starName))
			{
				stars[starName] = PlayerPrefs.GetInt(starName);
				starCountText.text = "x " + i.ToString("00");
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
		int starNum = int.Parse(name.Remove(0, 5));
		starCountText.text = "x " + starNum.ToString("00");
	}

	//You can use this to clear progress
	public void ClearStars()
	{
		for (int i = 0; i < 10; i++) 
		{
			string refString = "star_" + i;
			PlayerPrefs.SetInt(refString, 0);
		}
		// PlayerPrefs.SetInt("star_0", 0);
		// PlayerPrefs.SetInt("star_1", 0);
		// PlayerPrefs.SetInt("star_2", 0);
		// PlayerPrefs.SetInt("star_3", 0);
		// PlayerPrefs.SetInt("star_4", 0);
		// PlayerPrefs.SetInt("star_5", 0);
		// PlayerPrefs.SetInt("star_6", 0);
		// PlayerPrefs.SetInt("star_7", 0);
		// PlayerPrefs.SetInt("star_8", 0);
		// PlayerPrefs.SetInt("star_9", 0);
		starCountText.text = "x 00";
	}
}
