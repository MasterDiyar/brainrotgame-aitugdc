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
	
	public override void _Ready()
	{
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
			MobGridPos += (MobGridPos.Y < 2) ? Vector2I.Down : Vector2I.Zero;
		}

		if (Input.IsActionJustPressed("yes"))
		{
			var mob = GetMob(MobGridPos);
			if (Gridrot[ChooseGridPos.X, ChooseGridPos.Y] == null) {
				Gridrot[ChooseGridPos.X, ChooseGridPos.Y] = mob;
				Gridrot[ChooseGridPos.X, ChooseGridPos.Y].Position = StartPos + (ChooseGridPos * 128) + (ChooseGridPos.Y * Vector2.Down * 21) - (Vector2.One * 2);
				GetParent().AddChild(Gridrot[ChooseGridPos.X, ChooseGridPos.Y]);
				
				Gridrot[ChooseGridPos.X, ChooseGridPos.Y].TreeExited += () => Gridrot[ChooseGridPos.X, ChooseGridPos.Y] = null;
				_isChoosing = false;
			}
			else GD.Print(Gridrot[ChooseGridPos.X, ChooseGridPos.Y]);
		}
		
		_chooseGrid.Position = MobStartPos + (MobGridPos * 132) - (Vector2.One * 2);
	}

	public BrainRoted GetMob(Vector2 point)
	{
		string[] mobAbout = ["Type: Lirili larila\n Cost: 50"],
				paths = ["res://scene/br/lirili_larila.tscn"];
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
