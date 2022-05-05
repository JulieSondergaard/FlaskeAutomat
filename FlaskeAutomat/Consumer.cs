using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FlaskeAutomat
{
    class Consumer
    {

        Bottle bottle;

        public Consumer(Bottle bottle)
        {
            this.bottle = bottle;
        }

        public void ConsumeBeers()
        {
            while (true)
            {
                Console.WriteLine("Entering beer queue");
                Monitor.Enter(bottle.BottleQueue);

                if (bottle.BeerQueue.Count > 0)
                {
                    Console.WriteLine("Entering while in beer queue");
                    foreach (Bottle b in bottle.BeerQueue)
                    {
                        Console.WriteLine($"{b.Name} has been consumed.");
                    }
                    bottle.BeerQueue.Clear();
                    Console.WriteLine($"Cleared beer queue {bottle.BeerQueue.Count} ");
                    Monitor.PulseAll(bottle.BottleQueue);
                }
                else
                {
                    Console.WriteLine("ConsumeBeers is wating");
                    Monitor.Wait(bottle.BottleQueue);
                }

                Monitor.Exit(bottle.BottleQueue);
                Thread.Sleep(2000);
            }
        }


        public void ConsumeSodas()
        {
            while (true)
            {
                Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
                // Entering the lock of BottleQueue
                Monitor.Enter(bottle.BottleQueue);


                if (bottle.SodaQueue.Count > 0)
                {
                    
                    foreach (Bottle b in bottle.SodaQueue)
                    {
                        Console.WriteLine($"{b.Name} has been consumed.");
                    }
                    bottle.SodaQueue.Clear();
                    Console.WriteLine($"Cleared soda queue {bottle.SodaQueue.Count} ");
                    Monitor.PulseAll(bottle.BottleQueue);

                }
                else
                {
                 //   ConsumeSoda is waiting 
                    Monitor.Wait(bottle.BottleQueue);
                }

                Monitor.Exit(bottle.BottleQueue);
                Thread.Sleep(2000);
            }
        }


    }
}
