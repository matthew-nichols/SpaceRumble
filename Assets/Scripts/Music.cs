using UnityEngine;
using System.Collections;

public class Music : MonoBehaviour
{
		public AudioSource bgm;
		public AudioClip idleMusic;
		public AudioClip battleMusic;
		public bool gameState = false;
		bool currGameState = false;
		bool fadeFinish = true;
		float defaultVolume;

		void Start ()
		{
				defaultVolume = bgm.volume;
				bgm.clip = idleMusic;
		}

		void Update ()
		{
				//if(currGameState != gameState)
				//FadeOutMusic ();

				if (gameState) {
						bgm.clip = battleMusic;
						if (fadeFinish)
								currGameState = gameState;
				} else {
						bgm.clip = idleMusic;
						if (fadeFinish)
								currGameState = gameState;
				}
				if (bgm != null && !bgm.isPlaying && fadeFinish) {
						bgm.volume = defaultVolume;
						bgm.Play ();
				}
		}

		public void FadeOutMusic ()
		{
				StartCoroutine (FadeMusic ());
		}

		IEnumerator FadeMusic ()
		{
				while (bgm.volume > .1F) {
						fadeFinish = false;
						bgm.volume -= bgm.volume * Time.deltaTime; // = Mathf.Lerp(bgm.volume, 0F, Time.deltaTime);
						yield return 0;
				}
				bgm.volume = 0;
				fadeFinish = true;
		}
}
