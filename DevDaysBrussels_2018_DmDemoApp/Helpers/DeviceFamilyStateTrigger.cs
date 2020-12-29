using DmDemoApp.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace DmDemoApp.Helpers
{
    public class DeviceFamilyStateTrigger : StateTriggerBase
    {
        private static DeviceFormFactorType deviceFamily;

        static DeviceFamilyStateTrigger()
        {
            deviceFamily = App.DeviceType;
        }

        public DeviceFormFactorType DeviceFamily
        {
            get { return (DeviceFormFactorType)GetValue(DeviceFamilyProperty); }
            set { SetValue(DeviceFamilyProperty, value); }
        }

        public static readonly DependencyProperty DeviceFamilyProperty =
            DependencyProperty.Register("DeviceFamily", typeof(DeviceFormFactorType), typeof(DeviceFamilyStateTrigger),
            new PropertyMetadata(DeviceFormFactorType.Other, OnDeviceTypePropertyChanged));

        private static void OnDeviceTypePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var obj = (DeviceFamilyStateTrigger)d;
            var val = (DeviceFormFactorType)e.NewValue;

            if (deviceFamily == val)
                obj.IsActive = true;
        }

        private bool m_IsActive;

        public bool IsActive
        {
            get { return m_IsActive; }
            private set
            {
                if (m_IsActive != value)
                {
                    m_IsActive = value;
                    base.SetActive(value);
                    if (IsActiveChanged != null)
                        IsActiveChanged(this, EventArgs.Empty);
                }
            }
        }

        /// <summary>
        /// Occurs when the <see cref="IsActive" /> property has changed.
        /// </summary>
        public event EventHandler IsActiveChanged;
    }

}
