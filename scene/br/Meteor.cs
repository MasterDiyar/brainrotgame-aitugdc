using Godot;
using System;

public partial class Meteor : Node2D
{
	Area2D area2D;
	Sprite2D sprite2D;
	private double AliveTime =2;
	public Vector2 NormAngle;
	public override void _Ready()
	{
		sprite2D = GetNode<Sprite2D>("Meteors2");
		area2D = GetNode<Area2D>("Meteors2/Area2D");
		area2D.AreaEntered += Area2DOnAreaEntered;
	}

	private void Area2DOnAreaEntered(Area2D area)
	{
		if (area.GetParent() is People ppl)
		{
			ppl.TakeDamage(7);
			
		}
	}

	public override void _Process(double delta)
	{
		AliveTime -= delta;
		if (AliveTime <= 0) QueueFree();
		sprite2D.Position += NormAngle * 800*(float)delta;
	}
}
