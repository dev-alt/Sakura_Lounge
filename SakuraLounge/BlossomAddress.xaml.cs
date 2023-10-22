using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Maps;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace SakuraLounge
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class BlossomAddress : Page
    {
        private GeoboundingBox boundingBox;
        private Geopoint defaultpos;
        private Geopoint northwestCorner;
        private Geopoint southeastCorner;
        private bool recenterButtonClicked = false;
        private MapIcon aucklandIcon;
        private MapIcon wellingtonIcon;
        private MapIcon christchurchIcon;
        private MapIcon dunedinIcon;
        private MapIcon hamiltonIcon;

        public BlossomAddress()
        {
            this.InitializeComponent();
            aucklandIcon = new MapIcon
            {
                Location = new Geopoint(new BasicGeoposition { Latitude = -36.891164, Longitude = 174.684893 }),
                Title = "Dragon's Haven",
            };
            wellingtonIcon = new MapIcon
            {
                Location = new Geopoint(new BasicGeoposition { Latitude = -41.255073, Longitude = 174.761368 }),
                Title = "Lotus Isle",
            };
            christchurchIcon = new MapIcon
            {
                Location = new Geopoint(new BasicGeoposition { Latitude = -43.532054, Longitude = 172.636225 }),
                Title = "Phoenix Sanctuary",
            };
            dunedinIcon = new MapIcon
            {
                Location = new Geopoint(new BasicGeoposition { Latitude = -45.878760, Longitude = 170.502798 }),
                Title = "Jade Peaks Village",
            };
            hamiltonIcon = new MapIcon
            {
                Location = new Geopoint(new BasicGeoposition { Latitude = -37.787001, Longitude = 175.279253 }),
                Title = "Sakura Grove",
            };

            MyMap.MapElements.Add(aucklandIcon);
            MyMap.MapElements.Add(wellingtonIcon);
            MyMap.MapElements.Add(christchurchIcon);
            MyMap.MapElements.Add(dunedinIcon);
            MyMap.MapElements.Add(hamiltonIcon);


            double southLatitude = -47.25; // Southern border
            double westLongitude = 166.25; // Western border
            double northLatitude = -34.00; // Northern border
            double eastLongitude = 178.75; // Eastern border

            Geopoint center = new Geopoint(new BasicGeoposition
            {
                Latitude = (southLatitude + northLatitude) / 2,
                Longitude = (westLongitude + eastLongitude) / 2
            });

            // Compute the width and height of the bounding box
            double width = Math.Abs(eastLongitude - westLongitude);
            double height = Math.Abs(northLatitude - southLatitude);


            defaultpos = new Geopoint(new BasicGeoposition
            {
                Latitude = -41.255073,
                Longitude = 174.761368,
            });

            northwestCorner = new Geopoint(new BasicGeoposition
            {
                Latitude = center.Position.Latitude + height / 2,
                Longitude = center.Position.Longitude - width / 2
            });

            southeastCorner = new Geopoint(new BasicGeoposition
            {
                Latitude = center.Position.Latitude - height / 2,
                Longitude = center.Position.Longitude + width / 2
            });

            boundingBox = new GeoboundingBox(northwestCorner.Position, southeastCorner.Position);

            MyMap.CenterChanged += MapCenterChanged;
            MyMap.ZoomLevelChanged += MapZoomLevelChanged;
            MyMap.MapElementClick += MyMap_MapElementClick; 
        }



        private void MapCenterChanged(MapControl sender, object args)
        {
            if (!recenterButtonClicked)
            {
                var center = sender.Center;
                if (!IsGeopointInBoundingBox(center))
                {
                    // If the user pans out of the defined bounding box and the button wasn't clicked,
                    // reset it to the northwestCorner
                    sender.Center = defaultpos;
                }
            }
            else
            {
                // Reset the flag
                recenterButtonClicked = false;
            }
        }

        private void MapZoomLevelChanged(MapControl sender, object args)
        {
            // You can also limit the zoom level here if needed
        }

        private bool IsGeopointInBoundingBox(Geopoint point)
        {
            return point.Position.Latitude >= southeastCorner.Position.Latitude &&
                   point.Position.Latitude <= northwestCorner.Position.Latitude &&
                   point.Position.Longitude >= northwestCorner.Position.Longitude &&
                   point.Position.Longitude <= southeastCorner.Position.Longitude;
        }

        private void RecenterMapButton_Click(object sender, RoutedEventArgs e)
        {
            MyMap.Center = new Geopoint(new BasicGeoposition
            {
                Latitude = -36.891164, 
                Longitude = 174.684893, 
            });

            recenterButtonClicked = true;
        }
        private void MyMap_MapElementClick(MapControl sender, MapElementClickEventArgs args)
        {
            if (args.MapElements[0] is MapIcon clickedIcon)
            {
                Debug.WriteLine("MapElementClick event triggered.");
                Debug.WriteLine($"clickedIcon.Title: {clickedIcon.Title}");

                CityName.Text = clickedIcon.Title;

                var point = args.Position;
                Debug.WriteLine($"HorizontalOffset: {point.X}");
                Debug.WriteLine($"VerticalOffset: {point.Y}");

                // Open the popup
                InfoPopup.IsOpen = true;
            }
        }



        private void LocationComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (LocationComboBox.SelectedItem is ComboBoxItem selectedItem)
            {
                string locationTag = selectedItem.Tag as string;

                aucklandIcon.Visible = false;
                wellingtonIcon.Visible = false;
                christchurchIcon.Visible = false;
                dunedinIcon.Visible = false;
                hamiltonIcon.Visible = false;

                switch (locationTag)
                {
                    case "Auckland":
                        MyMap.Center = aucklandIcon.Location;
                        aucklandIcon.Visible = true;
                        break;
                    case "Wellington":
                        MyMap.Center = wellingtonIcon.Location;
                        wellingtonIcon.Visible = true;
                        break;
                    case "Christchurch":
                        MyMap.Center = christchurchIcon.Location;
                        christchurchIcon.Visible = true;
                        break;
                    case "Dunedin":
                        MyMap.Center = dunedinIcon.Location;
                        dunedinIcon.Visible = true;
                        break;
                    case "Hamilton":
                        MyMap.Center = hamiltonIcon.Location;
                        hamiltonIcon.Visible = true;
                        break;
                }
            }
        }


        private void MyMap_Tapped(object sender, TappedRoutedEventArgs e)
        {
            // Hide the pop-up
            InfoPopup.IsOpen = false;
        }

        private void MyMap_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            InfoPopup.IsOpen = false;
        }

        private void InfoPopup_Tapped(object sender, TappedRoutedEventArgs e)
        {
            InfoPopup.IsOpen = false;
        }

        private void CloseInfoPopup_Click(object sender, RoutedEventArgs e)
        {
            InfoPopup.IsOpen = false;
        }
    }
}