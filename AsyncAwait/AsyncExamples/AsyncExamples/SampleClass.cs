using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncExamples
{
    class SampleClass
    {
        public async Task<IEnumerable<double>> ManyTasks()
        {
            var numbers = new[] {1d, 2d, 3d};

            IEnumerable<Task<double>> tasks = numbers
            .Where(x => x < 9d)
            .Select(async x => await LongComputationAsync(x) + await LongComputation2Async(x));


            IEnumerable<double> transformed = await Task.WhenAll(tasks);

            return transformed;
        }

//        Task t = Task.Factory.StartNew(() => MyLongComputation(a, b),
//cancellationToken,
//TaskCreationOptions.LongRunning,
//taskScheduler);

        private async Task<double> LongComputationAsync(double x)
        {
            return await Task.Run(() => LongComputation(x));
        }

        private double LongComputation(double x)
        {
            return Math.Sin(x);
        }

        private async Task<double> LongComputation2Async(double x)
        {
            return await Task.Run(() => LongComputation2(x));
        }

        private double LongComputation2(double x)
        {
            return Math.Cos(x);
        }

        
    }
}
