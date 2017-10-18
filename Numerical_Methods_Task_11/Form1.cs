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

        private List<ExperimentInfo> ExperimentInfos = new List<ExperimentInfo>(); 
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
            ExperimentInfos.Clear();
            comboBox_TaskSelector.Items.Clear();
            CounterOfTests = 0;
        }

        private void button_start_Click(object sender, EventArgs e)
        {
            InitDataGridTaskInfo();
            InitDataGridMetodInfo();
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

            List<MetodInfo> metodInfos = rungeKutta3System.GetMetodInfos();
            metodInfos.ForEach(_ =>
                dataGridView_MetodInfo.Rows.Add
                (_.Iteration, _.H, _.X, _.V1, _.V2, _.V3, _.S, _.e, _.VCorr, _.CountPlusH, _.CountMinusH));
            dataGridView_MetodInfo.AutoResizeColumns();

            TaskInfo taskInfo = new TaskInfo(CounterOfTests,
                                             Convert.ToDouble(textBox_alfa1.Text), Convert.ToDouble(textBox_alfa2.Text),
                                             Convert.ToDouble(textBox_omega1.Text), Convert.ToDouble(textBox_omega2.Text),
                                             Convert.ToDouble(textBox_betta1.Text), Convert.ToDouble(textBox_betta2.Text),
                                             Convert.ToDouble(textBox_gamma1.Text), Convert.ToDouble(textBox_gamma2.Text),
                                             Convert.ToDouble(textBox_x0.Text), Convert.ToDouble(textBox_u_0_1.Text),
                                             Convert.ToDouble(textBox_u_0_2.Text), Convert.ToDouble(textBox_u_0_3.Text),
                                             Convert.ToDouble(textBox_h.Text), Convert.ToDouble(textBox_eps.Text),
                                             Convert.ToDouble(textBox_borderAccuracy.Text), Convert.ToInt32(textBox_max_steps.Text));
            dataGridView_TaskInfo.Rows.Add(taskInfo.Number, taskInfo.Alfa1, taskInfo.Alfa2, taskInfo.Omega1, taskInfo.Omega2,
                                           taskInfo.Betta1, taskInfo.Betta2, taskInfo.Gamma1, taskInfo.Gamma2,
                                           taskInfo.X0, taskInfo.U_1_0, taskInfo.U_2_0, taskInfo.U_3_0,
                                           taskInfo.h0, taskInfo.e, taskInfo.Border_eps, taskInfo.Max_iteration);
            dataGridView_MetodInfo.AutoResizeColumns();
            dataGridView_TaskInfo.AutoResizeColumns();

            ExperimentInfos.Add(new ExperimentInfo(taskInfo, metodInfos));

            comboBox_TaskSelector.Items.Add("Испытание №" + Convert.ToString(CounterOfTests));

            cartesianChart_u1.Series.Add(new LineSeries
            {
                Title = "Первая популяция жертв #" + Convert.ToString(CounterOfTests) + ".1",
                Values = new ChartValues<ObservablePoint>(rungeKutta3System
                    .GetPoints()
                    .Select(_ => new ObservablePoint(_.X, _.V1))),
                PointGeometrySize = 5
            });
            cartesianChart_u2.Series.Add(new LineSeries
            {
                Title = "Вторая популяция жертв #" + Convert.ToString(CounterOfTests) + ".2",
                Values = new ChartValues<ObservablePoint>(rungeKutta3System
                    .GetPoints()
                    .Select(_ => new ObservablePoint(_.X, _.V2))),
                PointGeometrySize = 5
            });
            cartesianChart_u3.Series.Add(new LineSeries
            {
                Title = "Популяция хищников #" + Convert.ToString(CounterOfTests) + ".3",
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
            dataGridView_MetodInfo.ColumnCount = 11;
            dataGridView_MetodInfo.Columns[0].HeaderText = "i";
            dataGridView_MetodInfo.Columns[1].HeaderText = "h_(i-1)";
            dataGridView_MetodInfo.Columns[2].HeaderText = "x_i";
            dataGridView_MetodInfo.Columns[3].HeaderText = "v_1_i";
            dataGridView_MetodInfo.Columns[4].HeaderText = "v_2_i";
            dataGridView_MetodInfo.Columns[5].HeaderText = "v_3_i";
            dataGridView_MetodInfo.Columns[6].HeaderText = "S";
            dataGridView_MetodInfo.Columns[7].HeaderText = "e";
            dataGridView_MetodInfo.Columns[8].HeaderText = "v_i_уточ";
            dataGridView_MetodInfo.Columns[9].HeaderText = "Ум. шага";
            dataGridView_MetodInfo.Columns[10].HeaderText = "Ув. шага";
            dataGridView_MetodInfo.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView_MetodInfo.AutoResizeRowHeadersWidth(DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders);
        }

        private void InitDataGridTaskInfo()
        {
            dataGridView_TaskInfo.Rows.Clear();
            dataGridView_TaskInfo.Columns.Clear();
            dataGridView_TaskInfo.RowCount = 1;
            dataGridView_TaskInfo.ColumnCount = 17;
            dataGridView_TaskInfo.Columns[0].HeaderText = "#";
            dataGridView_TaskInfo.Columns[1].HeaderText = "alfa1";
            dataGridView_TaskInfo.Columns[2].HeaderText = "alfa2";
            dataGridView_TaskInfo.Columns[3].HeaderText = "omaga1";
            dataGridView_TaskInfo.Columns[4].HeaderText = "omega2";
            dataGridView_TaskInfo.Columns[5].HeaderText = "betta1";
            dataGridView_TaskInfo.Columns[6].HeaderText = "betta2";
            dataGridView_TaskInfo.Columns[7].HeaderText = "gamma1";
            dataGridView_TaskInfo.Columns[8].HeaderText = "gamma2";
            dataGridView_TaskInfo.Columns[9].HeaderText = "x0";
            dataGridView_TaskInfo.Columns[10].HeaderText = "u_1_0";
            dataGridView_TaskInfo.Columns[11].HeaderText = "u_2_0";
            dataGridView_TaskInfo.Columns[12].HeaderText = "u_3_0";
            dataGridView_TaskInfo.Columns[13].HeaderText = "h0";
            dataGridView_TaskInfo.Columns[14].HeaderText = "e";
            dataGridView_TaskInfo.Columns[15].HeaderText = "Точность на границе";
            dataGridView_TaskInfo.Columns[16].HeaderText = "Максимум итераций";
            dataGridView_TaskInfo.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView_TaskInfo.AutoResizeRowHeadersWidth(DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders);
        }

        private void comboBox_TaskSelector_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataGridView_MetodInfo.Rows.Clear();
            dataGridView_TaskInfo.Rows.Clear();
            int index = comboBox_TaskSelector.SelectedIndex;

            dataGridView_TaskInfo.Rows.Add(ExperimentInfos[index].TaskInformation.Number, 
                ExperimentInfos[index].TaskInformation.Alfa1, ExperimentInfos[index].TaskInformation.Alfa2, 
                ExperimentInfos[index].TaskInformation.Omega1, ExperimentInfos[index].TaskInformation.Omega2,
                ExperimentInfos[index].TaskInformation.Betta1, ExperimentInfos[index].TaskInformation.Betta2, 
                ExperimentInfos[index].TaskInformation.Gamma1, ExperimentInfos[index].TaskInformation.Gamma2,
                ExperimentInfos[index].TaskInformation.X0, 
                ExperimentInfos[index].TaskInformation.U_1_0, ExperimentInfos[index].TaskInformation.U_2_0, ExperimentInfos[index].TaskInformation.U_3_0,
                ExperimentInfos[index].TaskInformation.h0, ExperimentInfos[index].TaskInformation.e, 
                ExperimentInfos[index].TaskInformation.Border_eps, ExperimentInfos[index].TaskInformation.Max_iteration);
            dataGridView_TaskInfo.AutoResizeColumns();

            ExperimentInfos[index].MetodInformation.ForEach(_ =>
                dataGridView_MetodInfo.Rows.Add
                (_.Iteration, _.H, _.X, _.V1, _.V2, _.V3, _.S, _.e, _.VCorr, _.CountPlusH, _.CountMinusH));
            dataGridView_MetodInfo.AutoResizeColumns();
        }
    }
}
