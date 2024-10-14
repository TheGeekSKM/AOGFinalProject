using SaiUtils.StateMachine;

public class EnemyBaseState : IState
{
    EnemyBrain _controller;
    protected EnemyBrain Controller => _controller;

    public EnemyBaseState(EnemyBrain controller)
    {
        _controller = controller;
    }

    public virtual void OnEnter()
    {

    }

    public virtual void Update()
    {

    }

    public virtual void FixedUpdate()
    {

    }

    public virtual void OnExit()
    {

    }
}