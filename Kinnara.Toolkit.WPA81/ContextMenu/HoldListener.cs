using System;
using System.Diagnostics;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Input;

namespace Kinnara.Xaml.Controls
{
    internal sealed class HoldListener : IDisposable
    {
        private static readonly Size DeadZoneInPixels = new Size(9.5, 9.5);

        private DispatcherTimer _timer;
        private Point _gestureOrigin;
        private bool _holdingStarted;
        private bool _suppressTap;
        private bool _isDisposed;

        private PointerEventHandler _pointerPressedHandler;
        private PointerEventHandler _pointerMovedHandler;
        private PointerEventHandler _pointerReleasedHandler;
        private PointerEventHandler _pointerCanceledHandler;
        private PointerEventHandler _pointerCaptureLostHandler;

        public HoldListener(UIElement target)
        {
            _pointerPressedHandler = new PointerEventHandler(Target_PointerPressed);
            _pointerMovedHandler = new PointerEventHandler(Target_PointerMoved);
            _pointerReleasedHandler = new PointerEventHandler(Target_PointerReleased);
            _pointerCanceledHandler = new PointerEventHandler(Target_PointerCanceled);
            _pointerCaptureLostHandler = new PointerEventHandler(Target_PointerCaptureLost);

            Target = target;
            Target.AddHandler(UIElement.PointerPressedEvent, _pointerPressedHandler, true);
            Target.Tapped += Target_Tapped;

            _timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(0.7)
            };
            _timer.Tick += Timer_Tick;
        }

        public event EventHandler HoldingStarted;

        public event EventHandler HoldingEnded;

        public UIElement Target { get; private set; }

        public void Dispose()
        {
            if (_isDisposed)
            {
                return;
            }

            _timer.Stop();

            Target.RemoveHandler(UIElement.PointerPressedEvent, _pointerPressedHandler);
            Target.RemoveHandler(UIElement.PointerMovedEvent, _pointerMovedHandler);
            Target.RemoveHandler(UIElement.PointerReleasedEvent, _pointerReleasedHandler);
            Target.RemoveHandler(UIElement.PointerCanceledEvent, _pointerCanceledHandler);
            Target.RemoveHandler(UIElement.PointerCaptureLostEvent, _pointerCaptureLostHandler);
            Target.Tapped -= Target_Tapped;

            _isDisposed = true;
        }

        private void StartHolding()
        {
            _timer.Stop();

            Target.CancelDirectManipulations();

            _holdingStarted = true;
            _suppressTap = true;

            SafeRaise.Raise(HoldingStarted, this);
        }

        private void EndHolding(bool handled = false)
        {
            _timer.Stop();

            Target.RemoveHandler(UIElement.PointerMovedEvent, _pointerMovedHandler);
            Target.RemoveHandler(UIElement.PointerReleasedEvent, _pointerReleasedHandler);
            Target.RemoveHandler(UIElement.PointerCanceledEvent, _pointerCanceledHandler);
            Target.RemoveHandler(UIElement.PointerCaptureLostEvent, _pointerCaptureLostHandler);

            if (_holdingStarted)
            {
                _holdingStarted = false;
                if (!handled)
                {
                    SafeRaise.Raise(HoldingEnded, this);
                }
            }
        }

        private void Target_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            Debug.WriteLine("PointerPressed");

            _holdingStarted = false;
            _suppressTap = false;

            _gestureOrigin = e.GetCurrentPoint(null).Position;

            _timer.Start();

            Target.AddHandler(UIElement.PointerMovedEvent, _pointerMovedHandler, true);
            Target.AddHandler(UIElement.PointerReleasedEvent, _pointerReleasedHandler, true);
            Target.AddHandler(UIElement.PointerCanceledEvent, _pointerCanceledHandler, true);
            Target.AddHandler(UIElement.PointerCaptureLostEvent, _pointerCaptureLostHandler, true);

            Target.CapturePointer(e.Pointer);
        }

        private void Target_PointerMoved(object sender, PointerRoutedEventArgs e)
        {
            Debug.WriteLine("PointerMoved");

            Point position = e.GetCurrentPoint(null).Position;
            if (position.X - _gestureOrigin.X > DeadZoneInPixels.Width || position.Y - _gestureOrigin.Y > DeadZoneInPixels.Height)
            {
                EndHolding();
            }
        }

        private void Target_PointerReleased(object sender, PointerRoutedEventArgs e)
        {
            Debug.WriteLine("PointerReleased");

            EndHolding();
        }

        private void Target_PointerCanceled(object sender, PointerRoutedEventArgs e)
        {
            Debug.WriteLine("PointerCanceled");

            EndHolding();
        }

        private void Target_PointerCaptureLost(object sender, PointerRoutedEventArgs e)
        {
            Debug.WriteLine("PointerCaptureLost");

            EndHolding(_holdingStarted);
        }

        private void Target_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Debug.WriteLine("Tapped");

            if (_suppressTap)
            {
                _suppressTap = false;
                e.Handled = true;
            }
        }

        private void Timer_Tick(object sender, object e)
        {
            Debug.WriteLine("Timer_Tick");

            StartHolding();
        }
    }
}
