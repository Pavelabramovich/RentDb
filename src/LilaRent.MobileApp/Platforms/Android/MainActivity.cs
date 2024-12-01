using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Views.InputMethods;
using Android.Views;
using Android.Widget;
using Color = Android.Graphics.Color;
using AndroidX.Core.View;
using System.Diagnostics;


namespace LilaRent.MobileApp;


[Activity(
    MainLauncher = true, 
    ConfigurationChanges = 
        ConfigChanges.ScreenSize  
        | ConfigChanges.UiMode 
        | ConfigChanges.ScreenLayout 
        | ConfigChanges.SmallestScreenSize 
        | ConfigChanges.Density, 
    ScreenOrientation = ScreenOrientation.Portrait)
]
public class MainActivity : MauiAppCompatActivity
{
    protected override void OnCreate(Bundle savedInstanceState)
    {
        base.OnCreate(savedInstanceState);

       // this.Window.SetFlags(WindowManagerFlags.LayoutNoLimits, WindowManagerFlags.LayoutNoLimits);
      //  this.Window.ClearFlags(WindowManagerFlags.LayoutNoLimits);
      
        
        //  this.Window.ClearFlags(WindowManagerFlags.TranslucentStatus);

        // only status bar transparent
        // var flags = SystemUiFlags.LayoutFullscreen | SystemUiFlags.LayoutStable;

      //  var flags = SystemUiFlags.LayoutHideNavigation | SystemUiFlags.LayoutStable;
      //  FindViewById(Android.Resource.Id.Content).SystemUiFlags |= flags;
    }


    // Unfocus entry on click to other part of the window.
    public override bool DispatchTouchEvent(MotionEvent? @event)
    {
        if (@event?.Action == MotionEventActions.Down)
        {
            var focusedElement = CurrentFocus;

            if (focusedElement is EditText editText)
            {
                var editTextLocation = new int[2];
                editText.GetLocationOnScreen(editTextLocation);

                var clearTextButtonWidth = 100;

                var editTextRect = new Rect(editTextLocation[0], editTextLocation[1], editText.Width + clearTextButtonWidth, editText.Height);

                var touchPosX = (int)@event.RawX;
                var touchPosY = (int)@event.RawY;

                if (!editTextRect.Contains(touchPosX, touchPosY))
                {
                    editText.ClearFocus();

                    if (GetSystemService(InputMethodService) is InputMethodManager inputMethodManager)
                        inputMethodManager.HideSoftInputFromWindow(editText.WindowToken, HideSoftInputFlags.None);
                }
            }
        }

        return base.DispatchTouchEvent(@event);
    }
}
