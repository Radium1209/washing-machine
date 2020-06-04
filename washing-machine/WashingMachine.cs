﻿using System;
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
        private WashingMethod _washingMethod;
        private WashingTime _washingTime;
        private MenuType _menuType;
        private int remainTime;
        private Thread timeThread;
        private Thread writeThread;
        private Thread readThread;
        private bool hasChange, isStart, isPause;
        public WashingMachine()
        {
            Console.Out.WriteLine("Starting washing machine...");
            _temperature = Temperature.C30_50;
            _washingMethod = WashingMethod.NORMAL;
            _washingTime = WashingTime.T30;
            _menuType = MenuType.MAIN;
            remainTime = -1;
            timeThread = new Thread(Washing);
            writeThread = new Thread(PrintMenu);
            readThread = new Thread(GetOption);
            hasChange = false;
            isStart = false;
            isPause = true;
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
                if (isStart = false || hasChange == true)
                {
                    if (remainTime == 0)
                    {
                        remainTime = -1;
                        Console.Out.WriteLine("Washing Finish!(Press any key to continue)");
                        Console.ReadLine();
                    }
                    hasChange = false;
                    Console.Clear();

                    if (remainTime > 0)
                    {
                        Console.Out.WriteLine("remaing {0} minute(s)", remainTime);
                    }
                    isStart = true;
                    Console.Out.WriteLine("Temperature: {0}", _temperature);
                    Console.Out.WriteLine("Washing Method: {0}", _washingMethod);
                    Console.Out.WriteLine("Washing Time: {0}", _washingTime);
                    switch(_menuType)
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
            if (remainTime <= 0)
            {
                Console.Out.WriteLine("0. Start washing");
            }
            else if (isPause == true)
            {
                Console.Out.WriteLine("0. Resume");
            }
            else
            {
                Console.Out.WriteLine("0. Pause");
            }
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
                switch(_menuType)
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
                if (remainTime <= 0)
                {
                    StartWashing();
                }
                else if (isPause == true)
                {
                    isPause = false;
                }
                else
                {
                    isPause = true;
                }
            }
            else if (op == "1" || op == "2" || op == "3")
            {
                _menuType = (MenuType)int.Parse(op);
            }
            hasChange = true;
        }

        public void SetTemperature(string op)
        {
            if (op == "0" || op == "1" || op == "2")
            {
                _temperature = (Temperature)int.Parse(op);
            }
            _menuType = MenuType.MAIN;
            hasChange = true;
        }

        public void SelectWashingMethod(string op)
        {
            if (op == "0" || op == "1")
            {
                _washingMethod = (WashingMethod)int.Parse(op);
            }
            _menuType = MenuType.MAIN;
            hasChange = true;
        }

        public void SetWashingTime(string op)
        {
            if (op == "0" || op == "1" || op == "2" || op =="3")
            {
                _washingTime = (WashingTime)int.Parse(op);
            }
            _menuType = MenuType.MAIN;
            hasChange = true;
        }

        public void StartWashing()
        {
            remainTime = ((int)_washingTime + 1) * 15;
            hasChange = true; isPause = false;
            timeThread.Start();
        }

        public void Washing()
        {
            while (remainTime > 0)
            {
                if (isPause == false)
                {
                    Thread.Sleep(5000);
                    remainTime--;
                    hasChange = true;
                }
            }
        }
    }
}
