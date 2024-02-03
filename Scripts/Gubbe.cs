using Godot;
using System;

public partial class Gubbe : CharacterBody2D
{
	private Vector2 _desiredPosition = Vector2.Zero;
	private float _speedMultiplier = 1.0f;

	[Export]
	public float Speed {get; set;} = 300.0f;

	// Get the gravity from the project settings to be synced with RigidBody nodes.
	public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle(); // No gravity.
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Position = GetViewportRect().Size / 2;
		_desiredPosition = Position;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		
	}

    public override void _Input(InputEvent @event)
    {
        if (@event is InputEventMouseButton mouseEvent && mouseEvent.Pressed)
        {
            MoveTo(GetGlobalMousePosition());
        }
    }

    // Handle Physics in here.
    public override void _PhysicsProcess(double delta)
	{
		Move();
	}

	/// <summary>
	/// Move towards the provided position in a straight line at max speed.
	/// </summary>
	public void MoveTo(Vector2 position){
		_desiredPosition = position;
	}

	/// <summary>
	/// Contains all physics related to movement for cleanliness.
	/// </summary>
	private void Move(){
		Velocity = Position.DirectionTo(_desiredPosition) * Speed * _speedMultiplier;
		if (Position.DistanceTo(_desiredPosition) > 10){
			MoveAndSlide();
		}
	}
}
