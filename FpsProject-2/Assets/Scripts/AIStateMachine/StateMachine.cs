
public class StateMachine
{
    IState currentState;

    public void ChangeState(IState state)
    {
        if (currentState != state && currentState != null) currentState.Exit();
        currentState = state;
        currentState.Enter();
    }

    public void Update(float dt)
    {
        if (currentState != null) currentState.Execute(dt);
    }

}

