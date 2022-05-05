using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FlaskeAutomat
{
    public class Bottle
    {
        static int counter = 0;
        private string name;

        Random random = new Random();
        static Queue<Bottle> bottleQueue = new Queue<Bottle>();
        static Queue<Bottle> beerQueue = new Queue<Bottle>();
        static Queue<Bottle> sodaQueue = new Queue<Bottle>();


        public string Name
        {
            get { return name; }
            set { name = value; }
        }


        public Queue<Bottle> BottleQueue
        {
            get { return bottleQueue; }
            set { bottleQueue = value; }
        }

        public Queue<Bottle> SodaQueue
        {
            get { return sodaQueue; }
            set { sodaQueue = value; }
        }

        public Queue<Bottle> BeerQueue
        {
            get { return beerQueue; }
            set { beerQueue = value; }
        }



        public Bottle()
        {

        }

        public Bottle(string name)
        {
            this.name = name;
            counter++;

        }

        public void ProduceBottles()
        {
            while (true)
            {
                Console.WriteLine("Entering ProduceBottles");

                Monitor.Enter(bottleQueue);

                if (bottleQueue.Count < 5)
                {
                   

                    for (int i = 0; i < 10; i++)
                    {



                        if (random.Next(1, 11) > 5)
                        {
                            Bottle b = new Bottle("Tuborg" + counter);
                            bottleQueue.Enqueue(b);
                            Console.WriteLine($"{b.Name} has entered the BottleQueue");
                        }
                        else
                        {
                            Bottle b = new Bottle("Cola" + counter);
                            bottleQueue.Enqueue(b);
                            Console.WriteLine($"{b.Name} has entered the BottleQueue");
                        }
  
                    }
                    Monitor.PulseAll(bottleQueue);
                }
                else
                {
                    Console.WriteLine("ProducingBottles is waiting");
                    Monitor.Wait(bottleQueue);
                }

                Monitor.Exit(bottleQueue);
                Thread.Sleep(2000);
            }
        }

        public void SplitBottles()
        {
            while (true)
            {
                Console.WriteLine("Entering SplitBottles");
                Monitor.Enter(bottleQueue);

                if (bottleQueue.Count > 0)
                {
                  
                    foreach (var bottle in bottleQueue)
                    {

                        if (bottle.name.Contains("Tuborg"))
                        {

                            beerQueue.Enqueue(bottle);
                        }
                        else
                        {
                            sodaQueue.Enqueue(bottle);
                        }

                        
                    }
                    Console.WriteLine($"SodaQueue: {sodaQueue.Count}");
                    foreach (var bottle in sodaQueue)
                    {
                        Console.WriteLine(bottle.name);
                    }
                    Console.WriteLine($"BeerQueue: {beerQueue.Count}");
                    foreach (var bottle in beerQueue)
                    {
                        Console.WriteLine(bottle.name);
                    }
                    bottleQueue.Clear();
                    Console.WriteLine("Cleared BottleQueue");
                    Monitor.PulseAll(bottleQueue);
                }
                else
                {
                    Console.WriteLine("SplittingBottles is waiting.");
                    Monitor.Wait(bottleQueue);
                }

                Monitor.Exit(bottleQueue);
                Thread.Sleep(2000);
            }
        }
    }
}

