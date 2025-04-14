using Godot;
using System.Collections.Generic;

public partial class Player : Area2D
{
	[Export]
	public float ThrustStrength { get; set; } = 100f;
	[Export]
	public float ThrustAcceleration { get; set; } = 5f;
	[Export]
	public float RotationTorque { get; set; } = 1f;

	private Vector2 Velocity = Vector2.Zero;
	private float angularVelocity = 0f;
	private float thrustAmount = 0f;
	private readonly List<PlanetArea> _influencedBy = [];
	private HUD _hud;

	public override void _Ready()
	{
		GD.Print("Rocket ready!");
		_hud = GetNode<HUD>("/root/Main/HUD");
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
		{
			angularVelocity -= RotationTorque * (float)delta;
		}

		if (Input.IsActionPressed("rotate_right"))
		{
			angularVelocity += RotationTorque * (float)delta;
		}

		// Apply rotation
		Rotation += angularVelocity * (float)delta;

		// Thrust
		float targetThrust = Input.IsActionPressed("thrust") ? ThrustStrength : 0f;
		Vector2 thrustDirection = new Vector2(1, 0).Rotated(Rotation);
		thrustAmount = Mathf.Lerp(thrustAmount, targetThrust, ThrustAcceleration * (float)delta);
		Velocity += thrustDirection * thrustAmount * (float)delta;

		// Gravity
		foreach (PlanetArea planet in _influencedBy)
		{
			Vector2 dir = (planet.GlobalPosition - GlobalPosition).Normalized();
			float dist = GlobalPosition.DistanceTo(planet.GlobalPosition);
			float gravity = planet.GravityStrength / Mathf.Max(dist * dist, 1);

			Velocity += dir * gravity * (float)delta;
		}

		// Apply velocity
		GlobalPosition += Velocity * (float)delta;

		// Update display
		_hud.UpdateHUD(thrustAmount, ThrustStrength, Velocity);
	}

}
