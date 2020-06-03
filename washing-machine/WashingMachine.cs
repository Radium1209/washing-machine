using System;
using System.Collections;
using System.Threading;

namespace washing_machine
{
    // Three gears of temperature
    public enum Temperature
    {
        C30_50 = 0,
        C50_70,
        C70_90
    }

    // Two washing methods
    public enum WashingMethod
    {
        NORMAL = 0,
        STERILIZE
    }

    public enum WashingTime
    {
        T15 = 0,
        T30,
        T45,
        T60
    }
    public enum MenuType
    {
        MAIN = 0,
        TEMPERATURE,
        WASHING_METHOD,
        WASHING_TIME
    }

    class WashingMachine
    {
        private Temperature _temperature;
        private WashingMethod _washingmethod;
        private WashingTime _washingtime;
        private MenuType _menutype;
        private MenuType preMenuType;
        private int remainTime, preRemainTime;
        private Thread timeThread;
        private Thread writeThread;
        private Thread readThread;
        private bool isStart;
        public WashingMachine()
        {
            Console.Out.WriteLine("Starting washing machine...");
            _temperature = Temperature.C30_50;
            _washingmethod = WashingMethod.NORMAL;
            _washingtime = WashingTime.T30;
            preMenuType = _menutype = MenuType.MAIN;
            preRemainTime = remainTime = -1;
            timeThread = new Thread(washing);
            writeThread = new Thread(PrintMenu);
            readThread = new Thread(GetOption);
            isStart = false;
        }

        public void Run()
        {
            writeThread.Start();
            readThread.Start();
        }

        public void PrintMenu()
        {
            while (true)
            {
                if (remainTime == -1 || preRemainTime != remainTime || _menutype != preMenuType)
                {
                    if (remainTime == 0)
                    {
                        Console.Out.WriteLine("Washing Finish!(Press any key to continue)");
                        Console.ReadLine();
                    }
                    if (remainTime == -1)
                    {
                        preRemainTime = remainTime = 0;
                    }
                    preRemainTime = remainTime;
                    preMenuType = _menutype;
                    Console.Clear();
                    Console.Out.WriteLine("remaing {0} minute(s)", remainTime);
                    Console.Out.WriteLine("Temperature:{0}", _temperature);
                    Console.Out.WriteLine("Washing Method:{0}", _washingmethod);
                    Console.Out.WriteLine("Washing Time:{0}", _washingtime);
                    switch(_menutype)
                    {
                        case MenuType.MAIN: MainMenu();
                            break;
                        case MenuType.TEMPERATURE: TemperatureMenu();
                            break;
                        case MenuType.WASHING_METHOD: WashingMethodMenu();
                            break;
                        case MenuType.WASHING_TIME: WashingTimeMenu();
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        public void MainMenu()
        {
            Console.Out.WriteLine("0. Start washing");
            Console.Out.WriteLine("1. Set temperature");
            Console.Out.WriteLine("2. Select washing method");
            Console.Out.WriteLine("3. Set washing time");
        }

        public void TemperatureMenu()
        {
            Console.Out.WriteLine("0. 30-50C");
            Console.Out.WriteLine("1. 50-70C");
            Console.Out.WriteLine("2. 70-90C");
        }

        public void WashingMethodMenu()
        {
            Console.Out.WriteLine("0. NORMAL");
            Console.Out.WriteLine("1. STERILIZE");
        }

        public void WashingTimeMenu()
        {
            Console.Out.WriteLine("0. 15 Minutes");
            Console.Out.WriteLine("1. 30 Minutes");
            Console.Out.WriteLine("2. 45 Minutes");
            Console.Out.WriteLine("3. 60 Minutes");
        }

        public void GetOption()
        {
            while (true)
            {
                var op = Console.ReadLine();
                switch(_menutype)
                {
                    case MenuType.MAIN: SelectMenu(op);
                        break;
                    case MenuType.TEMPERATURE: SetTemperature(op);
                        break;
                    case MenuType.WASHING_METHOD: SelectWashingMethod(op);
                        break;
                    case MenuType.WASHING_TIME: SetWashingTime(op);
                        break;
                    default:
                        break;
                }
            }
        }

        public void SelectMenu(string op)
        {
            if (op == "0")
            {
                StartWashing();
            }
            else if (op == "1" || op == "2" || op == "3")
            {
                _menutype = (MenuType)int.Parse(op);
            }
        }

        public void SetTemperature(string op)
        {
            if (op == "0" || op == "1" || op == "2")
            {
                _temperature = (Temperature)int.Parse(op);
            }
            _menutype = MenuType.MAIN;
        }

        public void SelectWashingMethod(string op)
        {
            if (op == "0" || op == "1")
            {
                _washingmethod = (WashingMethod)int.Parse(op);
            }
            _menutype = MenuType.MAIN;
        }

        public void SetWashingTime(string op)
        {
            if (op == "0" || op == "1" || op == "2" || op =="3")
            {
                _washingtime = (WashingTime)int.Parse(op);
            }
            _menutype = MenuType.MAIN;
        }

        public void StartWashing()
        {
            isStart = true;
            remainTime = ((int)_washingtime + 1) * 15;
            preRemainTime = remainTime + 1;
            timeThread.Start();
        }

        public void washing()
        {
            while (remainTime > 0)
            {
                Thread.Sleep(1000);
                remainTime--;
            }
        }
    }
}
