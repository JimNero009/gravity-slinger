using Godot;
using System;
using System.Collections.Generic;

public partial class Player : Area2D
{
	[Export]
	public int Speed { get; set; } = 100;

	private List<PlanetArea> _influencedBy = new List<PlanetArea>();

	public override void _Ready()
	{
		GD.Print("Rocket ready!");
	}

	public void EnterPlanetaryInfluence(PlanetArea planet)
	{
		_influencedBy.Add(planet);
	}

	public void ExitPlanetaryInfluence(PlanetArea planet)
	{
		// TODO: move to hashmap to optimise this?
		_influencedBy.Remove(planet);
	}


	public override void _Process(double delta)
	{
		var velocity = Vector2.Zero;

		foreach (var planet in _influencedBy)
		{
			velocity += planet.GravitationalEffect(this);
		}

		if (Input.IsActionPressed("thrust"))
		{
			velocity.X += 1;
		}

		if (velocity.Length() > 0)
		{
			velocity = velocity.Normalized() * Speed;
		}

		Position += velocity * (float)delta;
	}
}
