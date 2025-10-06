using Godot;
using System;

public partial class Laser : Node2D
{
	Area2D area2D;
	Sprite2D sprite2D;
	private double AliveTime = 3;
	public override void _Ready()
	{
		sprite2D = GetNode<Sprite2D>("Laser");
		area2D = GetNode<Area2D>("Laser/Area2D");
		area2D.AreaEntered += Area2DOnAreaEntered;
	}

	private void Area2DOnAreaEntered(Area2D area)
	{
		if (area.GetParent() is People ppl) {
			ppl.Hp -= 13;
			ppl.TakeDamage(17);
		}
	}

	public override void _Process(double delta)
	{
		AliveTime -= delta;
		if (AliveTime <= 0) QueueFree();
		sprite2D.Position += Vector2.Right * 980*(float)delta;
	}
}
