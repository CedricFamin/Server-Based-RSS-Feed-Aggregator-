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

        public bool ApplyEffect
        {
            get { return (bool)GetValue(ApplyEffectProperty); }
            set { SetValue(ApplyEffectProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ApplyEffect.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ApplyEffectProperty =
            DependencyProperty.Register("ApplyEffect", typeof(bool), typeof(ApplyEffectBehavior), new PropertyMetadata(false, new PropertyChangedCallback(OnApplyEffectChanged)));
        
        private static void OnApplyEffectChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != e.OldValue)
                (d as ApplyEffectBehavior).ApplyOrDeApplyEffect();
        }
        

        protected override void OnAttached()
        {
            base.OnAttached();
            //AssociatedObject.Deactivated += new EventHandler(AssociatedObject_Deactivated);
            AssociatedObject.Initialized += new EventHandler(AssociatedObject_Initialized);
        }

        void ApplyOrDeApplyEffect()
        {
            if (!ApplyEffect)
                Application.Current.MainWindow.Effect = EffectToApply;
            else
                Application.Current.MainWindow.Effect = null;
        }
        void AssociatedObject_Initialized(object sender, EventArgs e)
        {
            ApplyOrDeApplyEffect();
            //Application.Current.MainWindow.Effect = EffectToApply;
        }

        void AssociatedObject_Deactivated(object sender, EventArgs e)
        {
            Application.Current.MainWindow.Effect = null;
        }
    }
}
