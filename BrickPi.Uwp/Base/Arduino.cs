﻿namespace BrickPi.Uwp.Base
{

    public enum Arduino : byte
    {
        //Broadcast = 0x00,
        Arduino1 = 0x01,
        Arduino2 = 0x02,

    }

    public enum ArduinoPort : byte
    {
        Port1 = 0x00,
        Port2 = 0x01,
    }

    public static class ArduinoExtensions
    {

        public static ArduinoPort ArduinoPort(this SensorPort sensorPort)
        {
            return (ArduinoPort)((int)sensorPort % 2);
        }

        public static ArduinoPort ArduinoPort(this MotorPort motorPort)
        {
            return (ArduinoPort)((int)motorPort % 2);
        }

        public static SensorPort SensorPort(this Arduino arduino, ArduinoPort port)
        {
            return (SensorPort)(((int)arduino - 1) * 2 + (int)port);
        }

        public static SensorPort[] SensorPorts(this Arduino arduino)
        {
            switch (arduino)
            {
                case Arduino.Arduino1:
                    return new SensorPort[] { BrickPi.Uwp.Base.SensorPort.Port_S1, BrickPi.Uwp.Base.SensorPort.Port_S2 };
                case Arduino.Arduino2:
                    return new SensorPort[] { BrickPi.Uwp.Base.SensorPort.Port_S3, BrickPi.Uwp.Base.SensorPort.Port_S4};
                default:
                    return EnumExtension<SensorPort>.All();
            }
        }

        public static MotorPort MotorPort(this Arduino arduino, ArduinoPort port)
        {
            return (MotorPort)(((int)arduino - 1) * 2 + (int)port);
        }

        public static MotorPort[] MotorPorts(this Arduino arduino)
        {
            switch (arduino)
            {
                case Arduino.Arduino1:
                    return new MotorPort[] {BrickPi.Uwp.Base.MotorPort.Port_MA, BrickPi.Uwp.Base.MotorPort.Port_MB};
                case Arduino.Arduino2:
                    return new MotorPort[] { BrickPi.Uwp.Base.MotorPort.Port_MC, BrickPi.Uwp.Base.MotorPort.Port_MD};
                default:
                    return EnumExtension<MotorPort>.All();
            }
        }

    }
}
