/*using Microsoft.Web.WebView2.WinForms;
using System;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;

namespace adsl_Auto_Interaction_App
{
    public class ValidateConnection
    {
        private readonly WebView2 _web;
        private readonly string _serverIp;
        private readonly int _serverPort;
        private readonly byte[] _passwordHash;

        /// <summary>
        /// Synchronous constructor. Loads server data and password hash.
        /// </summary>
        /// <param name="webView">The WebView2 instance to display server messages.</param>
        /// <param name="passwordFile">Path to the password hash file (hex string).</param>
        public ValidateConnection(WebView2 webView, string passwordFile)
        {
            _web = webView;

            // Load server data (synchronous)
            (string ip, int port) = FileManager.ServerData.GetServerData();
            _serverIp = ip;
            _serverPort = port;

            // Load password hash from file
            FileManager.ServerData.Prepare();
            string hashHex = System.IO.File.ReadAllText(passwordFile).Trim();
            _passwordHash = StringToByteArray(hashHex);
        }

        /// <summary>
        /// Connects to the server and authenticates using challenge-response.
        /// </summary>
        public void ConnectAndAuthenticate()
        {
            try
            {
                using TcpClient client = new TcpClient();
                client.Connect(_serverIp, _serverPort);
                using NetworkStream stream = client.GetStream();

                // Step 1: Send initial request
                WriteString(stream, "AUTH");

                // Step 2: Read challenge from server
                string challenge = ReadString(stream);

                // Step 3: Compute response hash (passwordHash + challenge)
                byte[] combined = CombineByteArrays(_passwordHash, Encoding.UTF8.GetBytes(challenge));
                byte[] responseHash = ComputeSHA256(combined);
                string responseHex = BitConverter.ToString(responseHash).Replace("-", "");

                // Step 4: Send response to server
                WriteString(stream, responseHex);

                // Step 5: Read server reply
                string serverReply = ReadString(stream);

                // Display server message in WebView2
                Notification n = new Notification();
                n.Up(NoticficationStyle.Info, serverReply, 10000);
            }
            catch (Exception ex)
            {
                Notification n = new Notification();
                n.Up(NoticficationStyle.Error, ex.Message);
            }
        }

        // === Helper methods ===

        private static void WriteString(NetworkStream stream, string message)
        {
            byte[] data = Encoding.UTF8.GetBytes(message + "\n");
            stream.Write(data, 0, data.Length);
        }

        private static string ReadString(NetworkStream stream)
        {
            StringBuilder sb = new();
            int b;
            while ((b = stream.ReadByte()) != -1)
            {
                if (b == '\n') break;
                sb.Append((char)b);
            }
            return sb.ToString();
        }

        private static byte[] ComputeSHA256(byte[] inputBytes)
        {
            using var sha = SHA256.Create();
            return sha.ComputeHash(inputBytes);
        }

        private static byte[] CombineByteArrays(byte[] a, byte[] b)
        {
            byte[] result = new byte[a.Length + b.Length];
            Buffer.BlockCopy(a, 0, result, 0, a.Length);
            Buffer.BlockCopy(b, 0, result, a.Length, b.Length);
            return result;
        }

        private static byte[] StringToByteArray(string hex)
        {
            int length = hex.Length;
            byte[] data = new byte[length / 2];
            for (int i = 0; i < length; i += 2)
                data[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            return data;
        }
    }
}
*/