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

        [ObservableProperty]
        public ObservableCollection<ISeries> seriesList;

        private readonly ObservableCollection<ObservableValue> _observableValues = new ObservableCollection<ObservableValue>();

        private void InitSeries()
        {
            SeriesList = new ObservableCollection<ISeries>()
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