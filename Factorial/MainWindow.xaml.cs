using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Factorial
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            LoadAssembly(6);
            // очистка
            GC.Collect();
            GC.WaitForPendingFinalizers();

            foreach (Assembly asm in AppDomain.CurrentDomain.GetAssemblies())
                Console.WriteLine(asm.GetName().Name);

            Console.Read();
        }
        private void FactorialGet(object sender, RoutedEventArgs e)
        {
            string[] tokens = textBoxFactorial.Text.Split(',');
            var list = new List<int>();
            for (int i = 0; i < 10; i++)
            {
                if (int.TryParse(tokens[i], out var num))
                {
                    list.Add(num);
                }
            }
            string factorials = "";
            for (int i = 0; i < list.Count; i++)
            {
                factorials += FactorialFunc(list[i]).ToString() + "\n";
            }
            MessageBox.Show(factorials);
        }
        private static void LoadAssembly(int number)
        {
            var context = new CustomAssemblyLoadContext();
            context.Unloading += Context_Unloading;
            var assemblyPath = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "FactorialApp.dll");
            Assembly assembly = context.LoadFromAssemblyPath(assemblyPath);
            var type = assembly.GetType("FactorialApp.Program");
            var greetMethod = type.GetMethod("FactorialFunc");
            var instance = Activator.CreateInstance(type);
            int result = (int)greetMethod.Invoke(instance, new object[] { number });
            Console.WriteLine($"Факториал числа {number} равен {result}");
            foreach (Assembly asm in AppDomain.CurrentDomain.GetAssemblies())
                Console.WriteLine(asm.GetName().Name);
            context.Unload();
        }
        private static void Context_Unloading(AssemblyLoadContext obj)
        {
            Console.WriteLine("Библиотека выгружена \n");
        }
    }
    public class CustomAssemblyLoadContext : AssemblyLoadContext
    {
        public CustomAssemblyLoadContext() : base(isCollectible: true) { }

        protected override Assembly Load(AssemblyName assemblyName)
        {
            return null;
        }
    }
}