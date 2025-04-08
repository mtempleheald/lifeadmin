namespace LifeAdmin;

public class StateContainer
{
    public Dictionary<string, object?> State { get; private set; } = [];

    public void SaveState(StateEntry stateEntry)
    {
        State[stateEntry.Key] = stateEntry.Value;
        NotifyStateChanged();
    }

    public event Action? OnChange;

    private void NotifyStateChanged() => OnChange?.Invoke();
}
