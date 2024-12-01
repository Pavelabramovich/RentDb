using Microsoft.Maui.Handlers;
using System.Reflection;
using Microsoft.Maui.Controls.Handlers.Items;
using Microsoft.Maui.Controls.PlatformConfiguration;
using LilaRent.MobileApp.ViewModels;
using Microsoft.Maui.Platform;
using System.Globalization;


namespace LilaRent.MobileApp;


public static class SetupHandlersExtension
{
    public static MauiAppBuilder SetupHandlers(this MauiAppBuilder builder)
    {
        EntryHandler.Mapper.AppendToMapping("NoUnderline", (handler, view) =>
        {
            #if ANDROID
            {
                var nativeEntry = handler.PlatformView;
                nativeEntry.SetBackgroundColor(Android.Graphics.Color.Transparent);

                //nativeEntry.SetPadding(0, 0, 0, 0);
                //nativeEntry.Gravity = Android.Views.GravityFlags.CenterVertical;
            }
            #elif IOS || MACCATALYST
            {
                handler.PlatformView.BorderStyle = UIKit.UITextBorderStyle.None;
            }
            #elif WINDOWS
            {
                handler.PlatformView.BorderThickness = new Microsoft.UI.Xaml.Thickness()
                {
                    Bottom = 0,
                    Top = 0,
                    Left = 0,
                    Right = 0,
                };
            }
            #endif
        });

        EditorHandler.Mapper.AppendToMapping("NoUnderline", (handler, view) =>
        { 
            #if ANDROID
            {
                handler.PlatformView.SetBackgroundColor(Android.Graphics.Color.Transparent);
            }
            #elif IOS || MACCATALYST
            {
                handler.PlatformView.BorderStyle = UIKit.UITextViewBorderStyle.None;
            }
            #elif WINDOWS
            {
                handler.PlatformView.BorderThickness = new Microsoft.UI.Xaml.Thickness()
                {
                    Bottom = 0,
                    Top = 0,
                    Left = 0,
                    Right = 0,
                };
            }
            #endif
        });

        SearchBarHandler.Mapper.AppendToMapping("NoUnderline", (handler, view) =>
        {
            #if ANDROID
            {
                Android.Widget.LinearLayout linearLayout =  handler.PlatformView.GetChildAt(0) as Android.Widget.LinearLayout;  
                linearLayout = linearLayout.GetChildAt(2) as Android.Widget.LinearLayout;  
                linearLayout = linearLayout.GetChildAt(1) as Android.Widget.LinearLayout;  
                linearLayout.Background = null;  
            }
            #endif
        });

        TimePickerHandler.Mapper.AppendToMapping("NoUnderline", (handler, view) =>
        {
            #if ANDROID
            {
                handler.PlatformView.SetBackgroundColor(Android.Graphics.Color.Transparent);
            }
            #elif WINDOWS
            {
                handler.PlatformView.BorderThickness = new Microsoft.UI.Xaml.Thickness()
                {
                    Bottom = 0,
                    Top = 0,
                    Left = 0,
                    Right = 0,
                };
            }
            #endif
        });

        DatePickerHandler.Mapper.AppendToMapping("NoUnderline", (handler, view) =>
        { 
            #if ANDROID
            {
                handler.PlatformView.SetBackgroundColor(Android.Graphics.Color.Transparent);
            }
            #elif WINDOWS
            {
                handler.PlatformView.BorderThickness = new Microsoft.UI.Xaml.Thickness()
                {
                    Bottom = 0,
                    Top = 0,
                    Left = 0,
                    Right = 0,
                };
            }
            #endif
        });

        PickerHandler.Mapper.AppendToMapping("NoUnderline", (handler, view) =>
        {
            #if ANDROID
            {
                handler.PlatformView.SetBackgroundColor(Android.Graphics.Color.Transparent);
            }
            #elif WINDOWS
            {
                handler.PlatformView.BorderThickness = new Microsoft.UI.Xaml.Thickness()
                {
                    Bottom = 0,
                    Top = 0,
                    Left = 0,
                    Right = 0,
                };
            }
            #endif
        });


        EntryHandler.Mapper.AppendToMapping("BlackCursor", (handler, view) =>
        {
            #if ANDROID
            {
				// doesn't work on android api 26
				try
				{
					handler.PlatformView.TextCursorDrawable.SetTint(Android.Graphics.Color.Black);
				}
				catch (Exception) 
                { }
            }
            #elif IOS || MACCATALYST
            {
                handler.PlatformView.TintColor = UIKit.UIColor.Black;
            }
            #elif WINDOWS
            { 
                // There is no easy way to change only cursor color.
            }
            #endif
        });


        CheckBoxHandler.Mapper.AppendToMapping("NoExtraMargin", (handler, view) =>
        {
            #if ANDROID
            {
                var appCompatCheckBox = handler.PlatformView;

                // Somehow remove margin...
            }
            #endif
        });

        
        EntryHandler.Mapper.AppendToMapping<Entry, EntryHandler>("CanSetDotOnNumericEntry", (handler, view) =>
        {
            #if ANDROID
            {
                if (DeviceInfo.Manufacturer.Equals("Samsung", StringComparison.OrdinalIgnoreCase))
                {
                    var context = Android.App.Application.Context;
                    var appCompatEditText = handler.PlatformView;

                    UpdateEditText(appCompatEditText);
                    view.PropertyChanged += (sender, args) => UpdateEditText(appCompatEditText);

                    void UpdateEditText(AndroidX.AppCompat.Widget.AppCompatEditText appCompatEditText)
                    {
                        if (appCompatEditText.KeyListener is not LocalizedDigitsKeyListener and not Android.Text.Method.DigitsKeyListener)
                            return;

                        var currentInputMethod = Android.Provider.Settings.Secure.GetString(
                            context.ContentResolver,
                            Android.Provider.Settings.Secure.DefaultInputMethod
                        );

                        if (currentInputMethod is not null && currentInputMethod.Contains("SamsungKeypad"))
                        {
                            if (appCompatEditText.KeyListener is LocalizedDigitsKeyListener)
                            {
                                appCompatEditText.KeyListener = Android.Text.Method.DigitsKeyListener.GetInstance("0123456789-.");

                                appCompatEditText.InputType =
                                    Android.Text.InputTypes.ClassNumber
                                    | Android.Text.InputTypes.NumberFlagSigned
                                    | Android.Text.InputTypes.NumberFlagDecimal; 
                            }
                        }                        
                    }
                }
            }
            #endif
        });


        TabbedViewHandler.Mapper.AppendToMapping("CustomHeightAndAlignment", (handler, view) =>
        {
            int iconWidth = 80;
            int tabHeight = 200;

            #if ANDROID
            {               
                var virtualView = ((IPlatformViewHandler)handler).VirtualView;

                var tabbedPageManager = typeof(TabbedPage)
                    .GetField("_tabbedPageManager", BindingFlags.NonPublic | BindingFlags.Instance)?
                    .GetValue(virtualView);

                var bottomNavigationView = (Google.Android.Material.BottomNavigation.BottomNavigationView)tabbedPageManager.GetType()
                    .GetProperty("BottomNavigationView", BindingFlags.NonPublic | BindingFlags.Instance)
                    ?.GetValue(tabbedPageManager);

                // Move to separate class with separate handler in future...
                bottomNavigationView.ItemReselected += (sender, args) =>
                {
                    ((NavigationViewModel)((TabbedViewModel)((NavigationViewModel)((App)App.Current).MainViewModel).NavigationStack.Current).CurrentTab).NavigationStack.PopToRoot();
                };


                bottomNavigationView.LayoutParameters = new Android.Widget.FrameLayout.LayoutParams(
                    Android.Widget.RelativeLayout.LayoutParams.MatchParent,     
                    tabHeight
                );
     

                bottomNavigationView.SetOnApplyWindowInsetsListener(new RefreshNavbarListner());


                var menuView = (Google.Android.Material.BottomNavigation.BottomNavigationMenuView)bottomNavigationView.GetChildAt(0);

                for (int i = 0; i < menuView.ChildCount; i++)
                {
                    var menuItemView = (Google.Android.Material.BottomNavigation.BottomNavigationItemView)menuView.GetChildAt(i);

                    var frame = (Android.Widget.FrameLayout)menuItemView.GetChildAt(0);
               
                    var frameLayoutParams = new Android.Widget.FrameLayout.LayoutParams(
                        Android.Views.ViewGroup.LayoutParams.MatchParent,
                        Android.Views.ViewGroup.LayoutParams.MatchParent
                    );

                    frame.LayoutParameters = frameLayoutParams;  

                    var icon = (AndroidX.AppCompat.Widget.AppCompatImageView)frame.GetChildAt(1);

                    var titleLayout = (Google.Android.Material.Internal.BaselineLayout)menuItemView.GetChildAt(1);
                    var title = (Google.Android.Material.TextView.MaterialTextView)titleLayout.GetChildAt(0);
                        

                    title.Visibility = Android.Views.ViewStates.Gone;
                        
                    var iconLayoutParams = new Android.Widget.FrameLayout.LayoutParams(
                        iconWidth,
                        Android.Views.ViewGroup.LayoutParams.WrapContent,
                        Android.Views.GravityFlags.Center
                    );

                    icon.LayoutParameters = iconLayoutParams;   
                }

                if (tabbedPageManager is not null)
                {
                    var setContentBottomMarginMethod = tabbedPageManager
                        .GetType()
                        .GetMethod("SetContentBottomMargin", BindingFlags.NonPublic | BindingFlags.Instance);

                    setContentBottomMarginMethod.Invoke(tabbedPageManager, [tabHeight]);
                }
            }
            #elif IOS
            {
     
            
            }
            #endif
        });


        CollectionViewHandler.Mapper.AppendToMapping("NoScrollAnimation", (handler, View) => 
        {
            #if ANDROID
            {
                handler.PlatformView.OverScrollMode = Android.Views.OverScrollMode.Never; 

               
            }
            #endif
        });


        return builder;
    }


#if ANDROID

    // Listner to refresh tabbar if statusbar was changed
    private class RefreshNavbarListner : AndroidX.ViewPager2.Widget.ViewPager2.OnPageChangeCallback, Android.Views.View.IOnApplyWindowInsetsListener
    {
        public Android.Views.WindowInsets OnApplyWindowInsets(Android.Views.View view, Android.Views.WindowInsets insets)
        {
            var customInsets = insets.ReplaceSystemWindowInsets(
                insets.SystemWindowInsetLeft,
                insets.SystemWindowInsetTop,
                insets.SystemWindowInsetRight,
                bottom: 0
            );

            return view.OnApplyWindowInsets(customInsets);
        }
    }

    //public class LocalizedTextWatcher : Java.Lang.Object, Android.Text.ITextWatcher
    //{
    //    private readonly Android.Widget.EditText _editText;
    //    private readonly CultureInfo _culture;

    //    public LocalizedTextWatcher(Android.Widget.EditText editText, CultureInfo culture)
    //    {
    //        _editText = editText;
    //        _culture = culture;
    //    }

    //    public void AfterTextChanged(Android.Text.IEditable? s)
    //    {
    //        if (s == null) return;

    //        var currentText = s.ToString();

    //        // Убираем все некорректные символы и форматируем
    //        if (double.TryParse(currentText, NumberStyles.Any, _culture, out var result) 
    //            || double.TryParse(currentText, NumberStyles.Any, CultureInfo.InvariantCulture, out result))
    //        {
    //            var formatted = result.ToString(_culture) + "0123";

    //            if (formatted != currentText)
    //            {
    //                _editText.Text = formatted;
    //                _editText.SetSelection(Math.Min(formatted.Length, _editText.SelectionStart)); // Сохраняем позицию курсора
    //            }
    //        }
    //    }

    //    public void BeforeTextChanged(Java.Lang.ICharSequence? s, int start, int count, int after) { }
    //    public void OnTextChanged(Java.Lang.ICharSequence? s, int start, int before, int count) { }
    //}

#endif
}
