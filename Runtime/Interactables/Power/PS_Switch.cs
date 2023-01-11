namespace Gamekit2D.Runtime.Interactables.Power
{
    public class PS_Switch : PowerSupply
    {
        protected override void Interact() => IsOn = !IsOn;
    }
}