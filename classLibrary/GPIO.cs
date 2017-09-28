using System;
using System.IO;

namespace cSharpGPIO
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

            UnexportGPIO();
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
                WriteFile("/sys/class/gpio/gpio{Number}/value", value ? "1" : "0");
            }
        }

        public bool GetValue() {
            if (StreamGood && Direction == GPIODirection.input) {
                string value = ReadFile("/sys/class/gpio/gpio{Number}/value");
                return value != "0";
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
                WriteFile("/sys/class/gpio/gpio{Number}/direction", direction);
            }
        }

        private void ExportGPIO() {
            WriteFile("/sys/class/gpio/export", Number);
        }

        private void UnexportGPIO() {
            WriteFile("/sys/class/gpio/unexport", Number);
        }

        private string ReadFile(string path) {
            FileStream fileStream = new FileStream(path, FileMode.Open);
            string line = "";
            using (StreamReader reader = new StreamReader(fileStream))
            {
                line = reader.ReadLine();
            }
            fileStream.Close();
            return line;
        }
        private void WriteFile(string path, string value) {
            StreamWriter file = new StreamWriter(path);
            file.WriteLine(value);
            file.Close();
        }
    }
}
