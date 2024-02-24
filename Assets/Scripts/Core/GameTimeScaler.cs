using UnityEngine;

namespace Core
{
	public class GameTimeScaler
	{
		public void PauseGame()
		{
			Time.timeScale = 0;
		}

		public void ResumeGame()
		{
			Time.timeScale = 1;
		}
	}
}