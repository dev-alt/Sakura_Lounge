using System;

namespace SakuraLounge.Classes
{
    public class Wheel
    {
        public bool IsClicked { get; private set; }
        private Uri[] wheelImageUris;


        public Wheel()
        {
            wheelImageUris = new Uri[6];
        }
        public void ToggleClicked()
        {
            IsClicked = !IsClicked;
        }

        public void SetImageUri(int index)
        {
            wheelImageUris[0] = new Uri("ms-appx:///Assets/coins.png", UriKind.RelativeOrAbsolute);
            wheelImageUris[1] = new Uri("ms-appx:///Assets/Cherry_blossom_Charm.png", UriKind.RelativeOrAbsolute);
            wheelImageUris[2] = new Uri("ms-appx:///Assets/Bonsai_Tree.png", UriKind.RelativeOrAbsolute);
            wheelImageUris[3] = new Uri("ms-appx:///Assets/Koi_Fish.png", UriKind.RelativeOrAbsolute);
            wheelImageUris[4] = new Uri("ms-appx:///Assets/Lucky_Lantern.png", UriKind.RelativeOrAbsolute);
            wheelImageUris[5] = new Uri("ms-appx:///Assets/dragon_slot_icon.png", UriKind.RelativeOrAbsolute);
        }
    }
}
