using Godot;
using System;
using System.Collections.Generic;

public partial class Frigo : BrainRoted
{
	private AnimatedSprite2D _icon;
	
	[Export] public float AttackRate = 1f;
	[Export] public override float Cost { get; set; } = 120;
	public override void _Ready()
	{
		base._Ready();
		_icon = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		
		_attackTimer.Timeout += DealDamage;
		_attackTimer.WaitTime = AttackRate;
	}

	protected override void TimerCheck()
	{
		if (_targets.Count > 0) {
			_attackTimer.Start();
			_icon.Play();
		}else {
			_icon.Stop();
			_attackTimer.Stop();
		}
	}

	public void DealDamage()
	{
		var sharp = GD.Load<PackedScene>("res://scene/br/snezhok.tscn").Instantiate<Snezhok>();
		sharp.Position = _icon.GlobalPosition+Vector2.Down*35+Vector2.Right*120;
		GetParent().AddChild(sharp);
		TimerCheck();
	}
	
}
