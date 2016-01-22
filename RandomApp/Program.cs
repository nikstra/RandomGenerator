using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RandomGenerator;

namespace RandomApp
{
    class Program
    {
        static void Main(string[] args)
        {
            int count = 100;
            RandomGen randGen = new RandomGen();

            foreach( Tuple<string, string> aeroplane in randGen.AeroplaneModels(count))
                Console.WriteLine(aeroplane.Item1 + " " + aeroplane.Item2);

            foreach (Tuple<string, string> boat in randGen.BoatModels(count))
                Console.WriteLine(boat.Item1 + " " + boat.Item2);

            foreach (Tuple<string, string> bus in randGen.BusModels(count))
                Console.WriteLine(bus.Item1 + " " + bus.Item2);

            foreach (Tuple<string, string> car in randGen.CarModels(count))
                Console.WriteLine(car.Item1 + " " + car.Item2);

            foreach (Tuple<string, string> motorcykle in randGen.MotorcykleModels(count))
                Console.WriteLine(motorcykle.Item1 + " " + motorcykle.Item2);

            foreach (string regNum in randGen.RegNumbers(count))
                Console.WriteLine(regNum);

            foreach (string color in randGen.Colors(count))
                Console.WriteLine(color);

            foreach (Tuple<string, string> person in randGen.People(count))
                Console.WriteLine(person.Item1 + " " + person.Item2);

            Console.WriteLine(randGen.DateAndTime(new DateTime(1980, 1, 1, 0, 0, 0), new DateTime(1990, 12, 31, 23, 59, 59)));

            foreach (DateTime dateTime in randGen.DatesAndTimes(count, new DateTime(2015, 1, 1, 0, 0, 0), DateTime.Now))
                Console.WriteLine(dateTime);

            foreach (int value in randGen.Integers(count, 0, 10))
                Console.WriteLine(value);
        }
    }
}
