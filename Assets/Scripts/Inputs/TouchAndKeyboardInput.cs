using UI;
using UnityEngine;

namespace RunnerMeet.Inputs
{
	public class TouchAndKeyboardInput : IInput
	{
		private readonly UIChecker _uiChecker;

		public bool IsJump { get; private set; }

		public TouchAndKeyboardInput(UIChecker uiChecker)
		{
			_uiChecker = uiChecker;
		}

		public void CustomUpdate()
		{
			var isJumpByKeyboard = Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.Space);

			var isJumpByTouch =
				Input.touchCount > 0
				&& Input.GetTouch(0).phase == TouchPhase.Began
				&& _uiChecker.CheckPointerOverUI() == false;

			IsJump = isJumpByTouch || isJumpByKeyboard;
		}
	}
}