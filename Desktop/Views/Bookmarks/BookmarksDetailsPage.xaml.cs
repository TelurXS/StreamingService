using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Metflix.Core;
using Metflix.Core.Models;

namespace VideoDemos.Views.Bookmarks;

public partial class BookmarksDetailsPage : ContentPage
{
    private int _bookmarkId;

    private List<Banner> _banners = new List<Banner>()
    {
        new Banner(0, "big_buck_bunny.png", "Big Buck Bunny",
            "http://commondatastorage.googleapis.com/gtv-videos-bucket/sample/BigBuckBunny.mp4",
            "Big Buck Bunny tells the story of a giant rabbit with a heart bigger than himself. When one sunny day three rodents rudely harass him, something snaps... and the rabbit ain't no bunny anymore!"),
        new Banner(1, "elephant_dream.png", "Elephant Dream",
            "http://commondatastorage.googleapis.com/gtv-videos-bucket/sample/ElephantsDream.mp4",
            "The first Blender Open Movie from 2006"),
        new Banner(2, "for_bigger_blaze.png", "For Bigger Blazes",
            "http://commondatastorage.googleapis.com/gtv-videos-bucket/sample/ForBiggerBlazes.mp4",
            "HBO GO now works with Chromecast -- the easiest way to enjoy online video on your TV. For when you want to settle into your Iron Throne to watch the latest episodes. For $35.\\nLearn how to use Chromecast with HBO GO and more at google.com/chromecast."),
        new Banner(3, "for_big_escape.png", "For Bigger Escape",
            "http://commondatastorage.googleapis.com/gtv-videos-bucket/sample/ForBiggerEscapes.mp4",
            "ntroducing Chromecast. The easiest way to enjoy online video and music on your TV—for when Batman's escapes aren't quite big enough. For $35. Learn how to use Chromecast with Google Play Movies and more at google.com/chromecast."),
        new Banner(4, "for_bigger_fun.png", "For Bigger Fun",
            "http://commondatastorage.googleapis.com/gtv-videos-bucket/sample/ForBiggerFun.mp4",
            "Introducing Chromecast. The easiest way to enjoy online video and music on your TV. For $35.  Find out more at google.com/chromecast."),
        new Banner(5, "for_bigger_fun.png", "For Bigger Fun",
            "http://commondatastorage.googleapis.com/gtv-videos-bucket/sample/ForBiggerFun.mp4",
            "Introducing Chromecast. The easiest way to enjoy online video and music on your TV. For $35.  Find out more at google.com/chromecast."),
    };

    private string name = "Комедія";

    public BookmarksDetailsPage()
    {
        InitializeComponent();

        CollectionNameLabel.Text = name;

        foreach (Banner banner in _banners)
        {
            MainContainer.Add(BookmarksFactory.CreateDetailsBanner(banner));
        }
    }

    private void PrivacyChanged(object sender, ToggledEventArgs e)
    {
        
        Switch @switch = (Switch)sender;
        
        if (@switch.IsToggled)
        {
            LockedImage.Source = "lock.png";
            return;
        }
        
        LockedImage.Source = "unlock.png";
    }

    private void LockOpenMenuClicked(object sender, EventArgs e)
    {
    if (ChangePrivacyMenu.Opacity == 0)
    {
        ChangePrivacyMenu.Opacity = 1;
        return;
    }
    
    ChangePrivacyMenu.Opacity = 0;
    }


    private void BookmarksDetailsPage_OnLoaded(object sender, EventArgs e)
    {
        PrivacySwitch.Toggled += PrivacyChanged;
    }
}