using System.Security.Cryptography.X509Certificates;
using System.Text.Json;

namespace modul8_103022300071
{
    public class transfer
    {
        public int threshold { get; set; } = 25000000;
        public int low_fee { get; set; } = 6500;
        public int high_fee { get; set; } = 15000;
        public transfer(int threshold, int low_fee, int high_fee)
        {
            this.threshold = threshold;
            this.low_fee = low_fee;
            this.high_fee = high_fee;
        }
    }
    public class confirmation
    {
        public string en { get; set; } = "yes";
        public string id { get; set; } = "ya";

        public confirmation(string en, string id)
        {
            this.en = en;
            this.id = id;
        }
    }

    internal class BankTransferConfig

    {

        public string lang { get; set; }
        public transfer Transfer { get; set; }
        public List<string> methods { get; set; }
        public confirmation Confirmation { get; set; }



    }

    class UIConfig
    {
        public BankTransferConfig bankTransferConfig;
        public const String filePath = "bank_transfer_config.json";
        public UIConfig()
        {
            try
            {
                bankTransferConfig = ReadConfigFile();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error reading config file: " + ex.Message);
                setDefaultConfig();
                WriteConfigFile();
            }
        }
        private BankTransferConfig ReadConfigFile()
        {
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                return JsonSerializer.Deserialize<BankTransferConfig>(json);
            }
            else
            {
                return new BankTransferConfig();
            }
        }
        private void setDefaultConfig()
        {
            bankTransferConfig = new BankTransferConfig();
            bankTransferConfig.lang = "en";
            bankTransferConfig.Transfer.threshold = 25000000;
            bankTransferConfig.Transfer.low_fee = 6500;
            bankTransferConfig.Transfer.high_fee = 15000;
            bankTransferConfig.methods = new List<string> { "RTO (real -time)", "SKN", "RTGS", "BI Fast" };
            bankTransferConfig.Confirmation.en = "yes";
            bankTransferConfig.Confirmation.id = "ya";
        }
        private void WriteConfigFile()
        {
            JsonSerializerOptions options = new JsonSerializerOptions
            {
                WriteIndented = true
            };
            string json = JsonSerializer.Serialize(bankTransferConfig, options);
            File.WriteAllText(filePath, json);
        }
        public void start()
        {
            Console.WriteLine("Welcome to Bank Transfer Application");
            Console.WriteLine("1. English");
            Console.WriteLine("2. Indonesia");
            Console.WriteLine("Please select your language: ");
            string input = Console.ReadLine();
            if (input == "1")
            {
                bankTransferConfig.lang = "en";
            }
            else if (input == "2")
            {
                bankTransferConfig.lang = "id";
            }
            else
            {
                Console.WriteLine("Invalid input. Defaulting to English.");
                bankTransferConfig.lang = "en";
            }
            if (bankTransferConfig.lang == "en")
            {
                Console.WriteLine("Please insert the amount of money to transfer:");
            }
            else
            {
                Console.WriteLine("Masukkan jumlah uang yang akan di-transfer:");
            }
        }
            public void biaya_transfer()
            {
                int amount = Convert.ToInt32(Console.ReadLine());
                if (bankTransferConfig.lang == "id")
                {
                    if (amount > bankTransferConfig.Transfer.threshold)
                    {
                        Console.WriteLine("Biaya transfer adalah: " + bankTransferConfig.Transfer.high_fee + "Total Biaya = " + (amount + bankTransferConfig.Transfer.high_fee));
                    }
                    else
                    {
                        Console.WriteLine("Biaya transfer adalah: " + bankTransferConfig.Transfer.low_fee + "Total Biaya = " + (amount + bankTransferConfig.Transfer.low_fee));
                    }
                }
                else
                {
                    if (amount > bankTransferConfig.Transfer.threshold)
                    {
                        Console.WriteLine("Transfer fee is: " + bankTransferConfig.Transfer.high_fee + "Total Fee = " + (amount + bankTransferConfig.Transfer.high_fee));
                    }
                    else
                    {
                        Console.WriteLine("Transfer fee is: " + bankTransferConfig.Transfer.low_fee + "Total Fee = " + (amount + bankTransferConfig.Transfer.low_fee));
                    }

                }
            }
            public void pilih_metode()
            {
                if (bankTransferConfig.lang == "id")
                {
                Console.WriteLine("Pilih metode transfer:");
                }
                else
                {
                Console.WriteLine("Select transfer method:");
                }
                for (int i = 0; i < bankTransferConfig.methods.Count; i++)
                {
                    Console.WriteLine((i + 1) + ". " + bankTransferConfig.methods[i]);
                }
                int method = Convert.ToInt32(Console.ReadLine());
                if (bankTransferConfig.lang == "en")
                {
                    Console.WriteLine("Please type" + bankTransferConfig.Confirmation.en + "to confirm the transaction");
                }
                else
                {
                    Console.WriteLine("Silahkan ketik" + bankTransferConfig.Confirmation.id + "untuk mengkonfirmasi transaksi");
                }
                string confirm = Console.ReadLine();
                if (bankTransferConfig.lang == "en")
                {
                    if (confirm == bankTransferConfig.Confirmation.en)
                    {
                        Console.WriteLine("Transaction confirmed");
                    }
                    else
                    {
                        Console.WriteLine("Transaction cancelled");
                    }
                }
                else
                {
                    if (confirm == bankTransferConfig.Confirmation.id)
                    {
                        Console.WriteLine("Transaksi dikonfirmasi");
                    }
                    else
                    {
                        Console.WriteLine("Transaksi dibatalkan");
                    }
                }
            }
        
    }
}
