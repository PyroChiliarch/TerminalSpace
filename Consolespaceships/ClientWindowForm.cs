using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Net;
using System.Net.Sockets;

namespace Consolespaceships
{
    public partial class ClientWindowForm : Form
    {

        
        Socket socket;

        public ClientWindowForm()
        {
            //Creat the Form
            InitializeComponent();

            //Initialise Socket
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            //Connect with the socket to the server
            socket.Connect("127.0.0.1", 8);
            
            //Begin Receive Message thread
            socket.BeginReceive(new byte[] { 0 }, 0, 0, 0, MsgReceivedCallback, null);

        }




        //=============================================================================
        //Thread call back loops
        //=============================================================================
        //Receive Message Loop
        private void MsgReceivedCallback(IAsyncResult asyncResult)
        {
            try
            {

                //Receive Message Array
                //Stop receiveing with the socket so it can be read
                socket.EndReceive(asyncResult);
                //Make a storage for the new message
                byte[] buffer = new byte[8192];
                //Fill buffer with message
                int msgLength = socket.Receive(buffer, buffer.Length, 0);
                //Resize the buffer if necessary
                if (msgLength < buffer.Length)
                {
                    Array.Resize<byte>(ref buffer, msgLength);
                }
                //Place message in a var for use
                string incomingMsg = Encoding.Default.GetString(buffer);
                //Split commands up and fill a list based on EOF char
                string[] incomingMsgs = incomingMsg.Split(';');


                //Update the listview for message history

                foreach (string msg in incomingMsgs)
                {
                    if (msg != "")
                    {
                        string[] msgSplit = msg.Split(':');

                        Invoke((MethodInvoker)delegate
                        {
                            ListViewItem item = new ListViewItem();
                            item.Text = DateTime.Now.ToString();
                            item.SubItems.Add(msgSplit[0]);
                            item.SubItems.Add(msgSplit[1]);
                            item.Tag = null;
                            listMsgHistory.Items.Add(item);
                        });
                    }

                    
                }
                
                






                //Begin the thread again and listen for another msg from the connection
                socket.BeginReceive(new byte[] { 0 }, 0, 0, 0, MsgReceivedCallback, null);
            }
            catch
            {
                throw new NotImplementedException();
            }
        }










        //=============================================================================
        //Forms Events
        //=============================================================================

        private void btnSend_Click(object sender, EventArgs e)
        {
            //Send the command
            socket.Send(Encoding.Default.GetBytes(textBoxInput.Text));
            //Empty the text box
            textBoxInput.Text = "";
        }
    }
}
