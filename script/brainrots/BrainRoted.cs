using System.Collections.Generic;

using System;
using Godot;
public abstract partial class BrainRoted:Node2D 
{
    public float MaxHp { get; set; }
    public float Hp { get; set; } 
    public float Cost  { get; set; }
    
    public List<Upgrade> Upgrades { get; set; }

    public void TakeDamage(float damage)
    {
        Hp -= damage;
        if (Hp <= 0) QueueFree();
    }
    
}