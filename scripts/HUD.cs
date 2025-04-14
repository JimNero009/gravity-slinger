using Godot;

public partial class HUD : CanvasLayer
{
	private ProgressBar _thrustDial;
	private Label _speedLabel;

	public override void _Ready()
	{
		_thrustDial = GetNode<ProgressBar>("ThrustDial");
		_speedLabel = GetNode<Label>("SpeedLabel");
	}
	public void UpdateHUD(float currentThrust, float maxThrust, Vector2 velocity)
	{
		_thrustDial.MaxValue = maxThrust;
		_thrustDial.Value = currentThrust;

		float speed = velocity.Length();
		_speedLabel.Text = $"Speed: {Mathf.Round(speed)} px/s";
	}
}
