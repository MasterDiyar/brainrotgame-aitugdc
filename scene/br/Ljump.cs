using Godot;
using System;

public partial class Ljump : Node2D
{
	Area2D area2D;
	Sprite2D sprite2D;
	public double AliveTime = 1;
	public float damage = 16;
	public override void _Ready()
	{
		sprite2D = GetNode<Sprite2D>("Lirilarijump");
		area2D = GetNode<Area2D>("Lirilarijump/Area2D");
		area2D.AreaEntered += Area2DOnAreaEntered;
	}

	private void Area2DOnAreaEntered(Area2D area)
	{
		if (area.GetParent() is People ppl)
		{
			ppl.TakeDamage(damage);
			
		}
	}

	public override void _Process(double delta)
	{
		AliveTime -= delta;
		if (AliveTime <= 0) QueueFree();
		sprite2D.Scale += (float)delta* Vector2.One;
		sprite2D.Modulate = new Color(1, 1, 1, sprite2D.Modulate.A - (float)delta);
	}
}
