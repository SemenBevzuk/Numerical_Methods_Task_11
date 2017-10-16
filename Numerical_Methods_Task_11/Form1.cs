using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;

namespace Numerical_Methods_Task_11
{
    public partial class Form1 : Form
    {
        private int CounterOfTests = 0;
        public Form1()
        {
            InitializeComponent();
            cartesianChart_u1.DefaultLegend = new DefaultLegend { Visibility = Visibility.Visible };
            cartesianChart_u1.LegendLocation = LegendLocation.Bottom;
            cartesianChart_u2.DefaultLegend = new DefaultLegend { Visibility = Visibility.Visible };
            cartesianChart_u2.LegendLocation = LegendLocation.Bottom;
            cartesianChart_u3.DefaultLegend = new DefaultLegend { Visibility = Visibility.Visible };
            cartesianChart_u3.LegendLocation = LegendLocation.Bottom;

            InitDataGridMetodInfo();
            InitDataGridTaskInfo();
        }

        private void button_reset_Click(object sender, EventArgs e)
        {
            cartesianChart_u1.Series.Clear();
            cartesianChart_u2.Series.Clear();
            cartesianChart_u3.Series.Clear();
            InitDataGridMetodInfo();
            InitDataGridTaskInfo();
            CounterOfTests = 0;
        }

        private void button_start_Click(object sender, EventArgs e)
        {
            CounterOfTests++;
            SystemFunctions systemFunctions = new SystemFunctions(
                Convert.ToDouble(textBox_alfa1.Text),Convert.ToDouble(textBox_alfa2.Text),
                Convert.ToDouble(textBox_omega1.Text),Convert.ToDouble(textBox_omega2.Text),
                Convert.ToDouble(textBox_betta1.Text),Convert.ToDouble(textBox_betta2.Text),
                Convert.ToDouble(textBox_gamma1.Text),Convert.ToDouble(textBox_gamma2.Text)
                );
            Runge_Kutta_3_System rungeKutta3System = new Runge_Kutta_3_System(
                Convert.ToDouble(textBox_x0.Text), Convert.ToDouble(textBox_u_0_1.Text), 
                Convert.ToDouble(textBox_u_0_2.Text), Convert.ToDouble(textBox_u_0_3.Text),
                Convert.ToDouble(textBox_h.Text), Convert.ToDouble(textBox_eps.Text),
                Convert.ToDouble(textBox_borderAccuracy.Text), Convert.ToInt32(textBox_max_steps.Text),
                checkBox_StepControl.Checked, 
                systemFunctions.FunctionV1, systemFunctions.FunctionV2, systemFunctions.FunctionV3
                );
            rungeKutta3System.Run();
            cartesianChart_u1.Series.Add(new LineSeries
            {
                Title = "Численное решение #" + Convert.ToString(CounterOfTests) + ".1",
                Values = new ChartValues<ObservablePoint>(rungeKutta3System
                    .GetPoints()
                    .Select(_ => new ObservablePoint(_.X, _.V1))),
                PointGeometrySize = 5
            });
            cartesianChart_u2.Series.Add(new LineSeries
            {
                Title = "Численное решение #" + Convert.ToString(CounterOfTests) + ".2",
                Values = new ChartValues<ObservablePoint>(rungeKutta3System
                    .GetPoints()
                    .Select(_ => new ObservablePoint(_.X, _.V2))),
                PointGeometrySize = 5
            });
            cartesianChart_u3.Series.Add(new LineSeries
            {
                Title = "Численное решение #" + Convert.ToString(CounterOfTests) + ".3",
                Values = new ChartValues<ObservablePoint>(rungeKutta3System
                    .GetPoints()
                    .Select(_ => new ObservablePoint(_.X, _.V3))),
                PointGeometrySize = 5
            });
        }

        private void InitDataGridMetodInfo()
        {
            dataGridView_MetodInfo.Rows.Clear();
            dataGridView_MetodInfo.Columns.Clear();
            dataGridView_MetodInfo.RowCount = 1;
            dataGridView_MetodInfo.ColumnCount = 14;
            dataGridView_MetodInfo.Columns[0].HeaderText = "i";
            dataGridView_MetodInfo.Columns[1].HeaderText = "h_(i-1)";
            dataGridView_MetodInfo.Columns[2].HeaderText = "x_i";
            dataGridView_MetodInfo.Columns[3].HeaderText = "v_i";
            dataGridView_MetodInfo.Columns[4].HeaderText = "v_i_удв";
            dataGridView_MetodInfo.Columns[5].HeaderText = "v_i - v_i_удв";
            dataGridView_MetodInfo.Columns[6].HeaderText = "S";
            dataGridView_MetodInfo.Columns[7].HeaderText = "e";
            dataGridView_MetodInfo.Columns[8].HeaderText = "v_i_уточ";
            dataGridView_MetodInfo.Columns[9].HeaderText = "v_i_итог";
            dataGridView_MetodInfo.Columns[10].HeaderText = "u_i";
            dataGridView_MetodInfo.Columns[11].HeaderText = "|u_i - v_i|";
            dataGridView_MetodInfo.Columns[12].HeaderText = "Ум. шага";
            dataGridView_MetodInfo.Columns[13].HeaderText = "Ув. шага";
            dataGridView_MetodInfo.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView_MetodInfo.AutoResizeRowHeadersWidth(DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders);
        }

        private void InitDataGridTaskInfo()
        {
            dataGridView_TaskInfo.Rows.Clear();
            dataGridView_TaskInfo.Columns.Clear();
            dataGridView_TaskInfo.RowCount = 1;
            dataGridView_TaskInfo.ColumnCount = 8;
            dataGridView_TaskInfo.Columns[0].HeaderText = "#";
            dataGridView_TaskInfo.Columns[1].HeaderText = "alfa";
            dataGridView_TaskInfo.Columns[2].HeaderText = "sigma";
            dataGridView_TaskInfo.Columns[3].HeaderText = "x0";
            dataGridView_TaskInfo.Columns[4].HeaderText = "u0";
            dataGridView_TaskInfo.Columns[5].HeaderText = "h0";
            dataGridView_TaskInfo.Columns[6].HeaderText = "e";
            dataGridView_TaskInfo.Columns[7].HeaderText = "Максимум итераций";
            dataGridView_TaskInfo.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView_TaskInfo.AutoResizeRowHeadersWidth(DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders);
        }
    }
}
