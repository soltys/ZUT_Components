using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Komponentowe
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            LoadPlugins();


        }

        private void LoadPlugins()
        {
            var menu = pluginsMenu;
            for (int i = 0; i < 4; i++)
            {
                menu.DropDownItems.Add(new ToolStripMenuItem("Opis", null, onClick: MenuHandler, name: "Opis " + i));
            }

            var workingPath = System.IO.Path.GetDirectoryName(Application.ExecutablePath);
            foreach (var dllFileName in Directory.GetFiles(workingPath, "*.dll", SearchOption.AllDirectories))
            {

                var plugin = Assembly.LoadFile(dllFileName);
                foreach (var type in plugin.GetTypes())
                {
                    var getPluginName = type.GetMethod("GetPluginName");
                    if (getPluginName != null)
                    {
                        var methodName = (string)getPluginName.Invoke(null, new object[] { });
                        menu.DropDownItems.Add(new ToolStripMenuItem(methodName, null, onClick: (sender, e) =>
                        {
                            var method = type.GetMethod(methodName);
                            var parameters = method.GetParameters();
                            if (parameters[0].ParameterType.Name.Contains("String"))
                            {
                                this.rtbMain.Text = (string)method.Invoke(null, new object[] { this.rtbMain.Text });
                            }
                            else
                            {
                                method.Invoke(null, new object[] { this.rtbMain });
                            }

                        }, name: methodName));
                    }
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.DefaultExt = "*.rtf";
            saveFileDialog.Filter = "RTF Files|*.rtf";
            if (saveFileDialog.ShowDialog() == DialogResult.OK && saveFileDialog.FileName.Length > 0)
            {
                rtbMain.SaveFile(saveFileDialog.FileName);
            }
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.DefaultExt = "*.rtf";
            openFileDialog.Filter = "RTF Files|*.rtf";
            if (openFileDialog.ShowDialog() == DialogResult.OK && openFileDialog.FileName.Length > 0)
            {
                rtbMain.LoadFile(openFileDialog.FileName);
            }
        }

        private void btnInsertMenuItem_Click(object sender, EventArgs e)
        {
            string menuItemName = txtBoxMenuName.Text;
            if (!pluginsMenu.DropDownItems.ContainsKey(menuItemName))
            {
                pluginsMenu.DropDownItems.Add(new ToolStripMenuItem(menuItemName, null, onClick: MenuHandler, name: menuItemName));
            }
        }

        private void MenuHandler(object sender, EventArgs e)
        {
            ToolStripMenuItem item = (ToolStripMenuItem)sender;
            MessageBox.Show("Wciśnięto menu.\n Klucz: " + item.Name + "Napis: " + item.Text);
        }



    }
}
