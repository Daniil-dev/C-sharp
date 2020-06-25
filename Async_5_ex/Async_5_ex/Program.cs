using System;
using System.Threading;
using System.Threading.Tasks;

public class MatrixMultiplierParallel{

    bool[] t_comp; // completed threads

    public async void MultiplyAsync(int N, float[,] A, float[,] B, float[,] C, int from, int to, int thread_number)
    {
        await Task.Run(() => Multiply(N, A, B, C, from, to, thread_number));
    }

    public void Multiply(int N, float[,] A, float[,] B, float[,] C, int from, int to, int thread_number)
    {
        DateTime dt1 = DateTime.Now; // Check time
        for (int i = from; i < to; i++)
        {
            for (int j = 0; j < N; j++)
            {
                for (int k = 0; k < N; k++) C[i, j] += A[i, k] * B[k, j];
            }
        }
        t_comp[thread_number] = true;
    }

    
    public static void Main()
    {
        int i, j;
        int N = 10000; // Matrix size
        int M = 4;    // Thread count


        // 10,5 - 10,86 - 10,07 with 4 thread  || 12,05 - 12,2 - 11,88 with 2 thread || 19,12 - 19 - 19,23 with 1 thread on N = 1000

        //  -  -  with 4 thread  ||  -  -  with 2 thread ||  -  -  with 1 thread on N = 1000
        //  -  -  with 4 thread  ||  -  -  with 2 thread ||  -  -  with 1 thread on N = 10000

        float[,] A = new float[N, N];
        float[,] B = new float[N, N];
        float[,] C = new float[N, N];


        //  Matrix content generation
        Random rnd = new Random();

        for (i = 0; i < N; ++i)
        {
            for (j = 0; j < N; ++j)
            {
                A[i, j] = (float)rnd.NextDouble();
                B[i, j] = (float)rnd.NextDouble();
                C[i, j] = (float)0.0;
            }
        }

        // Divide matrix between asynchronous processes
        int q = N / M, r = N % M;
        int from = 0, to;

        MatrixMultiplierParallel mmp = new MatrixMultiplierParallel
        {
            t_comp = new bool[M]
        };

        Console.WriteLine("Computing, please wait...");
       
        DateTime dt1 = DateTime.Now; // Check time

        for (i = 0; i < M; ++i)
        {
            to = from + q + (i < r ? 1 : 0);

            mmp.MultiplyAsync(N, A, B, C, from, to, i); // Start the async method

            from = to;
        }

        while (true)
        {
            bool ret = true;
            for (i = 0; i < M; i++)
                ret &= mmp.t_comp[i];

            if (ret)
                break;
            
            Thread.Sleep(5);
        }

        Console.Clear();
        Console.WriteLine("Succefully !");
        Console.WriteLine("Matrix size: {0}, Number of parts: {1}.", N, M);
        Console.WriteLine("Time was " + (DateTime.Now - dt1).TotalSeconds + " sec."); // Check the time
        Console.ReadLine();
    }

}

