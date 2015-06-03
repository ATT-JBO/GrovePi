# GrovePi
Windows 10 IoT C# driver library for GrovePi

#####Examples
Below are some simple examples of how to use the library.

All supported sensors are available through the DeviceFactory.

######Measure Distance
Ultra sonic sensor plugged in to digital pin 2 (D2)
<p>
<code>
  var distance = DeviceFactory.Build.BuildUltraSonicSensor(Pin.DigitalPin2).MeasureInCentimeters();
</code>
</p>

######Display Hello World
<code>
  DeviceFactory.Build.RgbLcdDisplay().SetText("Hello World").SetBacklightRgb(0, 255, 255);
</code>

######Sound the buzzer!
<code>
  DeviceFactory.Build.BuildBuzzer(Pin.DigitalPin2).ChangeState(SensorStatus.On);
</code>
