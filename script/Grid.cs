using Godot;
using System;
using System.Collections.Generic;

public partial class Grid : Node2D
{
	public Vector2 StartPos = new Vector2(559, 222),
		MobStartPos = new Vector2(10, 580),
		GridScale = Vector2.One*128;

	public Vector2I ChooseGridPos = Vector2I.Zero,
		MobGridPos = Vector2I.Zero;
	
	public BrainRoted[,] Gridrot= new BrainRoted[8,4];
	
	private Sprite2D _chooseGrid;
	[Export] private Node2D _brainrotSpawnNode;
	[Export] private Label about;
	
	bool _isChoosing = false;

	private GameManager parent;
	
	public override void _Ready()
	{
		parent = GetParent<GameManager>();
		_chooseGrid = GetNode<Sprite2D>("Pos");
	}

	public override void _Process(double delta)
	{
		if (_isChoosing) ChooseInputs();
		else GridInputs();
	}

	public void GridInputs() //rabotaet
	{
		if (Input.IsActionJustPressed("right")) {
			ChooseGridPos += (ChooseGridPos.X < 7) ? Vector2I.Right : Vector2I.Zero;    
		} if (Input.IsActionJustPressed("left")) {
			ChooseGridPos += (ChooseGridPos.X > 0) ? Vector2I.Left : Vector2I.Zero;
		} if (Input.IsActionJustPressed("up")) {
			ChooseGridPos += (ChooseGridPos.Y > 0) ? Vector2I.Up : Vector2I.Zero;
		} if (Input.IsActionJustPressed("down")) {
			ChooseGridPos += (ChooseGridPos.Y < 3) ? Vector2I.Down : Vector2I.Zero;
		} if (Input.IsActionJustPressed("yes")) {
			_isChoosing = true;
			MobGridPos = Vector2I.Zero;
		}
		_chooseGrid.Position = StartPos + (ChooseGridPos * 128) + (ChooseGridPos.Y * Vector2.Down * 21) - (Vector2.One * 2);
	}

	public void ChooseInputs() //work in process
	{
		if (Input.IsActionJustPressed("right")) {
			MobGridPos += (MobGridPos.X < 2) ? Vector2I.Right : Vector2I.Zero;	
		} if (Input.IsActionJustPressed("left")) {
			MobGridPos += (MobGridPos.X > 0) ? Vector2I.Left : Vector2I.Zero;
		}if (Input.IsActionJustPressed("up")) {	
			MobGridPos += (MobGridPos.Y > 0) ? Vector2I.Up : Vector2I.Zero;
		}if (Input.IsActionJustPressed("down")) {
			MobGridPos += (MobGridPos.Y < 1) ? Vector2I.Down : Vector2I.Zero;
		}

		if (Input.IsActionJustPressed("yes"))
		{
			var mob = GetMob(MobGridPos);
			if (Gridrot[ChooseGridPos.X, ChooseGridPos.Y] == null &&  parent.Money >= mob.Cost ) {
				Gridrot[ChooseGridPos.X, ChooseGridPos.Y] = mob;
				GD.Print("Before: " + parent.Money+" ", mob.Cost);
				parent.Money -= mob.Cost;
				GD.Print("After: " + parent.Money);
				mob.Position = StartPos + (ChooseGridPos * 128) + (ChooseGridPos.Y * Vector2.Down * 21) - (Vector2.One * 2);
				GetParent().GetNode("brainrots").AddChild(mob);

				var pPos = ChooseGridPos;
				mob.TreeExited += () => Gridrot[pPos.X, pPos.Y] = null;
				
				_isChoosing = false;
			}
			else GD.Print(Gridrot[ChooseGridPos.X, ChooseGridPos.Y]);
		}

		if (Input.IsActionJustPressed("no"))
		{
			_isChoosing = false;
		}
		
		_chooseGrid.Position = MobStartPos + (MobGridPos * 132) - (Vector2.One * 2);
	}

	public BrainRoted GetMob(Vector2 point)
	{
		string[] mobAbout = ["Type: Lirili larila\n Cost: 50", "Type: Frigo Camelini\n Cost: 120", "Type: Udin din din dun\n Cost: 80"
			,"Type: Trippi troppi\n Cost: 170", "Type: La Vacca Saturno Saturnita\n Cost: 320","Type: Cappucini Ballerini\n Cost: 100"],
				paths = ["res://scene/br/lirili_larila.tscn", "res://scene/br/frigo.tscn", "res://scene/br/udindindindun.tscn",
				"res://scene/br/trippitroppi.tscn", "res://scene/br/lavaka.tscn", "res://scene/br/balerina.tscn"];
		try {
        	int width = 3;
        	int index = (int)point.Y * width + (int)point.X;
        	string mob = paths[index];
			about.Text = mobAbout[index];
        	return GD.Load<PackedScene>(mob).Instantiate<BrainRoted>();
        }catch (IndexOutOfRangeException e)
        {
        	GD.PrintErr("Индекс вне диапазона: " + e.Message, point);
        	return GD.Load<PackedScene>(paths[0]).Instantiate<BrainRoted>();
	        
        }
	}
	
}
