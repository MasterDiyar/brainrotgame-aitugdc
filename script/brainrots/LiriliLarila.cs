using Godot;
using System;
using System.Collections.Generic;

public partial class LiriliLarila : BrainRoted
{
	private AnimatedSprite2D _icon;
	
	private Tween _tween;
	
	[Export] public float AttackRate = 3f;
	[Export] public float JumpHeight = 60f;

	[Export] public override float Cost { get; set; } = 50;
	public override void _Ready()
	{
		base._Ready();
		Cost = 50;
		
		_tween = GetTree().CreateTween();
		_tween.Kill();
		
		_icon = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		_icon.Play();
		
		_attackTimer.Timeout += Attack;
		_attackTimer.WaitTime = AttackRate;
	}

	public virtual void Attack()
	{
		_tween = GetTree().CreateTween();
		Vector2 startPos = _icon.Position;
		Vector2 upPos = startPos + new Vector2(0, -JumpHeight);
		
		_tween.TweenProperty(_icon, "position", upPos, AttackRate / 4).SetTrans(Tween.TransitionType.Sine).SetEase(Tween.EaseType.Out);
		_tween.TweenProperty(_icon, "position", startPos, AttackRate / 4).SetTrans(Tween.TransitionType.Sine).SetEase(Tween.EaseType.In);


		_tween.Finished += DealDamage;
	}

	public void DealDamage()
	{
		var sharp = GD.Load<PackedScene>("res://scene/br/ljump.tscn").Instantiate<Ljump>();
		sharp.Position = _icon.GlobalPosition+Vector2.Down*120+Vector2.Right*64;
		GetParent().AddChild(sharp);
		TimerCheck();
	}
}
