using Godot;
using System;

public partial class Boom : Node2D
{
	Area2D area2D;
	AnimatedSprite2D sprite2D;
	private double AliveTime = 8/12.0;
	public override void _Ready()
	{
		sprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		sprite2D.Play();
		area2D = GetNode<Area2D>("AnimatedSprite2D/Area2D");
		area2D.AreaEntered += Area2DOnAreaEntered;
	}

	private void Area2DOnAreaEntered(Area2D area)
	{
		if (area.GetParent() is People ppl)
		{
			ppl.TakeDamage(120);
			QueueFree();
		}
	}

	public override void _Process(double delta)
	{
		AliveTime -= delta;
		if (AliveTime <= 0) QueueFree();
	}
}
