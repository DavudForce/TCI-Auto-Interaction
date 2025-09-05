using System;
using System.IO;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace adsl_Auto_Interaction_App
{
    public class ValidateConnection
    {
        /***************************** CLIENT ***************************/
        static string configFile = @"C:\ProgramData\BESH\Online Services\ADSL Interaction\adsl_Auto-Interaction_App\server\server.beshonic.h";
        static string passwordFile = @"C:\ProgramData\BESH\Online Services\ADSL Interaction\adsl_Auto-Interaction_App\server\security\server.beshonic.k";

        private class ServerConfig
        {
            public string Adress { get; set; }
            public int Port { get; set; }
        }

        public static async Task<string> RecieveUriAsync()
        {
            // Load server IP/Port from JSON
            ServerConfig config = LoadServerConfig(configFile);

            // Load password hash (hex) from file
            byte[] passwordHash = LoadPasswordHash(passwordFile);

            try
            {
                using TcpClient client = new TcpClient();
                await client.ConnectAsync(config.Adress, config.Port);
                using NetworkStream stream = client.GetStream();

                Console.WriteLine("[CLIENT] Connected to server.");

                var sender = $"DU:{Environment.UserDomainName}//{Environment.UserName}";

                // Send initial request
                await WriteStringAsync(stream, $"{sender}&/tci_uri*");
                Console.WriteLine("[CLIENT] Initial request sent.");

                // Receive challenge
                string challenge = await ReadStringAsync(stream);

                Console.WriteLine($"[CLIENT] Received challenge: {challenge}");

                // Combine password hash with challenge
                byte[] finalHash = ComputeSHA256(CombineByteArrays(passwordHash, Encoding.UTF8.GetBytes(challenge)));

                // Send final hash
                await WriteStringAsync(stream, BitConverter.ToString(finalHash).Replace("-", ""));
                Console.WriteLine("[CLIENT] Sent authentication hash.");

                // Read result
                string result = await ReadStringAsync(stream);
                if (result == "TIMEOUT")
                {
                    Console.WriteLine("[CLIENT] Server says: TIMEOUT");
                    return "!TIMEOUT";
                }
                return result;
            }
            
            catch (Exception ex)
            {
                return "!" + ex.Message;
            }
        }

        private static ServerConfig LoadServerConfig(string path)
        {
            if (!File.Exists(path))
                throw new FileNotFoundException($"Server config file not found: {path}");

            string json = File.ReadAllText(path);
            return JsonSerializer.Deserialize<ServerConfig>(json) ?? new ServerConfig();
        }

        private static byte[] LoadPasswordHash(string path)
        {
            if (!File.Exists(path))
                throw new FileNotFoundException($"Password file not found: {path}");

            string hex = File.ReadAllText(path).Trim();
            if (hex.Length % 2 != 0)
                throw new InvalidDataException("Invalid hex in password file.");

            byte[] bytes = new byte[hex.Length / 2];
            for (int i = 0; i < bytes.Length; i++)
                bytes[i] = Convert.ToByte(hex.Substring(i * 2, 2), 16);

            return bytes;
        }

        private static byte[] ComputeSHA256(byte[] input)
        {
            using var sha = SHA256.Create();
            return sha.ComputeHash(input);
        }

        private static byte[] CombineByteArrays(byte[] a, byte[] b)
        {
            byte[] result = new byte[a.Length + b.Length];
            Buffer.BlockCopy(a, 0, result, 0, a.Length);
            Buffer.BlockCopy(b, 0, result, a.Length, b.Length);
            return result;
        }

        private static async Task WriteStringAsync(NetworkStream stream, string message)
        {
            byte[] data = Encoding.UTF8.GetBytes(message + "\n");
            await stream.WriteAsync(data, 0, data.Length);
        }

        private static async Task<string> ReadStringAsync(NetworkStream stream)
        {
            StringBuilder sb = new();
            byte[] buffer = new byte[1];

            while (true)
            {
                int bytesRead = await stream.ReadAsync(buffer, 0, 1);
                if (bytesRead == 0) break; // disconnected
                if (buffer[0] == '\n') break;
                sb.Append((char)buffer[0]);
            }

            return sb.ToString();
        }

        public static async Task SavePasswordAsHexAsync(string password)
        {
            if (string.IsNullOrEmpty(password))
                throw new ArgumentException("Password cannot be empty.", nameof(password));

            // Compute SHA-256 hash of the password
            using var sha = SHA256.Create();
            byte[] hashBytes = sha.ComputeHash(Encoding.UTF8.GetBytes(password));

            // Convert hash to hex string
            string hex = BitConverter.ToString(hashBytes).Replace("-", "");

            // Write to file asynchronously
            await File.WriteAllTextAsync(passwordFile, hex);
            Console.WriteLine($"Password hash saved to {passwordFile}");
        }
    }
}
