using System.Collections.Generic;

using System;
using Godot;
public partial class BrainRoted:Node2D 
{
    [Export]protected Timer _attackTimer;
    [Export]public float MaxHp { get; set; }
    public float Hp { get; set; } 
    public virtual float Cost  { get; set; }
    
    public List<Upgrade> Upgrades { get; set; }
    
    protected List<People> _targets = new();
    
    [Export] protected Area2D area2D;

    public override void _Ready()
    {
        Hp = MaxHp;
        _attackTimer = GetNode<Timer>("Timer");

        area2D.AreaEntered += EreaEntered;
        area2D.AreaExited += OnBodyExited;
    }

    public void TakeDamage(float damage)
    {
        GD.Print("Taking damage: "+damage);
        Hp -= damage;
        if (Hp <= 0) QueueFree();
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
    
    protected virtual void TimerCheck()
    {
        if (_targets.Count > 0)_attackTimer.Start();
        else _attackTimer.Stop();
    }

    private void OnBodyExited(Area2D body)
    {
        if (body.GetParent() is People enemy)
        {
            _targets.Remove(enemy);
        }
    }
    
}