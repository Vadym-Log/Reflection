using System;
using System.Reflection;
using Task_01;

namespace Task_02
{
    class Program
    {
        static void Main(string[] args)
        {            
            Console.Write("Введите температуру в градусах Цельсия: ");
            float temperature = Convert.ToInt16(Console.ReadLine());
            
            TemperatureConverter temperatureConverter = new TemperatureConverter();

            Type type = temperatureConverter.GetType();

            MethodInfo mi = type.GetMethod("CelsiusToFahrenheit", BindingFlags.Static | BindingFlags.Public);

            float result = (float)mi.Invoke(temperatureConverter, new object[] { temperature });

            Console.WriteLine("Температура в градусах Фаренгейта = {0}", result);
            Console.ReadKey();            
        }
    }
}
