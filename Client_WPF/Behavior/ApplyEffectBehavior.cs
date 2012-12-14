using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Interactivity;
using System.Windows;
using System.Windows.Media.Effects;
using System.Windows.Media;
using Client_WPF.View;

namespace Client_WPF.Behavior
{
    class ApplyEffectBehavior : Behavior<LoginModal>
    {

        public Effect EffectToApply
        {
            get { return (Effect)GetValue(EffectToApplyProperty); }
            set { SetValue(EffectToApplyProperty, value); }
        }

        // Using a DependencyProperty as the backing store for EffectToApply.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EffectToApplyProperty =
            DependencyProperty.Register("EffectToApply", typeof(Effect), typeof(ApplyEffectBehavior), null);

        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.Deactivated += new EventHandler(AssociatedObject_Deactivated);
            AssociatedObject.Initialized += new EventHandler(AssociatedObject_Initialized);
        }

        void AssociatedObject_Initialized(object sender, EventArgs e)
        {
            Application.Current.MainWindow.Effect = EffectToApply;
        }

        void AssociatedObject_Deactivated(object sender, EventArgs e)
        {
            Application.Current.MainWindow.Effect = null;
        }
    }
}
