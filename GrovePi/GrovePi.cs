﻿using System;
using System.Globalization;
using Windows.ApplicationModel.Contacts;
using Windows.Devices.I2c;

namespace GrovePi
{
    public interface IGrovePi
    {
        string GetFirmwareVersion();
        byte[] DigitalRead(byte pin);
        byte[] DigitalWrite(byte pin);
        int AnalogRead(byte pin);
        byte[] AnalogWrite(byte pin);
    }


    internal sealed class GrovePi : IGrovePi
    {
        private const byte Unused = 0;
        private const byte DigitalReadCommandAddress = 1;
        private const byte DigitalWriteCommandAddress = 2;
        private const byte AnalogReadCommandAddress = 3;
        private const byte AnalogWriteCommandAddress = 4;
        private const byte VersionCommandAddress = 8;

        private readonly I2cDevice _device;

        internal GrovePi(I2cDevice device)
        {
            if (device == null) throw new ArgumentNullException(nameof(device));
            _device = device;
        }

        /// <summary>
        ///     Read the firmware version
        /// </summary>
        public string GetFirmwareVersion()
        {
            var buffer = new[] {VersionCommandAddress, Unused, Unused, Unused };
            _device.Write(buffer);
            _device.Read(buffer);
            return $"{buffer[1]}.{buffer[2]}.{buffer[3]}";
        }

        public byte[] DigitalRead(byte pin)
        {
            var buffer = new[] { DigitalReadCommandAddress, pin, Unused, Unused };
            _device.Write(buffer);
            _device.Read(buffer);
            return buffer;
        }

        public byte[] DigitalWrite(byte pin)
        {
            var buffer = new[] { DigitalWriteCommandAddress, pin, Unused, Unused };
            _device.Write(buffer);
            return buffer;
        }

        public int AnalogRead(byte pin)
        {
            var buffer = new[] { DigitalReadCommandAddress, AnalogReadCommandAddress, pin, Unused, Unused };
            _device.Write(buffer);
            _device.Read(buffer);
            return buffer[1] * 256 + buffer[2];
        }

        public byte[] AnalogWrite(byte pin)
        {
            var buffer = new[] { AnalogWriteCommandAddress, pin, Unused, Unused };
            _device.Write(buffer);
            return buffer;
        }
    }
}