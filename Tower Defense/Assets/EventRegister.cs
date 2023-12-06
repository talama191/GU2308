
using System;

public class EventRegister : MonoSingleton<EventRegister>
{
    public event Action<string> OnBuildTowerAction;
    public event Action<string> OnUpdatePath;

    public void InvokeBuildTowerAction(string data)
    {
        OnBuildTowerAction?.Invoke(data);
    }

    public void InvokeUpdatePath(string data)
    {
        OnUpdatePath?.Invoke(data);
    }
}
