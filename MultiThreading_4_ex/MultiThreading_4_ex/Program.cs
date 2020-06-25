using System;
using System.Threading;

class Program{
    static public void merge(int[] arr, int p, int q, int r)
    {
        int i, j, k;
        int n1 = q - p + 1;
        int n2 = r - q;
        int[] L = new int[n1];
        int[] R = new int[n2];
        for (i = 0; i < n1; i++)
        {
            L[i] = arr[p + i];
        }
        for (j = 0; j < n2; j++)
        {
            R[j] = arr[q + 1 + j];
        }
        i = 0;
        j = 0;
        k = p;
        while (i < n1 && j < n2)
        {
            if (L[i] <= R[j])
            {
                arr[k] = L[i];
                i++;
            }
            else
            {
                arr[k] = R[j];
                j++;
            }
            k++;
        }
        while (i < n1)
        {
            arr[k] = L[i];
            i++;
            k++;
        }
        while (j < n2)
        {
            arr[k] = R[j];
            j++;
            k++;
        }
    }
    public static void mergeSort(int[] arr, int p, int r) // p - is start of mass, r - is end of mass;  
    {
        if (p < r)
        {
            int q = (p + r) / 2;
            mergeSort(arr, p, q);
            mergeSort(arr, q + 1, r);
            merge(arr, p, q, r);
        }
    }
    public static void mergeSort(object par) // use only in multithreading
    {
        Parameters p = (Parameters)par;
        mergeSort(p.arr, p.p, p.r);
    }

    static void Main(string[] args)
    {
        int n = 50_000_000;
        int[] mass = new int[n];

        // FILL ARRAY
        fillArray(mass);

        // CREATE PARAMETERS METHOD FOR THREAD
        Parameters p = new Parameters(mass, 0, n / 2 - 1);
        Parameters p2 = new Parameters(mass, n / 2, n - 1);

        // ------------ CREATE THREAD AND SPLIT ARRAY BETWEEN THEM ------------
        // 34,130512 sec. elapsed to sorting 100 000 000 array elements with the use 2 thread;  || Time was 52,6572089 sec. only one mergeSort
        
        DateTime dt1 = DateTime.Now;
        
        // SPLIT
        Thread t1 = new Thread(mergeSort);
        Thread t2 = new Thread(mergeSort);
       
        t1.Start(p);
        t2.Start(p2);

        Console.WriteLine("Sorting. Please wait...");
        
        t1.Join();
        t2.Join();
        
        // MERGE ALL PARTS (2 parts)
        merge(mass, 0, n / 2 - 1, n - 1);

        DateTime dt2 = DateTime.Now;

        Console.WriteLine("Time was " + (dt2 - dt1).TotalSeconds + " sec.\n\n");
        // ------------ END SORTING IN MULTI THREADING MOD ------------


        // ------------ SORTING IN SINGLE-THREADED MOD ------------ 
        fillArray(mass);

        Console.WriteLine("Sorting in single-thread mode. Please wait...");
        DateTime dt3 = DateTime.Now;

        mergeSort(mass, 0, n - 1);

        DateTime dt4 = DateTime.Now;

        Console.WriteLine("Time was " + (dt4 - dt3).TotalSeconds + " sec.\n\n");
        // ------------ END SORTING IN SINGLE-THREADED MOD ------------ 

        Console.WriteLine("Multithreading up perfomance to {0}% ({1} vs {2} difference in {3} sec.)", Math.Round( ( ( (dt2 - dt1).TotalSeconds) / ( (dt4 - dt3).TotalSeconds) ) * 100, 3), Math.Round((dt2 - dt1).TotalSeconds, 3), Math.Round((dt4 - dt3).TotalSeconds, 3), Math.Round( (dt4 - dt3).TotalSeconds - (dt2 - dt1).TotalSeconds, 3) );
    }

    private static void fillArray(int[] mass)
    {
        Random rand = new Random();
        Console.WriteLine("FIll array. Please wait.");
        for (int i = 0; i < mass.Length; i++) mass[i] = rand.Next(20000);
        
    }
}

class Parameters
{
    public int[] arr;
    public int p;
    public int r;

    public Parameters(int[] arr)
    {
        this.arr = arr;
    }

    public Parameters(int[] arr, int p, int r)
    {
        this.arr = arr;
        this.p = p;
        this.r = r;
    }
}