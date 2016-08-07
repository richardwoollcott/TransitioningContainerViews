using Foundation;
using System;
using UIKit;
using System.Threading.Tasks;

namespace ContainerViews
{
    public partial class ContainerViewController : UIViewController
    {
        private NSString SegueIdentifierFirst = (NSString)"embedFirst";
        private NSString SegueIdentifierSecond = (NSString)"embedSecond";

        private TaskCompletionSource<bool> viewChanging;

        public ContainerViewController(IntPtr handle) : base(handle)
        {
        }

        public TaskCompletionSource<bool> ViewChanging
        {
            get { return viewChanging; }
        }

        public Task<bool> PresentFirstViewAsync()
        {
            viewChanging = new TaskCompletionSource<bool>();

            PerformSegue(SegueIdentifierFirst, this);

            return viewChanging.Task;
        }

        public Task<bool> PresentSecondViewAsync()
        {
            viewChanging = new TaskCompletionSource<bool>();

            PerformSegue(SegueIdentifierSecond, this);

            return viewChanging.Task;
        }

        public override void PrepareForSegue(UIStoryboardSegue segue,
                                             NSObject sender)
        {
            if ((segue.Identifier == SegueIdentifierFirst) ||
                (segue.Identifier == SegueIdentifierSecond))
            {
                if (ChildViewControllers.Length > 0)
                {
                    SwapFromViewController(ChildViewControllers[0], segue.DestinationViewController);
                }
                else
                {
                    AddInitialViewController(segue.DestinationViewController);
                }
            }
        }

        private void AddInitialViewController(UIViewController viewController)
        {
            //on first run no transition animation
            AddChildViewController(viewController);

            viewController.View.Frame = View.Bounds;

            Add(viewController.View);

            viewController.DidMoveToParentViewController(this);

            viewChanging.TrySetResult(true);
        }

        private void SwapFromViewController(UIViewController fromViewController,
                                            UIViewController toViewController)
        {
            fromViewController.WillMoveToParentViewController(null);

            toViewController.View.Frame = this.View.Bounds;

            AddChildViewController(toViewController);

            Transition(fromViewController,
                       toViewController,
                       0.3,
                       UIViewAnimationOptions.TransitionCrossDissolve,
                       () => { },
                       (bool finished) =>
                        {
                            fromViewController.RemoveFromParentViewController();
                            toViewController.DidMoveToParentViewController(this);

                            viewChanging.TrySetResult(true);
                        });
        }
    }
}