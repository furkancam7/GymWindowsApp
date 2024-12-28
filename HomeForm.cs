using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using LiveCharts;
using LiveCharts.WinForms;

namespace sali
{
    public partial class HomeForm : Form
    {
        public HomeForm()
        {
            InitializeComponent();
            this.Size = new Size(1200, 800); // Form boyutunu büyüt
        }

        private void HomeForm_Load(object sender, EventArgs e)
        {
            LoadDataAndSetupLayout();
        }

        private void LoadDataAndSetupLayout()
        {
            // Tüm kontrolleri temizle
            this.Controls.Clear();

            // TableLayoutPanel oluştur
            var tableLayout = new TableLayoutPanel
            {
                RowCount = 2,
                ColumnCount = 2,
                Dock = DockStyle.Fill,
                BackColor = Color.White
            };

            tableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            tableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            tableLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 50));
            tableLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 50));

            using (var context = new GymDatabaseEntitiess())
            {
                // Veritabanından en güncel verileri çek
                int memberCount = context.Members.Count();

                var classGroups = context.Classes
                    .GroupBy(c => c.class_name)
                    .Select(g => new GroupedData { Name = g.Key, Count = g.Count() })
                    .ToList();

                var equipmentGroups = context.Equipments
                    .GroupBy(e => e.equipment_name)
                    .Select(g => new GroupedData { Name = g.Key, Count = g.Count() })
                    .ToList();

                int staffCount = context.Staffs.Count();

                // Panelleri ekle
                tableLayout.Controls.Add(CreateChartPanel("Members", memberCount), 0, 0);
                tableLayout.Controls.Add(CreateGroupedBarChartPanel("Classes", classGroups), 1, 0);
                tableLayout.Controls.Add(CreateChartPanel("Staff", staffCount), 0, 1);
                tableLayout.Controls.Add(CreateGroupedBarChartPanel("Equipments", equipmentGroups), 1, 1);
            }

            this.Controls.Add(tableLayout);
        }

        private Panel CreateChartPanel(string title, int value)
        {
            var panel = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(10),
                BackColor = Color.White, // Arka planı beyaz yap
                BorderStyle = BorderStyle.FixedSingle
            };

            var label = new Label
            {
                Text = title,
                Dock = DockStyle.Top,
                Font = new Font("Segoe UI", 16, FontStyle.Bold), // Yazı boyutunu artır
                TextAlign = ContentAlignment.MiddleCenter,
                Height = 40
            };

            var chart = new CartesianChart
            {
                Dock = DockStyle.Fill,
                AxisX = { new LiveCharts.Wpf.Axis { Labels = new[] { title }, ShowLabels = true } },
                AxisY = { new LiveCharts.Wpf.Axis { Title = "Count" } },
                Series = new SeriesCollection
                {
                    new LiveCharts.Wpf.ColumnSeries
                    {
                        Values = new LiveCharts.ChartValues<int> { value },
                        DataLabels = true,
                        Fill = System.Windows.Media.Brushes.Blue
                    }
                }
            };

            panel.Controls.Add(chart);
            panel.Controls.Add(label);

            return panel;
        }

        private Panel CreateGroupedBarChartPanel(string title, List<GroupedData> groups)
        {
            var panel = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(10),
                BackColor = Color.White, // Arka planı beyaz yap
                BorderStyle = BorderStyle.FixedSingle
            };

            var label = new Label
            {
                Text = title,
                Dock = DockStyle.Top,
                Font = new Font("Segoe UI", 16, FontStyle.Bold), // Yazı boyutunu artır
                TextAlign = ContentAlignment.MiddleCenter,
                Height = 40
            };

            var chart = new CartesianChart
            {
                Dock = DockStyle.Fill,
                AxisX = { new LiveCharts.Wpf.Axis { Labels = groups.Select(g => g.Name).ToArray() } },
                AxisY = { new LiveCharts.Wpf.Axis { Title = "Count" } },
                Series = new SeriesCollection
                {
                    new LiveCharts.Wpf.ColumnSeries
                    {
                        Values = new LiveCharts.ChartValues<int>(groups.Select(g => g.Count)),
                        DataLabels = true,
                        Fill = System.Windows.Media.Brushes.Green
                    }
                }
            };

            panel.Controls.Add(chart);
            panel.Controls.Add(label);

            return panel;
        }
    }

    public class GroupedData
    {
        public string Name { get; set; }
        public int Count { get; set; }
    }
}
