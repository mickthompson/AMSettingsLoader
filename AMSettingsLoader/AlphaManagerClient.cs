using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Automation;

namespace AMSettingsLoader
{
    internal class AlphaManagerClient
    {
        private AutomationElement _app;

        public int TotalForce
        {
            get => int.Parse(GetValuePattern().Current.Value);
            set => GetValuePattern().SetValue(value.ToString());
        }

        private Dictionary<string, ValuePattern> _patterns = new Dictionary<string, ValuePattern>();

        public AlphaManagerClient(bool launch = false)
        {
            var root = AutomationElement.RootElement;

            if (root != null)
            {
                var condition = new PropertyCondition(AutomationElement.NameProperty, "SIMAGIC");
                _app = root.FindFirst(TreeScope.Children, condition);
            }
        }

        public void SetValue(string name, int value)
        {
            var p = GetValuePattern(name);

            if(p != null)
            {
                p.SetValue(value.ToString());
            }
        }

        private ValuePattern GetValuePattern([CallerMemberName] string name = null)
        {
            if (name == null)
            {
                return null;
            }

            if (_patterns.TryGetValue(name, out ValuePattern valuePattern))
            {
                return valuePattern;
            }

            var label = _app.FindFirst(TreeScope.Descendants, new PropertyCondition(AutomationElement.NameProperty, name));

            if (label != null)
            {
                var slider = TreeWalker.ControlViewWalker.GetNextSibling(label);
                //Sometimes there's some text first
                if (slider.Current.ControlType != ControlType.Slider)
                {
                    slider = TreeWalker.ControlViewWalker.GetNextSibling(slider);
                }

                if (slider.Current.ControlType == ControlType.Slider)
                {
                    if (slider.TryGetCurrentPattern(ValuePattern.Pattern, out var o))
                    {
                        var p = o as ValuePattern;
                        _patterns.Add(name, p);
                        return p;
                    }
                }
            }

            return null;
        }
    }
}
