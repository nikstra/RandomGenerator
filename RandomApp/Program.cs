using System;
using System.Collections.Generic;
using RandomLib;

namespace RandomApp
{
    class Program
    {
        static void Main(string[] args)
        {
            int count = 10;
            RandomGenerator randGen = new RandomGenerator();

            Console.WriteLine("Aeroplane models:");
            foreach ( Tuple<string, string> aeroplane in randGen.AeroplaneModels(count))
                Console.WriteLine(aeroplane.Item1 + " " + aeroplane.Item2);
            Console.WriteLine();

            Console.WriteLine("Boat models:");
            foreach (Tuple<string, string> boat in randGen.BoatModels(count))
                Console.WriteLine(boat.Item1 + " " + boat.Item2);
            Console.WriteLine();

            Console.WriteLine("Bus models:");
            foreach (Tuple<string, string> bus in randGen.BusModels(count))
                Console.WriteLine(bus.Item1 + " " + bus.Item2);
            Console.WriteLine();

            Console.WriteLine("Car models:");
            foreach (Tuple<string, string> car in randGen.CarModels(count))
                Console.WriteLine(car.Item1 + " " + car.Item2);
            Console.WriteLine();

            Console.WriteLine("Motorcykle models:");
            foreach (Tuple<string, string> motorcykle in randGen.MotorcykleModels(count))
                Console.WriteLine(motorcykle.Item1 + " " + motorcykle.Item2);
            Console.WriteLine();

            Console.WriteLine("Registration numbers:");
            foreach (string regNum in randGen.RegNumbers(count))
                Console.WriteLine(regNum);
            Console.WriteLine();

            Console.WriteLine("Colors (in Swedish):");
            foreach (string color in randGen.Colors(count))
                Console.WriteLine(color);
            Console.WriteLine();

            Console.WriteLine("Full names:");
            foreach (Tuple<string, string> person in randGen.People(count))
                Console.WriteLine(person.Item1 + " " + person.Item2);
            Console.WriteLine();

            Console.WriteLine("Full names, male:");
            foreach (Tuple<string, string> person in randGen.People(count, (int)Gender.male))
                Console.WriteLine(person.Item1 + " " + person.Item2);
            Console.WriteLine();

            Console.WriteLine("Full names, female:");
            foreach (Tuple<string, string> person in randGen.People(count, (int)Gender.female))
                Console.WriteLine(person.Item1 + " " + person.Item2);
            Console.WriteLine();

            Console.WriteLine("First names:");
            foreach (string name in randGen.FirstNames(count))
                Console.WriteLine(name);
            Console.WriteLine();

            Console.WriteLine("First names (female):");
            foreach (string name in randGen.FirstNames(count, (int)Gender.female))
                Console.WriteLine(name);
            Console.WriteLine();

            Console.WriteLine("First names (male):");
            foreach (string name in randGen.FirstNames(count, (int)Gender.male))
                Console.WriteLine(name);
            Console.WriteLine();

            Console.WriteLine("Last names:");
            foreach (string name in randGen.LastNames(count))
                Console.WriteLine(name);
            Console.WriteLine();

            Console.WriteLine("Date and time:");
            Console.WriteLine(randGen.DateAndTime(new DateTime(1980, 1, 1, 0, 0, 0), new DateTime(1990, 12, 31, 23, 59, 59)));
            Console.WriteLine();

            Console.WriteLine("Dates and times:");
            foreach (DateTime dateTime in randGen.DatesAndTimes(new DateTime(2015, 1, 1, 0, 0, 0), DateTime.Now, count))
                Console.WriteLine(dateTime);
            Console.WriteLine();

            Console.WriteLine("Random numbers:");
            foreach (int value in randGen.Integers(count, 0, 10))
                Console.WriteLine(value);
            Console.WriteLine();

            Console.WriteLine("Generic data (movie titles):");
            foreach (string item in randGen.GenericData<List<string>>(new List<string> {
                    "Carol", "Daddys Home", "Den gode dinosaurien", "En man som heter Ove",
                    "En underbar jävla jul", "In the Heart of the Sea", "Star Wars: The Force Awakens",
                    "Suffragette", "The Hateful Eight", "The Hunger Games - Mockingjay Part 2" }, count))
                Console.WriteLine(item);
            Console.WriteLine();
        }
    }
}

