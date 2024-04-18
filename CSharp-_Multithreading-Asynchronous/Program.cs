
namespace CSharp__Multithreading_Asynchronous
{
    internal class Program
    {



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
