namespace LaunchBad
{
    [System.Serializable]
    public class FuelTank
    {
        public float InitialFuelAmount;
        public float CurrentFuelAmount;
        public float RequiredFuelAmount;
        public float FuelLeakSpeed;
        public float FuelLeakStartTime;
    }
}