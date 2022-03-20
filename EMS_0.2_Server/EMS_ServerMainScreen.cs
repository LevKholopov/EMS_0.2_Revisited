using System.Net.Sockets;
using EMS_Library.Network;
using System.Data.SqlClient;
using System.Data.Sql;
namespace EMS_Server
{
    public partial class EMS_ServerMainScreen : Form
    {

        #region Variables
        TcpListener listener = new TcpListener(System.Net.IPAddress.Parse(EMS_Library.Config.ServerIP), EMS_Library.Config.ServerPort);
        TcpClient client = null;
        #endregion

        #region Tasks&Tokens
        public Task listeningTask;
        public CancellationTokenSource listner_CXL_Src = new CancellationTokenSource();
        CancellationToken listner_CXL;

        public Task SQLServerLookup;
        public CancellationTokenSource SQLLookup_CXL_Src = new CancellationTokenSource();
        CancellationToken SQLLookup_CXL;

        //public Task SQLServerLookup;

        #endregion

        public EMS_ServerMainScreen()
        {
            InitializeComponent();
            listeningTask = BuildServerTask();
            listner_CXL = listner_CXL_Src.Token;
            SQLServerLookup = BuildSQLServerLookup();
            SQLLookup_CXL = SQLLookup_CXL_Src.Token;
        }

        #region Event Methods
        private void EMS_ServerMainScreen_Load(object sender, EventArgs e)
        {
            listener.Start();
            listeningTask.Start();
            SQLServerLookup.Start();
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void listnerTimer_Tick(object sender, EventArgs e)
        {
            client.Close();
            client.Dispose();
            WriteToServerConsole("Client aborted!");
            listnerTimer.Stop();
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Testing();
        }
        private void txtServerConsole_TextChanged(object sender, EventArgs e)
        {
            string[] temp = txtServerConsole.Text.Split(Environment.NewLine);
            switch (temp[temp.Length - 2].ToLower())
            {
                default: break;
                case "close": { Close(); break; }
                case "terminate": { Close(); break; }
                case "exit": { Close(); break; }
                case "shutdown": { Close(); break; }
            }
        }
        #endregion

        #region NonEvent Methods
        public void WriteToServerConsole(string text)
        {
            this.Invoke((MethodInvoker)delegate
            {
                txtServerConsole.AppendText(text+Environment.NewLine);
                Console.WriteLine(text + "\n");
            });
        }
        public Task BuildServerTask()
        {
            return new Task(() => {
                WriteToServerConsole("Server started");
                while (true)
                {
                    WriteToServerConsole($"Listening...");
                    client = listener.AcceptTcpClient();
                    WriteToServerConsole($"client: " + client.Client.RemoteEndPoint);
                    listnerTimer.Start();
                    try 
                    { 
                        NetworkStream stream = client.GetStream();
                        DataPacket data = new DataPacket(stream);
                        WriteToServerConsole("Request:\n"+data.StringData);
                        string responseStr = new MyRouter().Router(data);
                        DataPacket response = new DataPacket(responseStr, 255);
                        WriteToServerConsole(responseStr);
                        stream.Write(response.Write(),0,response.GetTotalSize());
                        client.Close();
                        client.Dispose();
                    }
                    catch { }
                    WriteToServerConsole("Connection closed.");
                }
            }, listner_CXL);
        }
        public Task BuildSQLServerLookup()
        {
            return new Task(() =>
            {
                while (true)
                {
                    string SQLConnectionString = $"" +
                        $"server={EMS_Library.Config.SQLServerName};" +
                        $"database={EMS_Library.Config.SQLDatabaseName};" +
                        $"Integrated Security=SSPI;" +
                        $"Connection Timeout=1";
                    SqlConnection connenction = new SqlConnection(SQLConnectionString);

                    WriteToServerConsole("attempting " + SQLConnectionString);
                    try { connenction.Open(); }
                    catch
                    {
                        if (EMS_Library.Config.ServerNamesIterator > EMS_Library.Config.SQLServerNames.Length)
                        { MessageBox.Show("Couldn't find sql server!"); Close(); }
                    }
                    if (connenction.State == System.Data.ConnectionState.Open)
                    {
                        WriteToServerConsole("SQL Server Found.");
                        connenction.Close();
                        connenction.Dispose();
                        EMS_Library.Config.SQLConnectionString = SQLConnectionString;
                        break;
                    }
                }
            },SQLLookup_CXL);
        }

        #endregion

        private void Testing()
        {
            //TcpClient tcpClient = new TcpClient(EMS_Library.Config.ServerIP, EMS_Library.Config.ServerPort);
            //NetworkStream stream = tcpClient.GetStream();
            //DataPacket data = new DataPacket($"select * from Employees", 254);
            //stream.Write(data.Write(), 0, data.GetTotalSize());
            //WriteToServerConsole("Client made a request!");
            //DataPacket response = new DataPacket(stream);
            //WriteToServerConsole($"Client recieved response [{response}]");
        }

    }
}