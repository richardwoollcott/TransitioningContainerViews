using Foundation;
using System;
using UIKit;

namespace ContainerViews
{
    public partial class EmptySegue : UIStoryboardSegue
    {
        public EmptySegue (IntPtr handle) : base (handle)
        {
        }

        public override void Perform()
        {
            // Nothing. The ContainerViewController class handles all of the view
            // controller action.
        }
    }
}