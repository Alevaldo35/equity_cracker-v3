using System;
using System.IO;
using System.Configuration;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Threading;
using System.Diagnostics;
using Nethereum.Web3;

namespace equity_cracker
{

    internal static class Program
    {
        #region Vars
        private static object  consoleLock  = new object();
        public  static Boolean runCode      = false;
        public  static int     hits         = 0;
        public  static int     checks       = 0;
        public  static int     proxyChangerValue = 0;
        public  static Boolean DebugOption  = Convert.ToBoolean(ConfigurationManager.AppSettings["debug"]);
        public  static Boolean enableCustomRPC = Convert.ToBoolean(ConfigurationManager.AppSettings["enableCustomRPC"]);
        public  static Boolean censoreRPC   = Convert.ToBoolean(ConfigurationManager.AppSettings["censoreRPC"]);
        public  static int     Threads      = Convert.ToInt16(ConfigurationManager.AppSettings["threads"]);
        public  static string  cryptoToMine = Convert.ToString(ConfigurationManager.AppSettings["cryptoToMine"]);
        public  static string  customRPC    = Convert.ToString(ConfigurationManager.AppSettings["customRPC"]);
        public  static int     consoleRefreshRate = Convert.ToInt16(ConfigurationManager.AppSettings["consoleRefreshRate"]);
        #endregion

        public static async Task NewMenu()
        {
            Console.Title = "Loading..";
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            string text = @"
███████╗ ██████╗ ██╗   ██╗██╗████████╗██╗   ██╗    ██╗   ██╗██████╗ 
██╔════╝██╔═══██╗██║   ██║██║╚══██╔══╝╚██╗ ██╔╝    ██║   ██║╚════██╗
█████╗  ██║   ██║██║   ██║██║   ██║    ╚████╔╝     ██║   ██║ █████╔╝
██╔══╝  ██║▄▄ ██║██║   ██║██║   ██║     ╚██╔╝      ╚██╗ ██╔╝ ╚═══██╗
███████╗╚██████╔╝╚██████╔╝██║   ██║      ██║        ╚████╔╝ ██████╔╝
╚══════╝ ╚══▀▀═╝  ╚═════╝ ╚═╝   ╚═╝      ╚═╝         ╚═══╝  ╚═════╝ 
";
            foreach (char c in text)
            {
                Console.Write(c);
                Thread.Sleep(10);
            }
            string undertitle = "Version: v3.0.1 - build 1901";
            Console.CursorVisible = false;
            int width = 30;
            int left = Console.CursorLeft;
            int top = Console.CursorTop;
            int i = 0;

            /*
            for (i = 0; i <= width; i++)
            {
                Console.SetCursorPosition(left, top);
                Console.Write("[");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.SetCursorPosition(left + i, top);
                Console.Write("#");
                Console.ForegroundColor = ConsoleColor.White;
                Console.SetCursorPosition(left + width, top);
                Console.Write("]");
                Console.SetCursorPosition(left + width + 1, top);
                Console.Write(" {0}%", (i * 100) / width);
                Thread.Sleep(100);
            }
            */

            for(int x = 0; x <= 1; x++)
            {
                i++;
                Console.SetCursorPosition(left, top);
                Console.Write("[");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.SetCursorPosition(left + i, top);
                Console.Write("#");
                Console.ForegroundColor = ConsoleColor.White;
                Console.SetCursorPosition(left + width, top);
                Console.Write("]");
                Console.SetCursorPosition(left + width + 1, top);
                Console.Write(" {0}%", (i * 100) / width);
                Thread.Sleep(100);
            }

            Thread t = new Thread(BackgroundThread);
            t.Start();

            for (i = 0; i <= width; i++)
            {
                Console.SetCursorPosition(left, top);
                Console.Write("[");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.SetCursorPosition(left + i, top);
                Console.Write("#");
                Console.ForegroundColor = ConsoleColor.White;
                Console.SetCursorPosition(left + width, top);
                Console.Write("]");
                Console.SetCursorPosition(left + width + 1, top);
                Console.Write(" {0}%", (i * 100) / width);
                Thread.Sleep(60);
            }
            Console.CursorVisible = true;
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(new string(' ', Console.BufferWidth));
            Console.ForegroundColor = ConsoleColor.DarkGray;
            foreach (char c in undertitle)
            {
                Console.Write(c);
                Thread.Sleep(30);
            }
            Console.ForegroundColor = ConsoleColor.White;
            string choice1 = "ENTER";
            string choice1text = " Start Miner";
            string choice2 = "C";
            string choice2text = " Open config in notepad";
            string choice3 = "ESC";
            string choice3text = "  Exit";
            Console.WriteLine();
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("[");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            foreach (char c in choice1)
            {
                Console.Write(c);
                Thread.Sleep(30);
            }
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("]");
            Console.ForegroundColor = ConsoleColor.Green;
            foreach (char c in choice1text)
            {
                Console.Write(c);
                Thread.Sleep(30);
            }
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("[");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            foreach (char c in choice2)
            {
                Console.Write(c);
                Thread.Sleep(30);
            }
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("]    ");
            Console.ForegroundColor = ConsoleColor.Green;
            foreach (char c in choice2text)
            {
                Console.Write(c);
                Thread.Sleep(30);
            }
            Console.WriteLine();
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("[");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            foreach (char c in choice3)
            {
                Console.Write(c);
                Thread.Sleep(30);
            }
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("] ");
            Console.ForegroundColor = ConsoleColor.Red;
            foreach (char c in choice3text)
            {
                Console.Write(c);
                Thread.Sleep(30);
            }
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;
            while (true)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Enter)
                {
                    for (int x = 0; x < Threads; x++)
                    {
                        runCode = true;
                        Thread minerThreads = new Thread(testMiner);
                        minerThreads.Start();
                    }
                    var idkVar69 = false;
                    while(true)
                    {
                        key = Console.ReadKey(true);
                        if (key.Key == ConsoleKey.X)
                        {
                            idkVar69 = true;
                            runCode = false;
                            Thread.Sleep(1000);
                            Console.WriteLine("Miner Stopped. Press Enter to start again");
                        } else 
                        if (key.Key == ConsoleKey.Enter)
                        {
                            if (idkVar69 == true)
                            {
                                runCode = false;
                            }
                        }
                    }
                } else
                if (key.Key == ConsoleKey.Escape)
                {
                    Console.ForegroundColor= ConsoleColor.Red;
                    Console.WriteLine("Exiting..");
                    Thread.Sleep(2000);
                    Environment.Exit(0);
                } else
                if (key.Key == ConsoleKey.C)
                {
                    var exePath = System.Reflection.Assembly.GetEntryAssembly().Location;

                    string path = Path.GetFullPath(Path.Combine(exePath, @"..\..\config\settings.config"));
                    Process.Start("notepad.exe", path);
                }
            }
        }

        static void BackgroundThread2()
        {
            while(true)
            {

            }
        }

        static string address;
        static System.TimeSpan duration;
        static Nethereum.Hex.HexTypes.HexBigInteger balance;

        static string final;
        static string cpuUsage;
        static void BackgroundThread()
        {
            while(true)
            {
                if (runCode)
                {
                    Console.Title = "EquityCracker | Mining.. | Hits: " + hits;
                    Console.WriteLine("ok");
                    PerformanceCounter cpuCounter;
                    PerformanceCounter ramCounter;

                    cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
                    ramCounter = new PerformanceCounter("Memory", "Available MBytes");
                    while (true)
                    {
                        var firstCheck = checks;
                        Thread.Sleep(1000);
                        var secondCheck = checks;
                        var local = secondCheck - firstCheck;
                        var local2 = local;
                        final = local2.ToString();
                        cpuUsage = cpuCounter.NextValue() + "%";

                        Console.SetCursorPosition(0, 10);
                        Console.Write(new string(' ', Console.BufferWidth));
                        Console.SetCursorPosition(0, 10);
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.Write("         Wallet: ");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine(address);

                        Console.SetCursorPosition(0, 11);
                        Console.Write(new string(' ', Console.BufferWidth));
                        Console.SetCursorPosition(0, 11);
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.Write("Generation Time: ");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine(duration);

                        Console.SetCursorPosition(0, 12);
                        Console.Write(new string(' ', Console.BufferWidth));
                        Console.SetCursorPosition(0, 12);
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.Write("        Balance: ");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine(balance);

                        Console.SetCursorPosition(0, 13);
                        Console.Write(new string(' ', Console.BufferWidth));

                        Console.SetCursorPosition(0, 14);
                        Console.Write(new string(' ', Console.BufferWidth));
                        Console.SetCursorPosition(0, 14);
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.Write("         Checks: ");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine(checks);

                        Console.SetCursorPosition(0, 15);
                        Console.Write(new string(' ', Console.BufferWidth));
                        Console.SetCursorPosition(0, 15);
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.Write("            CPS: ");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine(final);

                        Console.SetCursorPosition(0, 16);
                        Console.Write(new string(' ', Console.BufferWidth));
                        Console.SetCursorPosition(0, 16);
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.Write("      CPU Usage: ");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine(cpuUsage);

                        Console.SetCursorPosition(0, 17);
                        Console.Write(new string(' ', Console.BufferWidth));
                        Console.SetCursorPosition(0, 17);
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.Write("       Endpoint: ");
                        Console.ForegroundColor = ConsoleColor.White;
                        if (censoreRPC == true)
                        {
                            Console.WriteLine("[censored]");
                        } else
                        {
                            Console.WriteLine(rpc);
                        }
                    }
                }
            }
        }

        static string rpc = "https://rpc.ankr.com/eth";
        static async void testMiner()
        {
            Console.CursorVisible = false;
            try
            {
                if (enableCustomRPC == true)
                {
                    rpc = customRPC;
                }
                while (true)
                {
                    while (runCode)
                    {
                        var web3 = new Nethereum.Web3.Web3(rpc);
                        var startTime = DateTime.Now;
                        var rng = new RNGCryptoServiceProvider();
                        var privateKeyBytes = new byte[32];
                        rng.GetBytes(privateKeyBytes);
                        var privateKey = BitConverter.ToString(privateKeyBytes).Replace("-", "").ToLower();
                        var endTime = DateTime.Now;
                        duration = endTime - startTime;
                        var account = new Nethereum.Web3.Accounts.Account(privateKey);
                        address = account.Address;
                        decimal etherAmount;
                        try
                        {
                            balance = await web3.Eth.GetBalance.SendRequestAsync(address);
                            etherAmount = Web3.Convert.FromWei(balance.Value);
                        }
                        catch (Exception)
                        {
                            if (enableCustomRPC == true)
                            {
                                continue;
                            } else
                            if (rpc == "https://rpc.ankr.com/eth")
                            {
                                rpc = "https://eth.llamarpc.com";
                            }
                            else if (rpc == "https://eth.llamarpc.com")
                            {
                                rpc = "https://cloudflare-eth.com/";
                            }
                            else if (rpc == "https://cloudflare-eth.com/")
                            {
                                rpc = "https://eth-mainnet.gateway.pokt.network/v1/5f3453978e354ab992c4da79";
                            }
                            else if (rpc == "https://eth-mainnet.gateway.pokt.network/v1/5f3453978e354ab992c4da79")
                            {
                                rpc = "https://rpc.ankr.com/eth";
                            }
                            continue;
                        }

                        checks++;

                        if (etherAmount != 0)
                        {
                            hits++;

                            var exePath = System.Reflection.Assembly.GetEntryAssembly().Location;

                            string path = Path.GetFullPath(Path.Combine(exePath, @"..\..\hits.txt"));

                            try
                            {
                                if (!(File.Exists(path)))
                                {
                                    using (FileStream fs = File.Create(path)) { };
                                }
                            } catch (Exception)
                            {
                                runCode = false;
                                Thread.Sleep(1000);
                                Console.WriteLine("Something went wrong with saving hits! Please save the private key down below!");
                                Console.WriteLine("If you want to support us, you can donate to this ethereum address: 0xe0f37a884658556d7577a5d34184f8054a4f752e");
                                Console.WriteLine();
                                Console.WriteLine("Private Key: " + privateKey);
                                Console.ReadLine();
                            }

                            Console.Title = "EquityCracker | Mining.. | Hits: " + hits;
                            try
                            {
                                Console.ForegroundColor= ConsoleColor.Green;
                                Console.WriteLine("YOU GOT A HIT! | PrivateKey" + privateKey);
                                Console.ForegroundColor = ConsoleColor.White;
                                File.AppendAllText(path,
                                        "Private Key: " + privateKey + Environment.NewLine);
                            } catch (Exception)
                            {
                                runCode = false;
                                Thread.Sleep(2000);
                                Console.WriteLine("Something went wrong with saving hits! Please save the private key down below!");
                                Console.WriteLine("If you want to support us, you can donate to this ethereum address: 0xe0f37a884658556d7577a5d34184f8054a4f752e");
                                Console.WriteLine();
                                Console.WriteLine("Private Key: " + privateKey);
                                Console.ReadLine();
                            }
                        }
                    }
                }
            } catch(Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

        }

        public static async Task Main()
        {
            await NewMenu();
            Console.ReadLine();
            Environment.Exit(0);
            
            #region old System
            /*
            if (cryptoToMine != "eth")
            {
                Console.WriteLine("Please use eth as CryptoToMine!");
                Console.ReadLine();
                Environment.Exit(0);
            }

            Console.Title = "Equity cracker v3 - Hits: " + hits;

            try
            {
                proxyChangerValue = Int32.Parse(proxyChangerValueString);
            }
            catch (FormatException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("proxyChangerValue needs to be a number! Check .config");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Press enter to exit..");
                Console.ReadLine();
                Environment.Exit(0);
            }

            #region Show Settings
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Current settings:");
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write("     UseProxy: ");
            if ( UseProxy == true ) { Console.ForegroundColor = ConsoleColor.Green; } else { Console.ForegroundColor = ConsoleColor.Red; }
            Console.WriteLine(UseProxy);
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write("Proxy Changer: ");
            if (proxyChanger == true) { Console.ForegroundColor = ConsoleColor.Green; } else { Console.ForegroundColor = ConsoleColor.Red; }
            Console.WriteLine(proxyChangerValue);
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write(" RPC endpoint: ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("rpc.ankr.com*");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write("       Crypto: ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Ethereum(eth)*");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write("      Threads: ");
            if (Threads > 10) { Console.ForegroundColor = ConsoleColor.Red; } else { Console.ForegroundColor = ConsoleColor.Green; }
            Console.WriteLine(Threads);
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write("        Debug: ");
            if (DebugOption == true) { Console.ForegroundColor = ConsoleColor.Green; } else { Console.ForegroundColor = ConsoleColor.Red; }
            Console.WriteLine(DebugOption);
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("* = can't be changed");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();
            Console.WriteLine("Press enter to start..");
            Console.ReadLine();
            #endregion

            lock (consoleLock) { Console.Clear(); }

            var exePath = System.Reflection.Assembly.GetEntryAssembly().Location;
            
            string path = Path.GetFullPath(Path.Combine(exePath, @"..\..\hits.txt"));
            string proxyFilePath = Path.GetFullPath(Path.Combine(exePath, @"..\..\proxys.txt"));

            if (UseProxy == true)
            {
                if (!(File.Exists(proxyFilePath)))
                {
                    using (FileStream fs = File.Create(proxyFilePath)) { };
                    var lineCount = File.ReadLines(proxyFilePath).Count();
                    if (lineCount > 0)
                    {
                        Console.Write("Creating proxys.txt.. | ");
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("You need to input proxy's!");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine("Press enter to exit..");
                        Console.ReadLine();
                        Environment.Exit(0);
                    }
                }
            }
            Console.ForegroundColor = ConsoleColor.DarkGray;
            if (!(File.Exists(path)))
            {
                Console.Write("Creating hits.txt.. | ");
                using (FileStream fs = File.Create(path)) { };
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("done.");
            }
            Console.WriteLine();

            Console.WriteLine("Starting miner.. Good Luck!");

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            Task[] tasks = new Task[Threads];

            int currentProxyIndex = 0;

            for (int i = 0; i < Threads; i++)
            {
                tasks[i] = Task.Run( async () =>
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    if (!(File.Exists(proxyFilePath))) { lock (consoleLock) { Console.WriteLine(proxyFilePath + " can't be found!"); Console.ReadLine(); Environment.Exit(0); } }
                    Console.ForegroundColor = ConsoleColor.White;

                    string[] proxys = File.ReadAllLines(proxyFilePath);

                    MinerStarted = true;

                    while (true)
                    {
                        try
                        {
                            while(runCode)
                            {
                                if (startedAtIDK == false)
                                {
                                    startedAtIDK = true;
                                    startTimeX = DateTime.Now;
                                }
                                count++;
                                var currentProxy = proxys[currentProxyIndex];
                                currentProxyIndex++;
                                string url = "https://rpc.ankr.com/eth";
                                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                                var startTime = DateTime.Now;
                                var rng = new RNGCryptoServiceProvider();
                                var privateKeyBytes = new byte[32];
                                rng.GetBytes(privateKeyBytes);
                                var privateKey = BitConverter.ToString(privateKeyBytes).Replace("-", "").ToLower();
                                var endTime = DateTime.Now;
                                var duration = endTime - startTime;

                                var account = new Nethereum.Web3.Accounts.Account(privateKey);
                                string address = account.Address;

                                HttpClientHandler handler = new HttpClientHandler();

                                HttpClient client = new HttpClient(handler);
                                client.Timeout = new TimeSpan(0, 0, 30);

                                string json = "{\"jsonrpc\":\"2.0\",\"method\":\"eth_getBalance\",\"params\":[" +
                                              $"\"{address}\",\"latest\"" +
                                              "],\"id\":1}";

                                var content = new StringContent(json, Encoding.UTF8, "application/json");

                                client.DefaultRequestHeaders.Connection.Clear();

                                HttpResponseMessage response = await client.PostAsync(url, content);


                                if (response.IsSuccessStatusCode)
                                {
                                    string responseString = response.Content.ReadAsStringAsync().Result;

                                    JObject responseJson = JObject.Parse(responseString);

                                    string balance = (string)responseJson["result"];

                                    var consoleColor = ConsoleColor.White;

                                    if (balance != "0x0")
                                    {
                                        consoleColor = ConsoleColor.Green;
                                        hits++;
                                        Console.Title = "Equity cracker v3 - Hits: " + hits;
                                        string textToSave = privateKey + " | Bal: " + balance;

                                        File.WriteAllText(path, textToSave);
                                    }
                                    else
                                    {
                                        consoleColor = ConsoleColor.Red;
                                    }

                                    var idk = count + "Private Key: " + privateKey;
                                    if (UseProxy == true)
                                    {
                                        idk = count + "Private Key: " + privateKey + " | Bal: " + balance;
                                    }

                                    Write(idk, consoleColor, duration.ToString(), "127.0.0.1");

                                    checks++;

                                    if (proxyChanger == true)
                                    {
                                        if (checks > proxyChangerValue)
                                        {
                                            Console.WriteLine("Changing proxy.."); checks = 0; MyException m;
                                            m = new MyException("Maximal checks reached");
                                            m.ExtraErrorInfo = "Maximal checks reached: (0)";
                                            throw m;
                                        }
                                    }

                                    if (currentProxyIndex >= proxys.Length) { currentProxyIndex = 0; }
                                }
                                else
                                {
                                    if (DebugOption == true) { Write("Failed getting balance | Status code: " + response.StatusCode, ConsoleColor.Red, "no", "no"); }
                                }
                            }
                        } 
                        catch (Exception e)
                        {
                            if (DebugOption == true) { Console.WriteLine("Exception - miner"); Console.WriteLine(e); }

                            if (currentProxyIndex >= proxys.Length) { currentProxyIndex = 0; }

                            continue;
                        }
                    }
                });
            }
            */
            #endregion
        }

        public static void Write(string text, ConsoleColor color, string duration, string proxy)
        {
            lock (consoleLock)
            {
                Console.ForegroundColor = color;
                if (duration != "no")
                {
                    Console.Write(text);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write(" | Generation time: " + duration);
                    Console.WriteLine(" | Proxy: " + proxy);
                } else
                {
                    Console.WriteLine(text);
                }

                Console.ForegroundColor = ConsoleColor.White;
            }
        }
    }

    public class MyException : Exception
    {
        public MyException() : base() { }
        public MyException(string message) : base(message) { }
        public MyException(string message, Exception e) : base(message, e) { }

        private string strExtraInfo;
        public string ExtraErrorInfo
        {
            get
            {
                return strExtraInfo;
            }

            set
            {
                strExtraInfo = value;
            }
        }
    }
}
