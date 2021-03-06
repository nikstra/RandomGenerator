﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace RandomLib
{
    /// <summary>
    /// Generates various random data such as names, vehicle makes and models, registration numbers and dates.
    /// </summary>
    public class RandomGenerator
    {
        private static Random _random = new Random();

        /// <summary>
        /// Generates a T (which implements IList) with elements in random order based on the T given as argument.
        /// </summary>
        /// <param name="items">A T that implements IList.</param>
        /// <param name="count">Number of items to generate.</param>
        /// <returns>T with elements in random order.</returns>
        public T GenericData<T>(T items, int count = 0) where T : IList, new()
        {
            if (items == null) throw new ArgumentNullException();
            if (count == 0) count = items.Count;

            T randomList = new T();

            for (int i = 0; i < count; i++)
                randomList.Add(items[_random.Next(items.Count)]);

            return randomList;
        }

        /// <summary>
        /// Generates random product brands and models from a dictionary passed as argument.
        /// </summary>
        /// <param name="count">Number of items to generate.</param>
        /// <param name="items">A dictionary of Lists models and brands as keys.</param>
        /// <returns>A List of brand/model tuples.</returns>
        public List<Tuple<string, string>> ProductModels(
            IReadOnlyDictionary<string, string[]> items, int count)
        {
            List<string> keys = new List<string>(items.Keys);
            List<Tuple<string, string>> brandModel = new List<Tuple<string, string>>();

            for (int i = 0; i < count; i++)
            {
                string key = keys[_random.Next(keys.Count)];
                brandModel.Add(
                    new Tuple<string, string>(
                        key,
                        items[key][_random.Next(items[key].Length)]
                    )
                );
            }

            return brandModel;
        }

        /// <summary>
        /// Generates random aeroplane make and models.
        /// </summary>
        /// <param name="count">Number of items to generate.</param>
        /// <returns>A List of make/model tuples.</returns>
        public List<Tuple<string, string>> AeroplaneModels(int count)
        {
            return ProductModels(_aeroplanes, count);
        }

        /// <summary>
        /// Generates random boat make and models.
        /// </summary>
        /// <param name="count">Number of items to generate.</param>
        /// <returns>A List of make/model tuples.</returns>
        public List<Tuple<string, string>> BoatModels(int count)
        {
            return ProductModels(_boats, count);
        }

        /// <summary>
        /// Generates random bus make and models.
        /// </summary>
        /// <param name="count">Number of items to generate.</param>
        /// <returns>A List of make/model tuples.</returns>
        public List<Tuple<string, string>> BusModels(int count)
        {
            return ProductModels(_busses, count);
        }

        /// <summary>
        /// Generates random car make and models.
        /// </summary>
        /// <param name="count">Number of items to generate.</param>
        /// <returns>A List of make/model tuples.</returns>
        public List<Tuple<string, string>> CarModels(int count)
        {
            return ProductModels(_cars, count);
        }

        /// <summary>
        /// Generates random motorcycle make and models.
        /// </summary>
        /// <param name="count">Number of items to generate.</param>
        /// <returns>A List of make/model tuples.</returns>
        public List<Tuple<string, string>> MotorcykleModels(int count)
        {
            return ProductModels(_motorcycles, count);
        }

        /// <summary>
        /// Generates registration number for vehicles.
        /// </summary>
        /// <param name="count">Number of items to generate.</param>
        /// <param name="countryCode">Generate registration numbers used by specified country.</param>
        /// <returns>A List of registration numbers (limited to 12167000 items).</returns>
        public List<string> RegNumbers(int count, string countryCode = "sv")
        {
            // Maximum possible registration numbers: 23*23*23*10*10*10 = 12167000
            if (count > 12167000) count = 12167000;

            HashSet<string> regNums = new HashSet<string>();
            StringBuilder rn = new StringBuilder(6);

            while (regNums.Count < count)
            {
                rn.Clear();

                for (int i = 0; i < 3; i++)
                    rn.Append(_regNumChars[_random.Next(1, _regNumChars.Length)]);

                rn.AppendFormat("{0:D3}", _random.Next(1000));

                regNums.Add(rn.ToString());
            }

            return new List<string>(regNums);
        }

        /// <summary>
        /// Generates random colors.
        /// </summary>
        /// <param name="count">Number of items to generate.</param>
        /// <returns>A List of colors.</returns>
        public List<string> Colors(int count)
        {
            return GenericData(new List<string>(_colors), count);
        }

        /// <summary>
        /// Generates a random first name.
        /// </summary>
        /// <param name="gender">Gender (0=female, 1=male, 2=any).</param>
        /// <returns>A name as a string.</returns>
        public string FirstName(int gender = (int)Gender.any)
        {
            if (gender == (int)Gender.any)
                gender = _random.Next(2);

            return _firstNames[gender][_random.Next(_firstNames[gender].Length)];
        }

        /// <summary>
        /// Generates a random list of first names.
        /// </summary>
        /// <param name="count">Number of names to generate.</param>
        /// <param name="gender">Gender (0=female, 1=male, 2=any).</param>
        /// <returns>A List of first names as strings.</returns>
        public List<string> FirstNames(int count, int gender = (int)Gender.any)
        {
            List<string> names = new List<string>(count);

            for (int i = 0; i < count; i++)
                names.Add(FirstName(gender));

            return names;
        }

        /// <summary>
        /// Generates a random last name.
        /// </summary>
        /// <returns>A last name as a string.</returns>
        public string LastName()
        {
            return _lastNames[_random.Next(_lastNames.Length)];
        }

        /// <summary>
        /// Generates a random list of last names.
        /// </summary>
        /// <param name="count">Number of names to generate.</param>
        /// <returns>A List of last names as strings.</returns>
        public List<string> LastNames(int count)
        {
            List<string> names = new List<string>(count);

            for (int i = 0; i < count; i++)
                names.Add(LastName());

            return names;
        }

        /// <summary>
        /// Generates random human names.
        /// </summary>
        /// <param name="count">Number of items to generate.</param>
        /// <returns>A List of first name/last name tuples.</returns>
        public List<Tuple<string, string>> People(int count, int gender = (int)Gender.any)
        {
            List<Tuple<string, string>> people = new List<Tuple<string, string>>();

            for (int i = 0; i < count; i++)
                people.Add(new Tuple<string, string>(FirstName(gender), LastName()));

            return people;
        }

        /// <summary>
        /// Generates a random date and time.
        /// </summary>
        /// <param name="from">Earliest DateTime for generated value.</param>
        /// <param name="to">Latest DateTime for the generated value.</param>
        /// <returns>A random DateTime in the range given as parameters.</returns>
        public DateTime DateAndTime(DateTime from, DateTime to)
        {
            var range = to - from;

            var randTimeSpan = new TimeSpan((long)(_random.NextDouble() * range.Ticks));

            return from + randTimeSpan;
        }

        /// <summary>
        /// Generates a list of random dates and times.
        /// </summary>
        /// <param name="from">Earliest DateTime for generated value.</param>
        /// <param name="to">Latest DateTime for the generated value.</param>
        /// <param name="count">Number of DateTimes to generate.</param>
        /// <returns>A List of random DateTimes in the range given as parameters.</returns>
        public List<DateTime> DatesAndTimes(DateTime from, DateTime to, int count)
        {
            List<DateTime> dateTimes = new List<DateTime>();

            for (int i = 0; i < count; i++)
                dateTimes.Add(DateAndTime(from, to));

            return dateTimes;
        }

        /// <summary>
        /// Generates a list of integers.
        /// </summary>
        /// <param name="count">Number of integers to generate.</param>
        /// <param name="minValue">Passed to Random.Next() as minValue.</param>
        /// <param name="maxValue">Passed to Random.Next() as maxValue.</param>
        /// <returns>A List of integers.</returns>
        public List<int> Integers(int count, int minValue, int maxValue)
        {
            List<int> ints = new List<int>();

            for (int i = 0; i < count; i++)
                ints.Add(_random.Next(minValue, maxValue));

            return ints;
        }

        private static readonly IReadOnlyDictionary<string, string[]> _aeroplanes = new Dictionary<string, string[]> {
            { "Airbus", new [] {
                "A300", "A310", "A318", "A319", "A320", "A321", "A330", "A340", "A350XWB", "A380" }
            },
            { "Antonov", new [] {
                "An-22", "An-30", "An-32", "An-38", "An-70", "An-72", "An-124", "An-225" }
            },
            { "Boeing", new [] {
                "247", "314", "377 Stratocruiser", "707", "720", "717", "727", "737", "747 Jumbojet",
                "757", "767", "777", "787 Dreamliner" }
            },
            { "Cessna", new [] {
                "120", "140", "150", "152", "162 Skycatcher", "165 Airmaster", "170", "172 Skyhawk",
                "175 Skylark", "177 Cardinal", "180 Skywagon", "182 Skylane", "185 Skywagon", "187",
                "188 Agwagon", "190", "195" }
            },
            { "Douglas", new [] {
                "DC-3", "TBD Devastator", "A-20 Havoc", "SBD Dauntless", "DC-4E", "B-23 Dragon",
                "DC-4", "DC-5", "XB-19", "A-26 Invader", "BTD Destroyer", "XA-42", "A-1 Skyraider",
                "C-74 Globemaster", "XB-43", "DC-6", "D-558-1 Skystreak", "D-558-2 Skyrocket", "F3D Skyknight",
                "C-124 Globemaster II", "A2D Skyshark", "F4D Skyray", "A-3 Skywarrior", "X-3 Stiletto",
                "A-4 Skyhawk", "B-66 Destroyer", "DC-7", "F5D Skylancer", "C-133 Cargomaster",
                "F6D Missileer", "DC-8", "DC-9", "DC-10" }
            },
            { "Lockheed Martin", new [] {
                "A-12", "C-130 Hercules", "C-5 Galaxy", "F-117 Nighthawk", "F-16 Fighting Falcon", "F-22 Raptor" }
            },
            { "Piper", new [] {
                "PA-18A Super", "L-18 Super Cub", "PA-19 Super Cub", "PA-20 Pacer", "PA-22 Tri-Pacer",
                "PA-23 Apache", "PA-23-250 Aztec", "PA-24 Comanche", "PA-25 Pawnee", "PA-28 Cherokee",
                "PA-28R Arrow Cherokee", "PA-30 Twin Comanche", "PA-31 Navajo", "PA-31P Mojave Navajo" }
            }
        };

        private static readonly IReadOnlyDictionary<string, string[]> _boats = new Dictionary<string, string[]> {
            { "Buster", new [] {
                "Cabin E", "Lx", "M2", "Magnum E", "Mini", "S Fish", "XL", "XS" }
            },
            { "Flipper", new [] {
                "600 DC", "600 SC", "600 ST", "640 DC", "640 SC", "640 ST", "670 DC", "670 ST", "760 DC", "880 ST" }
            },
            { "Nimbus", new [] {
                "21 Nova", "27 Nova S", "305 Coupé", "31 Nova S", "335 Coupé", "34 Nova", "365 Coupé", "405 Coupé" }
            },
            { "Sea Ray", new [] {
                "210 Sun Sport", "240 Sun Sport", "240 Sundeck", "270 SLX", "270 Sundeck", "300 SLX", "310 Sundancer",
                "330 Sundancer", "350 SLX", "350 Sundancer", "355 Sundancer", "370 Sundancer" }
            },
            { "Storebro", new [] {
                "435 Commander", "435 SunTop", "460 Commander", "90 E" }
            }
        };

        private static readonly IReadOnlyDictionary<string, string[]> _busses = new Dictionary<string, string[]> {
            { "Neoplan", new [] {
                "Jetliner", "Cityliner", "Skyliner", "Tourliner" }
            },
            { "Scania", new [] {
                "Irizar i6", "Touring", "Van Hool TDX 27 Astromega", "Van Hool TX16" }
            },
            { "Setra", new [] {
                "ComfortClass S 511 HD", "ComfortClass S 515 HD", "ComfortClass S 515 MD", "MultiClass S 412 UL",
                "MultiClass S 415 H", "MultiClass S 415 LE business", "MultiClass S 415 UL", "MultiClass S 415 UL business",
                "MultiClass S 416 UL business", "TopClass S 515 HDH" }
            },
            { "Volvo", new [] {
                "7900", "8900", "9500", "9700", "9900" }
            }
        };

        private static readonly IReadOnlyDictionary<string, string[]> _cars = new Dictionary<string, string[]> {
            { "Audi", new [] {
                "80", "100", "A1", "A2", "A3", "A4", "A5", "A6", "A8", "Quattro", "R8", "TT", "V8" }
            },
            { "Citroën", new [] {
                "Ami", "Axel", "AX", "Berline 7", "Berline 11", "Berline 15", "Berlingo", "BX", "C-Crosser", "CX", "2CV", "C1", "C2",
                "C3", "C4", "C5", "C6", "C8", "C15", "Dyane", "DS", "DS3", "DS4", "DS5", "DS5 LS", "DS6 WR", "GS", "GS Birotor", "GSA",
                "ID", "Jumpy", "Jumper", "LN", "LNA", "Méhari", "Saxo", "SM", "Traction Avant", "Visa", "XM", "Xantia", "Xsara", "ZX" }
            },
            { "Fiat", new [] {
                "124", "125", "131", "500", "600", "Croma", "Punto", "Ritmo", "Tipo", "Uno", "X1/9" }
            },
            { "Ford", new [] {
                "Cortina", "Fairlane", "Focus", "Escort", "Granada", "Scorpio", "Sierra", "Taunus", "Zephyr" }
            },
            { "Opel", new [] {
                "Admiral", "Agila", "Ampera", "Antara", "Ascona", "Astra", "Blitz", "Calibra", "Campo", "Combo", "Commodore", "Corsa",
                "Diplomat", "Frontera", "GT", "Insignia", "Kadett", "Kapitän", "Laubfrosch", "Manta", "Meriva", "Monterey", "Monza",
                "Olympia", "Omega", "Rekord", "Senator", "Signum", "Sintra", "Speedster", "Tigra", "Vectra", "Vivaro", "Zafira" }
            },
            { "Porsche", new [] {
                "911", "914", "924", "928", "944", "Boxter", "Carrera GT", "Cayenne" }
            },
            { "Lada", new [] {
                "Granta", "Largus Cross", "Niva" }
            },
            { "Peugeot", new [] {
                "1007", "104", "106", "107", "203", "204", "205", "206", "207", "208", "3008", "304", "305", "306", "307", "308",
                "309", "4007", "403", "404", "405", "406", "407", "5008", "504", "505", "508", "604", "605", "607", "806", "807" }
            },
            { "Renault", new [] {
                "Clio", "Espace", "Kangoo", "Megane", "R4", "R5", "R8", "R14", "R16" }
            },
            { "Volvo", new [] {
                "144", "164", "240", "340", "360", "740", "Amazon", "S40", "S60", "V70", "XC90" }
            },
            { "Volkswagen", new [] {
                "Bora", "Caddy", "Caravelle", "Corado", "Golf", "Jetta", "Lupo", "New Beetle", "Polo", "Passat", "Typ 1" }
            }
        };

        private static readonly IReadOnlyDictionary<string, string[]> _motorcycles = new Dictionary<string, string[]> {
            { "Harley-Davidson", new [] {
                "FL Hydra Glide", "FL Duo Glide", "FLH Electra Glide", "FLHS Electra Glide Sport", "Low Rider FXS",
                "Fat Bob FXEF", "Sturgis FXB", "Street Bob FXDB", "Softail Deuce", "Night Train", "Cross Bones" }
            },
            { "Husqvarna", new [] {
                "Modell 120", "Modell 160", "Modell 22 Änglavinge", "Modell 24 Svartqvarna", "Modell 25", "Modell 27 Rödqvarna",
                "Modell 281 Drömbågen", "Modell 282 Silverpilen", "Modell 283 Guldpilen", "Modell 30", "Modell 301", "Modell 32",
                "Modell 35", "Modell 610", "Modell 65 MotoReve" }
            },
            { "Norton", new [] {
                "Classic", "Commander", "F1 Sport", "F1", "Interpol 2", "NRS588", "RC588", "RCW588" }
            },
            { "Suzuki", new [] {
                "GS1000", "GS500", "GSX1100", "GSX550", "GSX750", "GT380", "GT550", "GT750" }
            },
            { "Yamaha", new [] {
                "DT125", "MT-01", "MT-03", "MT-07", "RD250", "RD350", "SR250", "SR400", "SR500", "TDR 250", "TZR 125", "TZR 250",
                "XJR1200", "XJR1300", "XJR400" }
            }
        };

        private static readonly string[] _colors = new[] {
            "akvamarin", "aprikos", "babyblå", "becksvart", "beige", "blodröd", "blå", "blåklintsblå", "blåvit", "brandgul", "brun",
            "bärnstensgul", "cerise", "citrongul", "cyan", "fuchsia", "gredelin", "gräddvit", "grå", "grön", "gul", "gyllenbrun",
            "havsblå", "indigo", "isabellafärgad", "isblå", "jeansblå", "kaffebrun", "kaffefärgad", "kaki", "karmosinröd",
            "kastanjefärgad", "knallröd", "koboltblå", "kornblå", "lila", "lingul", "ljusblå", "ljusgul", "ljusröd", "marinblå",
            "mint", "mörkblå", "mörkgrön", "mörklila", "mörkrosa", "olivgrön", "orange", "purpur", "rosa", "röd", "scharlakansröd",
            "skär", "smaragdgrön", "snövit", "solgul", "svart", "turkos", "vinröd", "viol", "violett", "vit", "äggul"
        };

        // A-Z but not I,V and Q following the standards for Swedish vehicle registration numbers.
        private static readonly string _regNumChars = "ABCDEFGHJKLMNOPRSTUWXYZ";

        private static readonly string[][] _firstNames = {
            new [] {
                "Agnes", "Alice", "Alicia", "Alma", "Alva", "Amanda", "Amelia", "Anna", "Astrid", "Celine", "Cornelia", "Ebba", "Edith",
                "Elin", "Elina", "Elisa", "Elise", "Ella", "Ellen", "Ellie", "Ellinor", "Elsa", "Elvira", "Emelie", "Emilia", "Emma",
                "Emmy", "Ester", "Felicia", "Filippa", "Freja", "Greta", "Hanna", "Hedda", "Hilda", "Hilma", "Ida", "Ines", "Ingrid",
                "Iris", "Isabella", "Isabelle", "Jasmine", "Jenny", "Joline", "Julia", "Juni", "Klara", "Leah", "Leia", "Lilly", "Linn",
                "Linnea", "Lisa", "Liv", "Livia", "Lotta", "Lova", "Lovis", "Lovisa", "Luna", "Lykke", "Maja", "Majken", "Maria",
                "Matilda", "Meja", "Melissa", "Minna", "Mira", "Moa", "Molly", "My", "Märta", "Nathalie", "Nellie", "Nicole", "Nora",
                "Nova", "Novalie", "Olivia", "Ronja", "Rut", "Saga", "Sally", "Sara", "Selma", "Signe", "Sigrid", "Siri", "Sofia",
                "Stella", "Stina", "Svea", "Thea", "Tilda", "Tilde", "Tindra", "Tuva", "Tyra", "Vera", "Victoria", "Wilma"
            },
            new [] {
                "Adam", "Adrian", "Albin", "Alex", "Alexander", "Alfred", "Ali", "Alvin", "Anders", "Anton", "Aron", "Arvid", "August",
                "Axel", "Benjamin", "Carl", "Casper", "Charlie", "Colin", "Daniel", "Dante", "David", "Ebbe", "Eddie", "Edward", "Edvin",
                "Elias", "Elis", "Elliot", "Elton", "Elvin", "Emil", "Erik", "Felix", "Filip", "Frank", "Gabriel", "Gustav", "Hampus",
                "Harry", "Henry", "Hjalmar", "Hugo", "Isak", "Ivar", "Jack", "Jacob", "Joel", "John", "Jonathan", "Josef", "Julian",
                "Kalle", "Kevin", "Leo", "Leon", "Liam", "Linus", "Loke", "Loui", "Love", "Lucas", "Ludvig", "Malte", "Matteo", "Max",
                "Maximilian", "Melker", "Melvin", "Milo", "Milton", "Mio", "Mohamed", "Neo", "Nils", "Noah", "Noel", "Oliver", "Olle",
                "Oscar", "Otto", "Rasmus", "Sam", "Samuel",  "Sebastian", "Sigge", "Simon", "Sixten", "Svante", "Tage", "Theo", "Theodor",
                "Valter", "Vidar", "Viggo", "Viktor", "Vilgot", "Wilhelm", "Ville", "William", "Wilmer", "Vincent"
            }
        };

        private static readonly string[] _lastNames = new[] {
            "Abrahamsson", "Ali", "Andersson", "Andreasson", "Arvidsson", "Axelsson", "Bengtsson", "Berg", "Berggren", "Berglund",
            "Bergman", "Bergqvist", "Bergström", "Björk", "Björklund", "Blom", "Blomqvist", "Claesson", "Dahlberg", "Danielsson", "Ek",
            "Eklund", "Ekström", "Eliasson", "Engström", "Eriksson", "Falk", "Forsberg", "Fransson", "Fredriksson", "Gran", "Gunnarsson",
            "Gustafsson", "Hansen", "Hansson", "Hedlund", "Hellström", "Henriksson", "Hermansson", "Holm", "Holmberg", "Holmgren",
            "Holmqvist", "Håkansson", "Isaksson", "Jakobsson", "Jansson", "Johansson", "Jonasson", "Jonsson", "Jönsson", "Karlsson",
            "Larsson", "Lind", "Lindberg", "Lindgren", "Lindholm", "Lindqvist", "Lindström", "Lund", "Lundberg", "Lundgren", "Lundin",
            "Lundqvist", "Lundström", "Löfgren", "Magnusson", "Martinsson", "Mattsson", "Mohamed", "Månsson", "Mårtensson", "Nilsson",
            "Norberg", "Nordin", "Nordström", "Nyberg", "Nyström", "Olofsson", "Olsson", "Persson", "Pettersson", "Pålsson", "Samuelsson",
            "Sandberg", "Sandström", "Sjöberg", "Sjögren", "Skog", "Ström", "Strömberg", "Sundberg", "Sundström", "Svensson", "Söderberg",
            "Wallin", "Viklund", "Wikström", "Åberg", "Åkesson", "Åström", "Öberg"
        };
    }
}
