using SaiUtils.StateMachine;


public class PawnBaseState : IState
{
    PawnController _controller;
    protected PawnController Controller => _controller;

    public PawnBaseState(PawnController controller)
    {
        _controller = controller;
    }

    public virtual void OnEnter()
    {
        // Implementation for OnEnter");
    }

    public virtual void Update()
    {
        // Implementation for Update
    }

    public virtual void FixedUpdate()
    {
        // Implementation for FixedUpdate
    }

    public virtual void OnExit()
    {
        // Implementation for OnExit
    }
}
