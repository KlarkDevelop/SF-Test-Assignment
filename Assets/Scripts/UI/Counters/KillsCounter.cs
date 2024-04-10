
public class KillsCounter : Counter
{
    protected override void Init()
    {
        base.Init();
        GameStatistic.onKillsValueUpdate += UpdateCounter;
    }
}
