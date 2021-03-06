﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrickPi.Uwp.Base;

namespace BrickPi.Uwp.Sensors.EV3
{
    public class EV3TouchSensor: RawSensor
    {
        private const int threshold = 1020; //from BrickPi source
        public event EventHandler<SensorEventArgs> OnPressed;

        public event EventHandler<SensorEventArgs> OnReleased;

        public bool Pressed { get; set; }

        public EV3TouchSensor(SensorPort sensorPort): base(sensorPort)
        {
            SensorType = SensorType.EV3_TOUCH_DEBOUNCE;
        }

        public EV3TouchSensor(SensorPort sensorPort, SensorType sensorType) : base(sensorPort)
        {
            if (sensorType != SensorType.EV3_TOUCH_0 && sensorType != SensorType.EV3_TOUCH_DEBOUNCE)
                throw new ArgumentOutOfRangeException();
            SensorType = sensorType;
        }

        public override void UpdateSensorResponse(ProtocolArray responseData)
        {
            bool state = Pressed;
            if (SensorType == SensorType.EV3_TOUCH_0)
            {
                RawValue = (int)responseData.GetBits(1, 16);
            }
            else
                base.UpdateSensorResponse(responseData);
            Pressed = (RawValue >= threshold);
            if (state != Pressed)
            {
                this.OnChangedEventHandler(new TouchSensorEventArgs() { Pressed = this.Pressed });
                if (Pressed)
                {
                    if (null != OnPressed)
                        Task.Run(() => OnPressed(this, new SensorEventArgs()));
                }
                else
                {
                    if (null != OnReleased)
                        Task.Run(() => OnReleased(this, new SensorEventArgs()));
                }
            }

            base.UpdateSensorResponse(responseData);
        }
    }
}
