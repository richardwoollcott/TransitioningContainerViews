// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace ContainerViews
{
    [Register ("ViewController")]
    partial class ViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIView MainContainer { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UISegmentedControl SegmentedView { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (MainContainer != null) {
                MainContainer.Dispose ();
                MainContainer = null;
            }

            if (SegmentedView != null) {
                SegmentedView.Dispose ();
                SegmentedView = null;
            }
        }
    }
}