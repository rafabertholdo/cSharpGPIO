using System;
using System.IO;

namespace CSharpGPIO.Library
{
    public enum GPIODirection {
        input,
        output
    }

    public class GPIO
    {
        public string Number { get; private set; }
        public GPIODirection Direction { get; private set; }
        public bool StreamGood { get; private set; }

        public GPIO(string number, GPIODirection direction) {
            this.Number = number;
            this.Direction = direction;
            try {
                UnexportGPIO();
            } catch(Exception) {

            }

            ExportGPIO();

            if (direction == GPIODirection.input) {
                SetInputDirection();
            } else {
                SetOutputDirection();
            }
        }

        ~GPIO() {
            UnexportGPIO();
        }

        public void SetValue(bool value) {
            if (StreamGood && Direction == GPIODirection.output) {
                WriteFile(string.Format("/sys/class/gpio/gpio{0}/value", Number), value ? "1" : "0");
            }
        }

        public bool GetValue() {
            if (StreamGood) {
                string value = ReadFile(string.Format("/sys/class/gpio/gpio{0}/value", Number));
                return value[0] != '0';
            } else {
                return false;
            }
        }

        private void SetInputDirection() {
            SetDirection("in");
        }

        private void SetOutputDirection() {
            SetDirection("out");
        }

        private void SetDirection(string direction) {
            if (StreamGood) {
                WriteFile(string.Format("/sys/class/gpio/gpio{0}/direction", Number), direction);
            }
        }

        private void ExportGPIO() {
            WriteFile("/sys/class/gpio/export", Number);
        }

        private void UnexportGPIO() {
            WriteFile("/sys/class/gpio/unexport", Number);
        }

        private string ReadFile(string path) {
            string result = File.ReadAllText(path);
            StreamGood = true;
            return result;
        }

        private void WriteFile(string path, string value) {
            File.WriteAllText(path, value);
            StreamGood = true;
        }
    }
}
