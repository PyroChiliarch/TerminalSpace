using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Numerics;

using Server2.Networking;
using Server2.GameWorld;

namespace Server2
{
    public partial class ServerWindow : Form
    {
        NetworkController networkController;
        Instance instance;


        
        public ServerWindow()
        {
            InitializeComponent();
            
            networkController = new NetworkController();
            instance = new Instance(new List<Vector3> { 
                new Vector3(0, 0, 0),
                new Vector3(0, 0, 1) } );

        }
    }
}
