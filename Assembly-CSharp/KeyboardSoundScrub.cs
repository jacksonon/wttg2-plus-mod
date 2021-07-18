using System;
using UnityEngine;
using UnityEngine.UI;

public class KeyboardSoundScrub : MonoBehaviour
{
	private void processKeySFX()
	{
		int num = Random.Range(1, LookUp.SoundLookUp.KeyboardSounds.Length);
		AudioFileDefinition audioFileDefinition = LookUp.SoundLookUp.KeyboardSounds[num];
		GameManager.AudioSlinger.PlaySound(audioFileDefinition);
		LookUp.SoundLookUp.KeyboardSounds[num] = LookUp.SoundLookUp.KeyboardSounds[num];
		LookUp.SoundLookUp.KeyboardSounds[0] = audioFileDefinition;
	}

	private void processSpaceReturnSFX()
	{
		GameManager.AudioSlinger.PlaySound(LookUp.SoundLookUp.KeyboardSpaceReturnSound);
	}

	private void Awake()
	{
		this.myTextInput = base.GetComponent<InputField>();
	}

	private void Update()
	{
		if (StateManager.PlayerState == PLAYER_STATE.COMPUTER && this.myTextInput.isFocused)
		{
			if (Input.GetKeyDown(32))
			{
				this.processSpaceReturnSFX();
			}
			else if (Input.GetKeyDown(13))
			{
				this.processSpaceReturnSFX();
			}
			else if (Input.anyKeyDown && !Input.GetMouseButtonDown(0) && !Input.GetMouseButtonDown(1) && !Input.GetMouseButtonDown(2))
			{
				this.processKeySFX();
			}
		}
	}

	private InputField myTextInput;
}
