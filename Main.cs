using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AdventOfCode2021
{
    public partial class Main : Form
    {
        private static List<string> projects = new List<string>() {"day1"};

        public enum LogLevel
        {
            Result1,
            Result2,
            Normal,
            Debug
        }

        public Main()
        {
            InitializeComponent();
            foreach (var project in projects)
            {
                cmbDaySelector.Items.Add(project);
            }

            cmbDaySelector.SelectedIndex = projects.Count - 1;
        }

        private ProjectDay GetDayForSelectedIndex()
        {
            switch (cmbDaySelector.SelectedIndex)
            {
                case 0:
                    return new Day1();
                    break;

                default:
                    throw new Exception("not yet supported");
            }
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            ProjectDay projectDay = GetDayForSelectedIndex();
            projectDay.SetLogFunction(LogFunction);
            projectDay.Run();
        }

        private void LogFunction(string message, LogLevel logLevel)
        {
            switch (logLevel)
            {
                case LogLevel.Result1:
                    tbResult1.Text = message;
                    break;
                case LogLevel.Result2:
                    tbResult2.Text = message;
                    break;
                default:
                    lbLog.Items.Add(message);
                    break;
            }
        }
    }
}