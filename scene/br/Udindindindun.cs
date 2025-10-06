using Godot;
using System;

public partial class Udindindindun : BrainRoted
{
	private AnimatedSprite2D _icon;
	[Export] public override float Cost { get; set; }= 80;
	[Export] public float AttackRate = 1f;
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
		var sharp = GD.Load<PackedScene>("res://scene/br/fist.tscn").Instantiate<Fist>();
		sharp.Position = _icon.GlobalPosition+Vector2.Down*70+Vector2.Right*120;
		GetParent().AddChild(sharp);
		TimerCheck();
	}
}
