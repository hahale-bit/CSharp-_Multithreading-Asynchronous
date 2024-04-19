using System.Diagnostics;
using System.Net;

namespace CSharp__Multithreading_Asynchronous
{
    internal class DoSomething
    {
        //创建一个计时器
        Stopwatch sw = new Stopwatch();

        //调用异步方法的方法叫调用方法 calling method
        public void DoRun()
        {
            const int LargeNumber = 6_000_000;
            sw.Start();
            Task<int> t1 = CountCharacters(1, "http://www.microsoft.com");
            Task<int> t2 = CountCharacters(2, "http://illustratedcsharp.com");
            CountNumber(1, LargeNumber);
            CountNumber(2, LargeNumber);
            CountNumber(3, LargeNumber);
            CountNumber(4, LargeNumber);
            Console.WriteLine($"Characters of     http://www.microsoft.com:{t1.Result}");
            Console.WriteLine($"Characters of http://illustratedcsharp.com:{t2.Result}");
        }
        //这是一个异步方法，async method
        private async Task<int> CountCharacters(int id,string uristring)
        {
            WebClient wc1 = new WebClient();
            Console.WriteLine("Starting call {0}:{1}ms", id, sw.Elapsed.TotalMilliseconds);
            //下面这个就是一个异步的方法
            string result = await wc1.DownloadStringTaskAsync(new Uri(uristring));
            Console.WriteLine("Call {0} completed:{1}ms", id, sw.Elapsed.TotalMilliseconds);
            return result.Length;
        }

        private void CountNumber(int id,int number)
        {
            for (int i = 0; i < number; i++) { };
            Console.WriteLine("Counting {0} : {1}", id, sw.Elapsed.TotalMilliseconds);
        }

    }

    internal class Program
    {

        //9.FromBook（PART1）
        public static void Main(string[] args)
        {
            DoSomething doSomething = new DoSomething();
            doSomething.DoRun();
        }

      
        //8.Is Asynchronous == Multithreading?
        /*public static void Main(string[] args)
        {
           *//* TaskTest();
            ThreadTest();*//*
           DoSomething();
        }

        static void DoSomething()
        {
            Console.WriteLine("Task开始");
            Task.Delay(30000).Wait();
            Console.WriteLine("Task结束");
        }

        public static void TaskTest()
        {
            var sw = new Stopwatch();
            sw.Start();

            for (int i = 0; i < 1000; i++) {
                Task.Factory.StartNew(() => { });
            }
            sw.Stop();
            Console.WriteLine($"Task {sw.ElapsedMilliseconds}");
        }

        public static void ThreadTest()
        {
            var sw = new Stopwatch();
            sw.Start();
            for(int i = 0;i < 1000;i++)
            {
                new Thread(() => { }).Start();
            }
            sw.Stop();
            Console.WriteLine($"Thread {sw.ElapsedMilliseconds}");
        }*/
        //7.C#的异步怎么写
        /*public async Task<int> Method1()
        {
            Task<int> ta = LongTimeAsync();
            int resuly = await ta;
            return resuly;
        }

        public async Task<int> LongTimeAsync()
        {
            await Task.Delay(100000);
            return 2;//为什么这里明明返回的是一个整数2，方法返回值类型定义的是Task<int>，但没有报错呢？ 个人理解为，这里的Task<int>中的Task更多是为了强调当前方法是一个异步方法？
        }*/

        //6.异步Task

        /*static void Main(string[] args)
        {
            Calculate();
            Console.Read();
        }

         static async void Calculate()
        {
            var t1 = Task.Run(() =>
            {
                return Calculate1();
            });
            var awaiter1 = t1.GetAwaiter();
            awaiter1.OnCompleted(() =>
            {
                var result1 = awaiter1.GetResult();
                var t2 = Task.Run(() =>
                {
                    return Calculate2(result1);
                });
                var awaiter2 = t2.GetAwaiter();
                awaiter2.OnCompleted(() =>
                {
                    var result2 = awaiter2.GetResult();
                    Calculate3(result1, result2);
                });
            });
           
           
            //如何取一个Task的返回值呢？
            
            //一个Task.Run的表达式里面如果有多个方法要执行咋办？那就执行，但是返回值只能有一个
            

        }
        static int Calculate1() {
            int result = 8;
            Thread.Sleep(3000);
            Console.WriteLine(result);
            return result;
        }

        static int Calculate2(int a) { 
            int result = a + 4;
            Thread.Sleep(2000);
            Console.WriteLine(result);
            return result;
        }

        static int Calculate3(int a,int b) {
            int result = a + b;
            Thread.Sleep(1000);
            Console.WriteLine(result);
            return result;
        }*/

        //5.线程锁lock

        /*static object threadLock = new object();//创建一个锁
        static void Main(string[] args)
        {
            for (int i = 0; i < 10; i++)
            {
                Thread thread = new Thread(AddText);
                thread.Start();
            }
        }

        private static void AddText(object? obj)
        {
            lock(threadLock)
            {
                File.AppendAllText(@"D:\test.txt", $"开始 {Thread.CurrentThread.ManagedThreadId}");
                Thread.Sleep(100);
                File.AppendAllText(@"D:\test.txt", $"结束{Thread.CurrentThread.ManagedThreadId}");
            }
            
        }*/

        //4.线程之间的关系,其实也是线程控制的一种

        /*static void Main(string[] args)
        {
            Thread thread = new Thread(PrintHello);
            thread.Start();

            //如果当前的主线程某些部分依赖于子线程执行的结果，就需要等待
            //thread.Join();
            while(thread.IsAlive)
            {
                Console.WriteLine("子线程还在运行中！");
                Thread.Sleep(100);
            }

            Console.WriteLine("程序现在退出了！");
        }

        private static void PrintHello(object? obj)
        {
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine("Hello World!");
                Thread.Sleep(100);
            }
        }*/

        //3.线程的控制

        /*static void Main(string[] args)
        {
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
            //为什么下面这种方式是错的，我理解应该是给委托指向的方法传递参数？
            //Thread thread = new Thread(PrintHello(cancellationTokenSource.Token));
            Thread thread = new Thread(() => { PrintHello(cancellationTokenSource.Token); });
            thread.Start();

            //比如我现在有一个下载文件的线程，我只想给5s钟
            Thread.Sleep(5000);
            cancellationTokenSource.CancelAfter(1000);

            Console.WriteLine("程序从这里退出！");
        }

        private static void PrintHello(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                Thread.Sleep(1000);
                Console.WriteLine("Hello World!");
            }
        }*/
    }
}
