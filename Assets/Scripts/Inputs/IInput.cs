namespace RunnerMeet.Inputs
{
	public interface IInput
	{
		bool IsJump { get; }

		void CustomUpdate();
	}
}