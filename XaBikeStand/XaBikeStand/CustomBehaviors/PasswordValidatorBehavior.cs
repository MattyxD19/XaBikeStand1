using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Xamarin.Forms;

namespace XaBikeStand.CustomBehaviors
{
    class PasswordValidatorBehavior : BehaviorBase<Entry>
    {

        const string passwordRegex = @"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$"
;

        public static readonly BindablePropertyKey IsValidPropertyKey = BindableProperty.CreateReadOnly("IsValid", typeof(bool), typeof(EmailValidatorBehavior), false);

        public static readonly BindableProperty IsValidProperty = IsValidPropertyKey.BindableProperty;

        public bool IsValid
        {
            get { return (bool)base.GetValue(IsValidProperty); }
            private set { base.SetValue(IsValidPropertyKey, value); }
        }

        protected override void OnAttachedTo(Entry bindable)
        {
            bindable.Unfocused += HandleUnFocused;
            bindable.Focused += HandleFocused;
        }

        void HandleUnFocused(object sender, EventArgs e)
        {
            Entry thisEntry = ((Entry)sender);
            IsValid = (Regex.IsMatch(thisEntry.Text, passwordRegex, RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250)));
            thisEntry.TextColor = IsValid ? Color.FromHex("66e6d9") : Color.Red;
        }

        void HandleFocused(object sender, EventArgs e)
        {
            ((Entry)sender).TextColor = Color.FromHex("66e6d9");
        }
        protected override void OnDetachingFrom(Entry bindable)
        {
            bindable.Unfocused -= HandleUnFocused;
            bindable.Focused -= HandleFocused;


        }
    }
}
