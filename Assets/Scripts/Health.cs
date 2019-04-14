using System;

public class Health
{
    public EventHandler OnChanged;
    public EventHandler OnDepleted;

    public float MaxHP     { get; private set; }
    public float CurrentHP { get; private set; }

    public Health(float MaxHP)
    {
        this.MaxHP     = MaxHP;
        this.CurrentHP = MaxHP;
    }

    public void IncreaseBy(float value)
    {
        CurrentHP += Math.Abs(value);

        if (CurrentHP > MaxHP)
            CurrentHP = MaxHP;

        OnChanged?.Invoke(this, null);
    }

    public void DecreaseBy(float value)
    {
        CurrentHP -= Math.Abs(value);

        if (CurrentHP <= 0)
        {
            CurrentHP = 0f;

            OnChanged ?.Invoke(this, null);
            OnDepleted?.Invoke(this, null);

            return;
        }
           
        OnChanged?.Invoke(this, null);
    }
}
