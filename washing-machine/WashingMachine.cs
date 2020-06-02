using System;
using Time = System.UInt32;

namespace washing_machine
{
    // Three gears of temperature
    public enum Temperature
    {
        T30_50,
        T50_70,
        T70_90
    }

    // Two washing methods
    public enum WashingMethod
    {
        WET,
        DRY
    }

    class WashingMachine
    {
        private Temperature temper;
        private WashingMethod method;
        private Time time;
        public WashingMachine()
        {
            Console.Out.Write("Starting washing machine...");
            temper = Temperature.T30_50;
            method = WashingMethod.WET;
            time = 30;
        }

        public void menu()
        {
            while (true)
            {
                Console.Out.WriteLine("Menu");
                Console.Out.WriteLine("1. Set temperature");
                Console.Out.WriteLine("2. Select washing method");
                Console.Out.WriteLine("3. Set washing time");
                var t = Console.ReadLine();
                switch (t)
                {
                    case "1": SetTemperature();
                        break;
                    case "2": SelectWashingMethod();
                        break;
                    case "3": SetWashingTime();
                        break;
                    default: return;
                }
            }
        }

        public void SetTemperature()
        {
            Console.Out.WriteLine("Set temperature");
        }

        public void SelectWashingMethod()
        {
            Console.Out.WriteLine("Select washing method");
        }

        public void SetWashingTime()
        {
            Console.Out.WriteLine("Set washing time");
        }
    }
}
