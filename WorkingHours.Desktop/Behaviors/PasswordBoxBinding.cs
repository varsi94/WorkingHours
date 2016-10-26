using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace WorkingHours.Desktop.Behaviors
{
    public static class PasswordBoxBinding
    {
        private static bool IsChangedFromCode = false;
        public static string GetBoundPassword(DependencyObject obj)
        {
            return (string)obj.GetValue(BoundPasswordProperty);
        }

        public static void SetBoundPassword(DependencyObject obj, string value)
        {
            IsChangedFromCode = true;
            obj.SetValue(BoundPasswordProperty, value);
            IsChangedFromCode = false;
        }

        // Using a DependencyProperty as the backing store for BoundPassword.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BoundPasswordProperty =
            DependencyProperty.RegisterAttached("BoundPassword", typeof(string), typeof(PasswordBoxBinding), new FrameworkPropertyMetadata(BoundPasswordChanged));

        private static void BoundPasswordChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (!IsChangedFromCode)
            {
                ((PasswordBox) d).Password = (string)e.NewValue;
            }
        }

        public static bool GetIsPasswordBound(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsPasswordBoundProperty);
        }

        public static void SetIsPasswordBound(DependencyObject obj, bool value)
        {
            obj.SetValue(IsPasswordBoundProperty, value);
        }

        // Using a DependencyProperty as the backing store for IsPasswordBound.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsPasswordBoundProperty =
            DependencyProperty.RegisterAttached("IsPasswordBound", typeof(bool), typeof(PasswordBoxBinding), new PropertyMetadata(false, IsBoundChanged));

        private static void IsBoundChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var pwdBox = (PasswordBox)d;
            if ((bool)e.OldValue == true)
            {
                pwdBox.PasswordChanged -= PasswordChanged;
            }

            if ((bool) e.NewValue == true)
            {
                pwdBox.PasswordChanged += PasswordChanged;
            }
        }

        private static void PasswordChanged(object sender, RoutedEventArgs e)
        {
            var pwdBox = (PasswordBox)sender;
            SetBoundPassword(pwdBox, pwdBox.Password);
        }
    }
}
