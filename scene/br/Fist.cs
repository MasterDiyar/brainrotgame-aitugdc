using Godot;
using System;

public partial class Fist : Node2D
{
	Area2D area2D;
	Sprite2D sprite2D;
	private double AliveTime = 0.7f;
	public override void _Ready()
	{
		sprite2D = GetNode<Sprite2D>("Fist");
		area2D = GetNode<Area2D>("Fist/Area2D");
		area2D.AreaEntered += Area2DOnAreaEntered;
	}

	private void Area2DOnAreaEntered(Area2D area)
	{
		if (area.GetParent() is People ppl)
		{
			ppl.TakeDamage(17);
			ppl.Speed -= (ppl.Speed > 4) ? 3: 0;
			QueueFree();
			
		}
	}

	public override void _Process(double delta)
	{
		AliveTime -= delta;
		if (AliveTime <= 0) QueueFree();
		sprite2D.Position += Vector2.Right * 110*(float)delta;
		
	}
}
