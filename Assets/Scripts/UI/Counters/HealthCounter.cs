

public class HealthCounter : Counter
{
    protected override void Init()
    {
        base.Init();
        Player.onHealthValueChange += UpdateCounter;
    }
}
