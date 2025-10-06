using Godot;
using System;

public partial class Trippitroppi : BrainRoted
{
	private AnimatedSprite2D _icon;
	[Export] public override float Cost { get; set; }= 170;
	[Export] public float AttackRate = 9/10f;
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
		} else {
			_icon.Stop();
			_attackTimer.Stop();
		}
	}

	public void DealDamage()
	{
		var sharp = GD.Load<PackedScene>("res://scene/br/puzir.tscn").Instantiate<Puzir>();
		sharp.Position = _icon.GlobalPosition+Vector2.Down*25+Vector2.Right*110;
		GetParent().AddChild(sharp);
		TimerCheck();
	}
}
