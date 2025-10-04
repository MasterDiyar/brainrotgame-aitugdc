using Godot;
using System;

public partial class Snezhok : Node2D
{
	Area2D area2D;
	Sprite2D sprite2D;
	private double AliveTime = 1;
	public override void _Ready()
	{
		sprite2D = GetNode<Sprite2D>("Snow");
		area2D = GetNode<Area2D>("Snow/Area2D");
		area2D.AreaEntered += Area2DOnAreaEntered;
	}

	private void Area2DOnAreaEntered(Area2D area)
	{
		if (area.GetParent() is People ppl)
		{
			ppl.TakeDamage(6);
			ppl.Speed -= (ppl.Speed > 4) ? 1: 0;
			ppl.Modulate = new Color(ppl.Modulate.R-0.05f, ppl.Modulate.G-0.05f, 1, 1);
			
		}
	}

	public override void _Process(double delta)
	{
		AliveTime -= delta;
		if (AliveTime <= 0) QueueFree();
		sprite2D.Position += Vector2.Right * 600*(float)delta;
	}
}
