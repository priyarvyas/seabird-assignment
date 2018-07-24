using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using EngineeringService;

namespace EngineeringService
{

    //ITarget interface
    public interface IAircraft
    {
        bool Airborne { get; }
        void TakeOff();
        void IncreaseHeight();
        void DecreaseHeight();
        void RaiseNose();
        void LowerNose();
        int Height { get; }
    }

    // Target 
    public sealed class Aircraft : IAircraft
    {
        int height;
        bool airborne;
        public Aircraft()
        {
            height = 0;
            airborne = false;
        }

        public void TakeOff()
        {
            Console.WriteLine("Aircraft engine takeoff");
            airborne = true;
            height = 200; //metres
        }

        public void RaiseNose()
        {
            Console.WriteLine("Aircraft nose raised");
        }

        public void LowerNose()
        {
            Console.WriteLine("Aircraft nose lowered");
        }

        public void IncreaseHeight()
        {
            height = height + 100;
        }

        public void DecreaseHeight()
        {
            while (height > 0)
                height = height - 100;
        }

        public bool Airborne
        {
            get { return airborne; }
        }

        public int Height
        {
            get { return height; }
        }
    }
}  // end of EngineeringService

//Adaptee interface
public interface ISeacraft
{
    int Speed { get; }
    void IncreaseRevs();
    void DecreaseRevs();
}

// Adaptee   
public class Seacraft : ISeacraft
{
    int speed = 0;

    public virtual void IncreaseRevs()
    {
        speed += 10;
        Console.WriteLine("Seacraft engine increases revs to " + speed + " knots");
    }

    public virtual void DecreaseRevs()
    {
        while (speed > 0)
        {
            speed = speed - 10;
            Console.WriteLine("Seacraft engine decrease revs to " + speed + " knots");
        }       
    }

    public int Speed
    {
        get { return speed; }
    }
}

//Adapter
public class Seabird : Seacraft, IAircraft
{
    int height = 0;
    // A two-way adapter hides and routes the Target's methods
    //  Use Seacraft instructions to implement this one 
    public void TakeOff()
    {
        while (!Airborne)
            IncreaseRevs();
    }

    //Routes this straight back to the Aircraft
    public int Height
    {
        get { return height; }
    }

    public void IncreaseHeight()
    {
        height = height + 100;
    }
    // This method is common to both Target and Adaptee
    public override void IncreaseRevs()
    {
        base.IncreaseRevs();
        if (Speed > 40)
            height += 100;
    }

    public void RaiseNose()
    {
        while (!Airborne)
            Console.WriteLine("Seabird Raises the nose");
    }

    public void LowerNose()
    {
        while (!Airborne)
            Console.WriteLine("Seabird Lowers the nose");
    }

    public void DecreaseHeight()
    {
        while (Airborne)
            height = height - 100;
        base.DecreaseRevs();
    }

    public bool Airborne
    {
        get { return height > 50; }
    }
}

class question2
{
    static void Main()
    {
        // No adapter
        Console.WriteLine("Experiment 1: test the aircraft engine:");
        IAircraft aircraft = new Aircraft();
        IAircraft seabird = new Seabird();
        (seabird as ISeacraft).IncreaseRevs();
        (seabird as ISeacraft).IncreaseRevs();
        (seabird as ISeacraft).IncreaseRevs();
        (seabird as ISeacraft).IncreaseRevs();
        (seabird as ISeacraft).IncreaseRevs();
        

        Console.WriteLine("\nExperiment 2: Increase the speed of the Seabird, Aircraft engine takeoff");
        (seabird as ISeacraft).IncreaseRevs();
        (seabird as ISeacraft).IncreaseRevs();
        seabird.RaiseNose();
        aircraft.TakeOff();
        if (aircraft.Airborne)
            Console.WriteLine("The aircraft engine is fine, flying at " + aircraft.Height + " metres");

        Console.WriteLine("\nExperiment 3: Increase height of the Seabird");
        seabird.IncreaseHeight();
        if (seabird.Airborne)
            Console.WriteLine("Seabird flying at height " + seabird.Height +
                        " metres and speed " + (seabird as ISeacraft).Speed + " knots");
        seabird.IncreaseHeight();
        if (seabird.Airborne)
            Console.WriteLine("Seabird flying at height " + seabird.Height +
                        " metres and speed " + (seabird as ISeacraft).Speed + " knots");
        seabird.IncreaseHeight();
        if (seabird.Airborne)
            Console.WriteLine("Seabird flying at height " + seabird.Height +
                        " metres and speed " + (seabird as ISeacraft).Speed + " knots");


        // Two-way adapter: using seacraft instructions on an IAircraft object
        // (where they are not in the IAricraft interface)
        Console.WriteLine("\nExperiment 4: Prepare for landing");
        seabird.LowerNose();
        while (seabird.Airborne)
            seabird.DecreaseHeight();

        Console.WriteLine("Seabird is at height " + seabird.Height +
                        " metres and speed " + (seabird as ISeacraft).Speed + " knots. Landed Successfully!");
        Console.ReadKey();
    }
}

/* Output
Experiment 1: test the aircraft engine:
Seacraft engine increases revs to 10 knots
Seacraft engine increases revs to 20 knots
Seacraft engine increases revs to 30 knots
Seacraft engine increases revs to 40 knots
Seacraft engine increases revs to 50 knots

Experiment 2: Increase the speed of the Seabird, Aircraft engine takeoff
Seacraft engine increases revs to 60 knots
Seacraft engine increases revs to 70 knots
Aircraft engine takeoff
The aircraft engine is fine, flying at 200 metres

Experiment 3: Increase height of the Seabird
Seabird flying at height 400 metres and speed 70 knots
Seabird flying at height 500 metres and speed 70 knots
Seabird flying at height 600 metres and speed 70 knots

Experiment 4: Prepare for landing
Seacraft engine decrease revs to 60 knots
Seacraft engine decrease revs to 50 knots
Seacraft engine decrease revs to 40 knots
Seacraft engine decrease revs to 30 knots
Seacraft engine decrease revs to 20 knots
Seacraft engine decrease revs to 10 knots
Seacraft engine decrease revs to 0 knots
Seabird is at height 0 metres and speed 0 knots. Landed Successfully!
*/
