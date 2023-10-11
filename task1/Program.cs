using System;
using System.IO;

namespace task1
{
    class Program
    {
        static string[] ReadDataFromTxt(string fileName)
        {
            try
            {
                string[] arr = new string[2];
                using (StreamReader s = new StreamReader(new FileStream(fileName, FileMode.Open, FileAccess.Read)))
                {
                    arr[0] = s.ReadLine();
                    arr[1] = s.ReadLine();
                }
                return arr;
            }
            catch (FileNotFoundException)
            {
                throw new Exception($"File {fileName} is not found");
            }
        }

        static double Average()
        {
            using(StreamWriter noFileWriter = new StreamWriter("no_file.txt"), 
                overflowWriter = new StreamWriter("overflow.txt"),
                badDataWriter = new StreamWriter("bad_data.txt"))
            {
                int sum = 0;
                int amount = 0;

                for (int i = 10; i < 30; i++)
                {
                    string fileName = $"{i}.txt";
                    try
                    {
                        string[] arr = ReadDataFromTxt(fileName);
                        try
                        {
                            int num1 = int.Parse(arr[0]);
                            int num2 = int.Parse(arr[1]);

                            try
                            {
                                checked
                                {
                                    sum += num1 * num2;
                                    amount++;
                                }
                            }
                            catch (OverflowException)
                            {
                                overflowWriter.WriteLine(fileName);
                                Console.WriteLine($"Result from {fileName} exceeds maximum 'int' size");
                            }
                        }
                        catch (Exception)
                        {
                            badDataWriter.WriteLine(fileName);
                            Console.WriteLine($"Data from {fileName} is not in a correct format");
                        }
                    }
                    catch (Exception ex)
                    {
                        noFileWriter.WriteLine(fileName);
                        Console.WriteLine(ex.Message);
                    }
                }

                try
                {
                    return sum / amount;
                }
                catch (Exception)
                {
                    throw new Exception("Average can not be calculated. All files names " +
                        "are placed in 'no_file.txt' or 'bad_data.txt' or 'overflow.txt'");
                }
            }
        }

        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine($"Average is equal to {Average()}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
