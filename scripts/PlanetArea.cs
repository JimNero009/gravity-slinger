using Godot;

public partial class PlanetArea : Area2D
{
	[Export]
	public float GravityStrength { get; set; } = 100000f;

	private void OnAreaEntered(Area2D area)
	{
		if (area is not Player)
		{
			return;
		}
		GD.Print("Enter!");
		var player = area as Player;
		player.EnterPlanetaryInfluence(this);
	}

	private void OnAreaExited(Area2D area)
	{
		if (area is not Player)
		{
			return;
		}
		GD.Print("Exit!");
		var player = area as Player;
		player.ExitPlanetaryInfluence(this);
	}
}
