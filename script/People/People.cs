using Godot;

public partial class People: Node2D
{
    public float Hp = 20;
    [Export] public float MaxHp = 80;
    [Export] public float Speed = 20;
    [Export] public float Damage = 30;
    [Export] public Area2D MeArea;

    public override void _Ready()
    {
        base._Ready();
        Hp = MaxHp;

        MeArea.AreaEntered += Entered;

    }
    
    public override void _Process(double delta)
    {
        Position += Vector2.Left * Speed*(float)delta;
        if (Position.X < 500) GetParent().GetParent<GameManager>().leaveGame();
    }

    public virtual void TakeDamage(float amount)
    {
        Hp -= amount;
        if (Hp <= 0) {
            QueueFree();
        }
    }

    public virtual void Entered(Area2D area)
    {
        if (area.IsInGroup("Unit") && area.GetParent() is BrainRoted roted)
        {
            roted.TakeDamage(Damage);  
            Position += new Vector2(Speed, 0);
        }
        
    }
}