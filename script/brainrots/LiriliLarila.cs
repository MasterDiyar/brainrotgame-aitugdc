using Godot;
using System;
using System.Collections.Generic;

public partial class LiriliLarila : BrainRoted
{
	Area2D area2D;
	
	private List<People> _targets = new();
	private Timer _attackTimer;
	
	private AnimatedSprite2D _icon;
	
	private Tween _tween;
	
	[Export] public float AttackRate = 1f;
	[Export] public float JumpHeight = 20f; 
	public override void _Ready()
	{
		_tween = GetTree().CreateTween();
		_tween.Kill();
		
		_icon = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		_icon.Play();
		
		area2D = GetNode<Area2D>("Area2D");
		area2D.AreaEntered += EreaEntered;
		area2D.AreaExited += OnBodyExited;
		
		_attackTimer = GetNode<Timer>("Timer");
		_attackTimer.Timeout += Attack;
		_attackTimer.WaitTime = AttackRate;
	}

	void TimerCheck()
	{
		if (_targets.Count > 0)_attackTimer.Start();
        		else _attackTimer.Stop();
	}

	public virtual void Attack()
	{
		_tween = GetTree().CreateTween();
		Vector2 startPos = _icon.Position;
		Vector2 upPos = startPos + new Vector2(0, -JumpHeight);
		
		_tween.TweenProperty(_icon, "position", upPos, AttackRate / 4).SetTrans(Tween.TransitionType.Sine).SetEase(Tween.EaseType.Out);
		// вниз за вторую половину
		_tween.TweenProperty(_icon, "position", startPos, AttackRate / 4).SetTrans(Tween.TransitionType.Sine).SetEase(Tween.EaseType.In);

		// в конце наносим урон
		_tween.Finished += DealDamage;
	}

	public void DealDamage()
	{
		var sharp = GD.Load<PackedScene>("res://scene/br/ljump.tscn").Instantiate<Ljump>();
		sharp.Position = _icon.GlobalPosition+Vector2.Down*120+Vector2.Right*64;
		GetParent().AddChild(sharp);
		TimerCheck();
	}


	
	private void EreaEntered(Area2D area)
	{
		if (area.GetParent() is People enemy)
		{
			if (!_targets.Contains(enemy))
			{
				_targets.Add(enemy);
				TimerCheck();
				enemy.TreeExited += () => _targets.Remove(enemy);
			}
		}
	}

	private void OnBodyExited(Area2D body)
	{
		if (body.GetParent() is People enemy)
		{
			_targets.Remove(enemy);
		}
	}
}
