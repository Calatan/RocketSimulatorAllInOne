using RocketSimulator.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using static System.Math;
using static System.Console;

namespace RocketSimulatorAllInOne
{
    class Program
    {
        static Rocket[] rocketList = new Rocket[7];

        static void Main(string[] args)
        {
            Console.SetWindowSize(120, 30);

            bool shouldNotExit = true;

            uint rocketListCurrentIndexPosition = 0;

            int seconds = 0;

            while (shouldNotExit)
            {
                WriteLine("   MAIN MENU");
                WriteLine("");
                WriteLine("1. Add rocket");
                WriteLine("2. List rockets");
                WriteLine("3. Simulate speed after time period");
                WriteLine("4. Display velocity over time period");
                WriteLine("5. Exit");
                WriteLine("");
                WriteLine("");
                WriteLine("Instructions:");
                WriteLine("Add one or several rockets.");
                WriteLine("Only one rocket of each type may be added.");
                WriteLine("You must add at least one rocket before you can simulate speed.");
                WriteLine("You must simulate speed before displaying velocity over time.");
                WriteLine("");
                WriteLine("");
                WriteLine("<Choose any number from the MAIN MENU to continue>");
                ConsoleKeyInfo keyPressed = ReadKey(true);

                Clear();

                switch (keyPressed.Key)
                {
                    case ConsoleKey.D1:
                    case ConsoleKey.NumPad1:
                        {
                            WriteLine("    CHOOSE ROCKET TO ADD: ");
                            WriteLine("");
                            WriteLine("1 - Archytas' Flying Pigeon");           // Able to fly about 200 meters before it ran out of steam.
                            WriteLine("2 - Goddard's Auburn rocket");           // It attained a height of 41 feet in 2.5 seconds, and it came to rest 184 feet from the launch pad.
                            WriteLine("3 - Goddard's Rosswell rocket");         // Goddard fired an 11 foot liquid fueled rocket, to a height of 2000 feet at a speed of 500 miles per hour.
                            WriteLine("4 - Bell X-1");                          // Achieved a speed of nearly 1,000 miles per hour (1,600 km/h; 870 kn)
                            WriteLine("5 - Navy Viking 7");                     // Set the new altitude record for single stage rockets by reaching 136 miles and a speed of 4,100 mph.
                            WriteLine("6 - Jupiter-C");                         // To an altitude of 680 mi (1,100 km), a speed of 16,000 mph (7 km/s), and a range of 3,300 mi (5,300 km).
                            WriteLine("7 - Saturn V");                          // The first stage burned for about 2 minutes and 41 seconds, lifting the rocket to an altitude of 42 miles (68 km) and a speed of 6,164 miles per hour (2,756 m/s) and burning 4,700,000 pounds (2,100,000 kg) of propellant.  S-II second stage burned for 6 minutes and propelled the craft to 109 miles (175 km) and 15,647 mph (6,995 m/s), close to orbital velocity. During Apollo 11, a typical lunar mission, the third stage burned for about 2.5 minutes until first cutoff at 11 minutes 40 seconds. At this point it was 1,430 nautical miles (2,650 km)  downrange and in a parking orbit at an altitude of 103.2 nautical miles (191.1 km)  and velocity of 17,432 mph (7,793 m/s).
                            WriteLine("");
                            WriteLine("");
                            WriteLine("<Choose any number from the list to continue>");

                            //Genererar ett hexadecimalt slumptal på exakt sju tecken som används som registry
                            var r = new Random();
                            int A = r.Next(16777216, 268435455);
                            string registry = A.ToString("X");

                            keyPressed = ReadKey(true);

                            Rocket newRocket = null;

                            if (keyPressed.Key == ConsoleKey.D1)
                            {
                                newRocket = new FlyingPigeon(registry);
                            }
                            else if (keyPressed.Key == ConsoleKey.D2)
                            {
                                newRocket = new AuburnRocket(registry);
                            }
                            else if (keyPressed.Key == ConsoleKey.D3)
                            {
                                newRocket = new RosswellRocket(registry);
                            }
                            else if (keyPressed.Key == ConsoleKey.D4)
                            {
                                newRocket = new BellX(registry);
                            }
                            else if (keyPressed.Key == ConsoleKey.D5)
                            {
                                newRocket = new NavyViking(registry);
                            }
                            else if (keyPressed.Key == ConsoleKey.D6)
                            {
                                newRocket = new JupiterC(registry);
                            }
                            else if (keyPressed.Key == ConsoleKey.D7)
                            {
                                newRocket = new SaturnV(registry);
                            }

                            Clear();

                            string name = newRocket.Brand;

                            Rocket theRocket = SearchRocketByName(name);

                            if (theRocket != null)
                            {
                                WriteLine("Rocket already added");
                                Thread.Sleep(2000);
                            }
                            else
                            {
                                rocketList[rocketListCurrentIndexPosition++] = newRocket;
                                WriteLine("Rocket added");
                                Thread.Sleep(2000);
                            }
                        }
                        break;

                    case ConsoleKey.D2:
                    case ConsoleKey.NumPad2:
                        {
                            WriteLine("LIST OF ROCKETS ADDED:");
                            WriteLine("");
                            
                            string nameHeader = "Name".PadRight(30, ' ');
                            string yearHeader = "Year".PadRight(10, ' ');

                            Write(nameHeader);
                            WriteLine(yearHeader);

                            WriteLine("-------------------------------------");

                            foreach (Rocket rocket in rocketList)
                            {
                                if (rocket == null) continue;

                                string brand = rocket.Brand.PadRight(30, ' ');
                                string model = rocket.Model.PadRight(10, ' ');

                                Write(brand);
                                WriteLine(model);
                            }

                            WriteLine("");
                            WriteLine("");
                            WriteLine("<Press any key to continue>");
                            ReadKey(true);
                        }
                        break;

                    case ConsoleKey.D3:
                    case ConsoleKey.NumPad3:

                        Write("Engine burn period (sec): ");
                        seconds = int.Parse(ReadLine());

                        Clear();

                        WriteLine("Engine burn period (sec): " + seconds);
                        WriteLine("");

                        string empty = " ".PadRight(45, ' ');
                        string velocityHeaderSimulation = "Velocity".PadRight(10, ' ');
                        string fuelLeftHeaderSimulation = "Fuel left";
                        string nameHeaderSimulation = "Name".PadRight(30, ' ');
                        string yearHeaderSimulation = "Year".PadRight(15, ' ');
                        string velocityUnitHeaderSimulation = "(km/h)".PadRight(10, ' ');
                        string fuelLeftUnitHeaderSimulation = "(kg)";

                        Write(empty);
                        Write(velocityHeaderSimulation);
                        WriteLine(fuelLeftHeaderSimulation);
                        Write(nameHeaderSimulation);
                        Write(yearHeaderSimulation);
                        Write(velocityUnitHeaderSimulation);
                        WriteLine(fuelLeftUnitHeaderSimulation);

                        WriteLine("--------------------------------------------------------------------------------------");

                        foreach (Rocket rocket in rocketList)
                        {
                            if (rocket == null) continue;

                            rocket.Accelerate(seconds);

                            string brand = rocket.Brand.PadRight(30, ' ');
                            string model = rocket.Model.PadRight(15, ' ');
                            string velocity = rocket.Velocity.ToString().PadRight(10, ' ');
                            int fuelLeft = rocket.FuelLeft;

                            Write(brand);
                            Write(model);
                            Write(velocity);
                            WriteLine(fuelLeft);
                        }

                        WriteLine("");
                        WriteLine("");
                        WriteLine("<Press any key to continue>");
                        ReadKey(true);

                        break;

                    case ConsoleKey.D4:
                    case ConsoleKey.NumPad4:

                        int counterRockets = 1;

                        //Räknar antalet tillagda raketer (rockets i rocketList)
                        int numberOfRockets = 0;
                        foreach (var item in rocketList)
                        {
                            if (item != null)
                            {
                                numberOfRockets++;
                            }
                        }

                        bool shouldDisplay = true;

                        while (shouldDisplay == true)
                        {
                            foreach (Rocket rocket in rocketList)
                            {
                                if (rocket == null) continue;

                                string brand = rocket.Brand;
                                string model = rocket.Model;
                                string info = rocket.Info;

                                Clear();

                                WriteLine("");
                                WriteLine("                           Displaying velocity over " + seconds + " seconds for " + brand + " (" + model + ")");
                                WriteLine("                            - " + info);
                                WriteLine("     km/h");
                                WriteLine("       ^");
                                WriteLine("       |");
                                WriteLine("       |");
                                WriteLine("       |");
                                WriteLine("       |");
                                WriteLine("       |");
                                WriteLine("       |");
                                WriteLine("       |");
                                WriteLine("       |");
                                WriteLine("       |");
                                WriteLine("       |");
                                WriteLine("       |");
                                WriteLine("       |");
                                WriteLine("       |");
                                WriteLine("       |");
                                WriteLine("       |");
                                WriteLine("       |");
                                WriteLine("       |");
                                WriteLine("       |");
                                WriteLine("       |");
                                WriteLine("       |");
                                WriteLine("        ----------------------------------------------------------------------------------------------------> Seconds");
                                WriteLine("");
                                WriteLine("");
                                WriteLine("       ESC: Main menu                                   (" + counterRockets + "/" + numberOfRockets + ")                                         (>) Next rocket");

                                int sMax = seconds;
                                float sPart = (((float)(sMax)) / 16);

                                float[] coordinatesSeconds = new float[17];
                                int[] coordinatesVelocity = new int[17];

                                // Skapar värden för x-axeln
                                int i = 1;
                                while (i < coordinatesSeconds.Length)
                                {
                                    coordinatesSeconds[i++] = sPart;
                                    sPart = sPart + (((float)(sMax)) / 16);
                                }

                                //Skapar värden för y-axel 
                                i = 0;
                                foreach (int value in coordinatesSeconds)
                                {
                                    rocket.Accelerate(value);
                                    int velocity = rocket.Velocity;
                                    coordinatesVelocity[i++] = velocity;
                                }

                                //Hittar maximala hastigheten vMax för att kunna skala om värden för y-axeln
                                int vMax = coordinatesVelocity[0];
                                for (int a = 0; a < coordinatesVelocity.Length; a++)
                                {
                                    if (vMax < Math.Abs(coordinatesVelocity[a]))
                                        vMax = Math.Abs(coordinatesVelocity[a]);
                                }

                                //Förhindrar division med 0 i de fall vMax är 0 (kan hända t ex då 'Engine burn period' är stor och sPart blir större än tiden en raket faktiskt rör sig)
                                if (vMax == 0)
                                {
                                    vMax = 1;
                                }

                                //Skriver ut datapunkter
                                i = 0;
                                while (i < coordinatesSeconds.Length)
                                {
                                    SetCursorPosition(7 + (6 * i), 25 - ((coordinatesVelocity[i] * 20) / vMax));
                                    Write("¤");
                                    i++;
                                }

                                //Skriver ut skala för y-axel
                                SetCursorPosition(1, 5);
                                Write("{0,5}", vMax);
                                SetCursorPosition(1, 10);
                                Write("{0,5}", ((3 * vMax) / 4));
                                SetCursorPosition(1, 15);
                                Write("{0,5}", (vMax / 2));
                                SetCursorPosition(1, 20);
                                Write("{0,5}", (vMax / 4));
                                SetCursorPosition(1, 25);
                                Write("{0,5}", "0");

                                //Skriver ut skala för x-axel
                                SetCursorPosition(2, 26);
                                i = 0;
                                foreach (var time in coordinatesSeconds)
                                {
                                    int tick = (int)coordinatesSeconds[i];
                                    Write("{0,6}", tick);
                                    i++;
                                }

                                //Gör så att toppen på konsollfönstret visas, även om det plottas negativa värden på hastighet
                                SetCursorPosition(0, 0);
                                SetCursorPosition(0, 29);

                                keyPressed = ReadKey(true);

                                if (keyPressed.Key == ConsoleKey.RightArrow && counterRockets != numberOfRockets)
                                {
                                    counterRockets++;
                                    continue;
                                }
                                else if (keyPressed.Key == ConsoleKey.Escape)
                                {
                                    shouldDisplay = false;
                                }
                                else
                                {
                                    shouldDisplay = false;
                                }
                                shouldDisplay = false;
                            }
                            shouldDisplay = false;
                        }

                        ReadKey(true);

                        break;

                    case ConsoleKey.D5:
                    case ConsoleKey.NumPad5:

                        shouldNotExit = false;

                        break;
                }

                Clear();
            }
        }

        private static Rocket SearchRocketByName(string name)
        {
            Rocket rocketReferenceToReturn = null;

            foreach (Rocket rocketName in rocketList)
            {
                if (rocketName == null) continue;

                if (rocketName.Brand == name)
                {
                    rocketReferenceToReturn = rocketName;
                    break;
                }
            }
            return rocketReferenceToReturn;
        }
    }
}

namespace RocketSimulator.Domain
{
    class FlyingPigeon : Rocket
    {
        public FlyingPigeon(string registry)
            : base("Archytas' Flying Pigeon", " 350 BC", registry, "a small device suspended on a horizontal wire and propelled by steam")
        {

        }

        public override void Accelerate(float seconds)
        {
            if (seconds <= 7.5)
            {
                Velocity = (int)((15 * seconds - (seconds * seconds)) * 0.01 * 3.6);
                FuelLeft = (int)(1 - ((1 / 7.5) * seconds));
            }
            else if (seconds <= 15)
            {
                Velocity = (int)((15 * seconds - (seconds * seconds)) * 0.01 * 3.6);
                FuelLeft = 0;
            }
            else
            {
                Velocity = 0;
                FuelLeft = 0;
            }
        }
    }
}
namespace RocketSimulator.Domain
{
    class AuburnRocket : Rocket
    {
        public AuburnRocket(string registry)
            : base("Goddard's Auburn rocket", "1926 AD", registry, "a tiny rocket fired vertically, hence negative velocity when falling back to the ground")
        {

        }

        public override void Accelerate(float seconds)
        {
            if (seconds < 2.5)
            {
                Velocity = (int)((14 * seconds - (6 * seconds * seconds)) * 3.6);
                FuelLeft = (int)(5 - (2 * seconds));
            }
            else if (seconds < 3.5)
            {
                Velocity = (int)((14 * seconds - (6 * seconds * seconds)) * 3.6);
                FuelLeft = 0;
            }
            else
            {
                Velocity = 0;
                FuelLeft = 0;
            }
        }
    }
}

namespace RocketSimulator.Domain
{
    class RosswellRocket : Rocket
    {
        public RosswellRocket(string registry)
            : base("Goddard's Rosswell rocket", "1937 AD", registry, "a small rocket fired vertically, hence negative velocity when falling back to the ground")
        {

        }

        public override void Accelerate(float seconds)
        {
            if (seconds < 21.429)
            {
                Velocity = (int)(-1 * ((120 * seconds * ((3.5 * seconds) - 50)) / 100) * 3.6);
                FuelLeft = (int)((21.429 * 7.5) - (7.5 * seconds));
            }
            else
            {
                Velocity = 0;
                FuelLeft = 0;
            }

        }
    }
}

namespace RocketSimulator.Domain
{
    class BellX : Rocket
    {
        public BellX(string registry)
            : base("Bell X-1", "1947 AD", registry, "the first airplane (rocket-engine–powered) to exceed the speed of sound in level flight")
        {

        }

        public override void Accelerate(float seconds)
        {
            if (seconds < 62.5)
            {
                Velocity = (int)(seconds * 25.6);
                FuelLeft = (int)(2500 - (40 * seconds));
            }
            else if (seconds < 125)
            {
                Velocity = (int)((62.5 * 25.6) - ((seconds - 62.5) * 25.6));
                FuelLeft = 0;
            }
            else
            {
                Velocity = 0;
                FuelLeft = 0;
            }
        }
    }
}

namespace RocketSimulator.Domain
{
    class NavyViking : Rocket
    {
        public NavyViking(string registry)
            : base("Navy Viking 7", "1951 AD", registry, "the first sounding rocket to measure density and winds in the upper atmosphere")
        {

        }

        public override void Accelerate(float seconds)
        {
            if (seconds < 100)
            {
                Velocity = (int)(seconds * 18.3 * 3.6);
                FuelLeft = (int)(5000 - (50 * seconds));
            }
            else if (seconds < 286.73)
            {
                Velocity = (int)((100 * 18.3 * 3.6) - ((seconds  - 100) * 9.8));
                FuelLeft = 0;
            }
            else
            {
                Velocity = 0;
                FuelLeft = 0;
            }
        }
    }
}

namespace RocketSimulator.Domain
{
    class JupiterC : Rocket
    {
        public JupiterC(string registry)
            : base("Jupiter-C", "1957 AD", registry, "the first rocket used by the US to heft a satellite into space (Explorer 1)")
        {

        }

        public override void Accelerate(float seconds)
        {
            if (seconds < 156)
            {
                Velocity = (int)(seconds * 45 * 3.6);
                FuelLeft = (int)(25000 - (160 * seconds));
            }
            else
            {
                Velocity = (int)(156 * 45 * 3.6);
                FuelLeft = 0;
            }
        }
    }
}

namespace RocketSimulator.Domain
{
    class SaturnV : Rocket
    {
        public SaturnV(string registry)
            : base("Saturn V", "1969 AD", registry, "a super heavy-lift launch vehicle used to send the Apollo missions to the Moon")
        {

        }

        public override void Accelerate(float seconds)
        {
            if (seconds < 162)
            {
                Velocity = (int)(((((Math.Pow(Math.E, (seconds / 22)) - 1) / 0.5) + (7.5 * seconds)) / 2) * 3.6);
                FuelLeft = (int)(2100000 - (13000 * seconds));
            }
            else
            {
                Velocity = (int)(((((Math.Pow(Math.E, (162 / 22)) - 1) / 0.5) + (15 * 162)) / 2) * 3.6);
                FuelLeft = 0;
            }
        }
    }
}

namespace RocketSimulator.Domain
{
    abstract class Rocket
    {
        public Rocket(string brand, string model, string registry, string info)
        {
            Brand = brand;
            Model = model;
            Registry = registry;
            Info = info;
        }

        public string Brand { get; }

        public string Model { get; }

        private string registry;
        public string Info { get; }

        public string Registry
        {
            get
            {
                return registry;
            }
            set
            {
                if (value.Length > 7)
                {
                    registry = value.Substring(0, 7);
                }
                else
                {
                    registry = value;
                }
            }
        }

        public int Velocity { get; protected set; }
        public int FuelLeft { get; protected set; }

        public abstract void Accelerate(float seconds);
    }
}