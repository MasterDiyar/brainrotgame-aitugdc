using Godot;
using System;

public partial class Toilet : BrainRoted
{
	private AnimatedSprite2D _icon;
	[Export] public override float Cost { get; set; }= 410;
	[Export] public float AttackRate = 2;
	private bool te = false;
	public override void _Ready()
	{
		base._Ready();
		
		_icon = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		
		_attackTimer.Timeout += TimerCheck;
		_attackTimer.WaitTime = AttackRate;
	}

	protected override void TimerCheck()
	{
		if (_targets.Count > 0) {
			_attackTimer.Start();
			_icon.Play();
		} else {
			_icon.Stop();
			_attackTimer.Stop();
		}
	}

	public override void _Process(double delta)
	{
		if (_icon.Frame == 6)
		{
			DealDamage();
			
		}
	}

	public void DealDamage()
	{
		te = !te;
		if (te) return;
		var sharp = GD.Load<PackedScene>("res://scene/br/laser.tscn").Instantiate<Laser>();
		sharp.Position = _icon.GlobalPosition+Vector2.Down*75+Vector2.Right*90;
		GetParent().AddChild(sharp);
	}
}
