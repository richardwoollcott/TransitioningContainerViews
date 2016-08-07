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

        private TaskCompletionSource<bool> viewChangingTcs;

        public ContainerViewController(IntPtr handle) : base(handle)
        {
        }

        public TaskCompletionSource<bool> ViewChangingTcs
        {
            get { return viewChangingTcs; }
        }

        public Task<bool> PresentFirstViewAsync()
        {
            viewChangingTcs = new TaskCompletionSource<bool>();

            PerformSegue(SegueIdentifierFirst, this);

            return viewChangingTcs.Task;
        }

        public Task<bool> PresentSecondViewAsync()
        {
            viewChangingTcs = new TaskCompletionSource<bool>();

            PerformSegue(SegueIdentifierSecond, this);

            return viewChangingTcs.Task;
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

            viewChangingTcs.TrySetResult(true);
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

                            viewChangingTcs.TrySetResult(true);
                        });
        }
    }
}