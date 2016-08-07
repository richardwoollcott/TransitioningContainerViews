using System;

using UIKit;

namespace ContainerViews
{
    public partial class ViewController : UIViewController
    {
        private ContainerViewController containerViewController;

        protected ViewController(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        public override void PrepareForSegue(UIStoryboardSegue segue, Foundation.NSObject sender)
        {
            if (segue.Identifier == "embedContainer")
            {
                containerViewController = segue.DestinationViewController as ContainerViewController;
            }
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            //select the First view
            SegmentedView.SelectedSegment = 0;
            PresentContainerView(SegmentedView.SelectedSegment);

            SegmentedView.ValueChanged += (sender, e) =>
            {
                var selectedSegmentId = (sender as UISegmentedControl).SelectedSegment;
                PresentContainerView(selectedSegmentId);
            };
        }

        async void PresentContainerView(nint selectedId)
        {
            //we need some synchronisation because the new view controller
            //is animated in. Disable the switch until the animation is complete
            if (selectedId == 0)
            {
                SegmentedView.Enabled = false;

                await containerViewController.PresentFirstViewAsync();

                SegmentedView.Enabled = true;
            }
            else if (selectedId == 1)
            {
                SegmentedView.Enabled = false;

                await containerViewController.PresentSecondViewAsync();

                SegmentedView.Enabled = true;
            }
        }

    }
}

