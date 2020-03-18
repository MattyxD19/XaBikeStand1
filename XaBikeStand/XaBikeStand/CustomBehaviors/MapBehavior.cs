﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XaBikeStand.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace XaBikeStand.CustomBehaviors
{
    public class MapBehavior : BehaviorBase<Map>
    {
        public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create<MapBehavior, IEnumerable<ILocationViewModel>>(
            p => p.ItemsSource, null, BindingMode.Default, null, ItemsSourceChanged);

        public IEnumerable<ILocationViewModel> ItemsSource
        {
            get { return (IEnumerable<ILocationViewModel>)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        private static void ItemsSourceChanged(BindableObject bindable, IEnumerable oldValue, IEnumerable newValue)
        {
            var behavior = bindable as MapBehavior;
            if (behavior == null) return;
            behavior.AddPins();
        }

        private void AddPins()
        {
            var map = AssociatedObject;
            for (int i = map.Pins.Count - 1; i >= 0; i--)
            {
                map.Pins[i].Clicked -= PinOnClicked;
                map.Pins.RemoveAt(i);
            }

            var pins = ItemsSource.Select(x =>
            {
                var pin = new Pin
                {
                    Type = PinType.SearchResult,
                    Position = new Position(x.Latitude, x.Longitude),
                    Label = x.Title,
                    Address = x.Description,

                };

                pin.Clicked += PinOnClicked;
                return pin;
            }).ToArray();

            foreach (var pin in pins)
                map.Pins.Add(pin);

            PositionMap();
        }

        private void PinOnClicked(object sender, EventArgs eventArgs)
        {
            var pin = sender as Pin;
            if (pin == null) return;
            var viewModel = ItemsSource.FirstOrDefault(x => x.Title == pin.Label);
            if (viewModel == null) return;
            viewModel.Command.Execute(null);
        }

        private void PositionMap()
        {
            if (ItemsSource == null || !ItemsSource.Any()) return;

            var centerPosition = new Position(ItemsSource.Average(x => x.Latitude), ItemsSource.Average(x => x.Longitude));

            var minLongitude = ItemsSource.Min(x => x.Longitude);
            var minLatitude = ItemsSource.Min(x => x.Latitude);

            var maxLongitude = ItemsSource.Max(x => x.Longitude);
            var maxLatitude = ItemsSource.Max(x => x.Latitude);
        }
    }
}