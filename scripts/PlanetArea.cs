using Godot;

public partial class PlanetArea : Area2D
{
	public Vector2 GravitationalEffect(Player player)
	{
		Vector2 toPlanet = (GlobalPosition - player.GlobalPosition).Normalized();
		return new Vector2(-toPlanet.Y, toPlanet.X);
	}

	private void OnAreaEntered(Area2D area)
	{
		if (!(area is Player))
		{
			return;
		}
		GD.Print("Enter!");
		var player = area as Player;
		player.EnterPlanetaryInfluence(this);
	}

	private void OnAreaExited(Area2D area)
	{
		if (!(area is Player))
		{
			return;
		}
		GD.Print("Exit!");
		var player = area as Player;
		player.ExitPlanetaryInfluence(this);
	}
}
