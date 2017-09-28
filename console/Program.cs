using System;
using cSharpGPIO;
using System.Threading;

namespace console
{
    class Program
    {
        static void Main(string[] args)
        {

            var gpio18 = new GPIO("18", GPIODirection.output);
            var gpio17 = new GPIO("17", GPIODirection.input);

            int hitCount =0;
            int maxHitCount = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("How many button presses: ");
            if (gpio18.StreamGood && gpio17.StreamGood) {
                bool oldButtonState = false;
                while (hitCount < maxHitCount) {
                    bool buttonState = gpio17.GetValue();
                    if (oldButtonState != buttonState && oldButtonState) {
                        hitCount++;
                    }
                    gpio18.SetValue(buttonState);
                    Thread.Sleep(200);
                    oldButtonState = buttonState;
                }
            } else {
                Console.WriteLine("Cannot initialize streams.....");
            }

            Console.WriteLine("Exiting.....");
        }
    }
}
