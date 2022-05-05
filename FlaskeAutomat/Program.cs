using System;
using System.Threading;

namespace FlaskeAutomat
{
    class Program
    {
        
        static void Main(string[] args)
        {
            
            Bottle producer = new Bottle();
            Consumer consumer = new Consumer(producer);

            Thread producingBottles = new Thread(producer.ProduceBottles);
            Thread splittingBottles = new Thread(producer.SplitBottles);

            Thread consumingBeers = new Thread(consumer.ConsumeBeers);
            Thread consumingSodas = new Thread(consumer.ConsumeSodas);
            Thread consumingSodas1 = new Thread(consumer.ConsumeBeers);
            Thread consumingSodas2 = new Thread(consumer.ConsumeSodas);
            Thread consumingSodas3 = new Thread(consumer.ConsumeSodas);
            Thread consumingSodas4 = new Thread(consumer.ConsumeSodas);

            producingBottles.Start();
            splittingBottles.Start();

            consumingBeers.Start();
            consumingSodas.Start();
            consumingSodas1.Start();
            consumingSodas2.Start();
            consumingSodas3.Start();
            consumingSodas4.Start();

            producingBottles.Join();
            splittingBottles.Join();

            consumingBeers.Join();
            consumingSodas.Join();

        }
    }
}
