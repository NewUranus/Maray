using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using LiveChartsCore.SkiaSharpView;
using LiveChartsCore;

using Maray.Configs;
using Maray.Services;
using Maray.V2ray;
using Maray.Views;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;
using System.Collections.ObjectModel;
using LiveChartsCore.Defaults;
using System;
using LiveChartsCore.Kernel.Sketches;

namespace Maray.ViewModels
{
    public partial class MainPageVM : ObservableObject
    {
        public MainPageVM()
        {
            ShowRunningServer();
            InitSeries();

            Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    RunRandomSeries();
                }
            });
        }

        [RelayCommand]
        private void ShowRunningServer()
        {
        }

        #region LiveCharts

        public Axis[] XAxes { get; set; } =
        {
            new Axis
            {
                Labeler = value => TimeSpan.FromSeconds((long)value).ToString("fff") + " s",

                // when using a date time type, let the library know your unit
                 UnitWidth = TimeSpan.FromSeconds(1).Ticks,

                // if the difference between our points is in hours then we would: UnitWidth = TimeSpan.FromHours(1).Ticks,

                // since all the months and years have a different number of days we can use the average, it would not cause any visible error in the user interface
                // Months: TimeSpan.FromDays(30.4375).Ticks
                // Years: TimeSpan.FromDays(365.25).Ticks

                // The MinStep property forces the separator to be greater than 1 ms.
                MinStep = TimeSpan.FromSeconds(1).Ticks,
            }
        };

        [ObservableProperty]
        public ObservableCollection<ISeries> series;

        private readonly ObservableCollection<ObservableValue> _observableValues = new ObservableCollection<ObservableValue>();

        private void InitSeries()
        {
            Series = new ObservableCollection<ISeries>()
            {
                new LineSeries<ObservableValue>()
                {
                    Values = _observableValues,
                    Stroke = null,
                    GeometrySize = 0,
                    Fill = new SolidColorPaint(SKColors.LightBlue),
                },
            };
        }

        private readonly Random _random = new();

        private void RunRandomSeries()
        {
            var randomValue = _random.Next(1, 10);
            _observableValues.Add(new ObservableValue(randomValue));

            if (_observableValues.Count > 20)
            {
                _observableValues.RemoveAt(0);
            }

            Thread.Sleep(1000);
        }

        #endregion LiveCharts
    }
}