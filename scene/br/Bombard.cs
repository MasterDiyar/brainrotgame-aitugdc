using Godot;
using System;

public partial class Bombard : BrainRoted
{
	private AnimatedSprite2D _icon;
	[Export] public override float Cost { get; set; }= 45;
	[Export] public float AttackRate = 1f;
	public override void _Ready()
	{
		base._Ready();
		
		_icon = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		_icon.Play();
		
		_attackTimer.Timeout += DealDamage;
		_attackTimer.WaitTime = AttackRate;
	}

	public override void _Process(double delta)
	{
		base._Process(delta);
		Position += (float)delta*Vector2.Right * 40;
	}


	public void DealDamage()
	{
		var sharp = GD.Load<PackedScene>("res://scene/br/boom.tscn").Instantiate<Boom>();
		sharp.Position = _icon.GlobalPosition+Vector2.One*64;
		GetParent().AddChild(sharp);
		QueueFree();
	}
}
