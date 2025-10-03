using Godot;

public partial class People: Node2D
{
    [Export] public float Hp = 20;
    [Export] public float MaxHp = 20;
    [Export] public float Speed = 20;

    public void TakeDamage(float amount)
    {
        Hp -= amount;
        if (Hp <= 0) {
            QueueFree();
        }
    }
}