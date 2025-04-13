using Godot;
using System;
using System.Collections.Generic;

public partial class Player : Area2D
{
	[Export]
	public float ThrustStrength { get; set; } = 100f;
	[Export]
	public float RotationSpeed = 2.0f;
	public Vector2 Velocity = Vector2.Zero;

	private readonly List<PlanetArea> _influencedBy = [];

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
		// Rotation
		if (Input.IsActionPressed("rotate_left"))
			Rotation -= RotationSpeed * (float)delta;

		if (Input.IsActionPressed("rotate_right"))
			Rotation += RotationSpeed * (float)delta;

		// Thrust
		if (Input.IsActionPressed("thrust"))
		{
			Vector2 thrustDirection = new Vector2(1, 0).Rotated(Rotation);
			Velocity += thrustDirection * ThrustStrength * (float)delta;
		}

		// Gravity
		foreach (PlanetArea planet in _influencedBy)
		{
			Vector2 dir = (planet.GlobalPosition - GlobalPosition).Normalized();
			float dist = GlobalPosition.DistanceTo(planet.GlobalPosition);
			float gravity = planet.GravityStrength / Mathf.Max(dist * dist, 1);

			Velocity += dir * gravity * (float)delta;
		}

		// Result
		GlobalPosition += Velocity * (float)delta;
	}

}
