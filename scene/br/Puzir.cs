using Godot;
using System;

public partial class Puzir : Node2D
{
	Area2D area2D;
	Sprite2D sprite2D;
	private double AliveTime = 2;
	public override void _Ready()
	{
		sprite2D = GetNode<Sprite2D>("Puzir");
		area2D = GetNode<Area2D>("Puzir/Area2D");
		area2D.AreaEntered += Area2DOnAreaEntered;
	}

	private void Area2DOnAreaEntered(Area2D area)
	{
		if (area.GetParent() is People ppl)
		{
			ppl.TakeDamage(11);
			ppl.Position += Vector2.Right * 5f;
			QueueFree();
			
		}
	}

	public override void _Process(double delta)
	{
		AliveTime -= delta;
		if (AliveTime <= 0) QueueFree();
		sprite2D.Position += Vector2.Right * 600*(float)delta;
	}
}
