using Godot;
using System;

public partial class Uchitel :People
{
    private float damagedCount = 1;

    public override void TakeDamage(float amount)
    {
        base.TakeDamage(amount/damagedCount);
        damagedCount += 0.05f;
    }
}

