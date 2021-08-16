using System;
using System.Collections.Generic;
using System.Text;

namespace RPG
{
    public static class Graphisme
    {
        public static ConsoleColor ColorCyan()
        {
            ConsoleColor currentForeground = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Cyan;
            return currentForeground;
        }

        public static ConsoleColor ColorGreen()
        {
            ConsoleColor currentForeground = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Green;
            return currentForeground;
        }

        public static ConsoleColor ColorRed()
        {
            ConsoleColor currentForeground = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            return currentForeground;
        }

        public static ConsoleColor ColorBlue()
        {
            ConsoleColor currentForeground = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Blue;
            return currentForeground;
        }

        public static ConsoleColor ColorMagenta()
        {
            ConsoleColor currentForeground = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Magenta;
            return currentForeground;
        }

        public static ConsoleColor ColorRarete(int rarete)
        {
            ConsoleColor currentForeground = Console.ForegroundColor;

            if (rarete == 1) Console.ForegroundColor = ConsoleColor.White;
            if (rarete == 2) Console.ForegroundColor = ConsoleColor.Green;
            if (rarete == 3) Console.ForegroundColor = ConsoleColor.Cyan;
            if (rarete == 4) Console.ForegroundColor = ConsoleColor.Magenta;

            return currentForeground;
        }
    }
}
