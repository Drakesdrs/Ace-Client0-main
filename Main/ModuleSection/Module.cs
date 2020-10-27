 using Ace_client.Main.CategorySection;
using Ace_client.Main.ImportantSection;
using Ace_client.Main.ModuleSection.ModuleSettings;
using System;
using System.Drawing;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Timers;
using System.Threading.Tasks;

using Ace_client.Main.KeySection;
using Ace_client.Main.UI;
using System.Windows.Forms;

namespace Ace_client.Main.ModuleSection
{

    public abstract class Module : IKeyInputHandler
    {
        public static implicit operator bool(Module module) => module.enabled;

        public void draw(int x, int y, int width, Graphics graphics)
        {
            if(ModuleMgr.registry.selectedModule == this)
                graphics.FillRectangle(OverlayMgr.selectedbackground, x, y, width + 6, 34);
            else
                graphics.FillRectangle(OverlayMgr.background, x, y, width + 6, 34);

            if (this.isEnabled)
                graphics.DrawString(this.name, OverlayMgr.font, Brushes.White, x, y);
            else
                graphics.DrawString(this.name, OverlayMgr.font, Brushes.Gray, x, y);
        }
        private bool isEnabled;
        public bool enabled
        {
            get => isEnabled;
            set 
            {
                if(value == this.isEnabled) return;

                if(value == true)
                {
                    foreach (Module mod in ModuleMgr.registry.activeModules)
                        if (mod.nameWidth < this.nameWidth)
                        {
                            ModuleMgr.registry.activeModules.Insert(ModuleMgr.registry.activeModules.IndexOf(mod), this);
                            goto label;
                        }
                    ModuleMgr.registry.activeModules.Add(this);
                    label:
                    this.onEnable();
                }
                else
                {
                    ModuleMgr.registry.activeModules.Remove(this);
                    this.onDisable();
                }
                SaveFile.SFile.SaveConfig();
                isEnabled = value;
            }
        }
        public void toggle()
        {
            this.enabled = !this.isEnabled;
            Logger.debug(this.name + " is now " + (this.isEnabled ? "enabled" : "disabled") + ".");
        }
        public bool selected;
        public int nameWidth;
        public string name { get => this.GetType().Name; }

        public Module(Keys keybind, string cat)
        {
            this.key = keybind;

            if (keybind != Keys.None)
                KeyInputMgr.keybinds[keybind] = this;

            this.nameWidth = TextRenderer.MeasureText(this.name, OverlayMgr.font).Width;

            CategoryHandler.registry.categories.Find(e => e.name.Contains(cat)).modules.Add(this);

            registerKeybind();
        }

        public IKeyInputHandler keybind;

        public virtual void registerKeybind()
        {

        }

        public Module(string cat)
        {
            this.key = Keys.None;

            this.nameWidth = TextRenderer.MeasureText(this.name, OverlayMgr.font).Width;

            CategoryHandler.registry.categories.Find(e => e.name.Contains(cat)).modules.Add(this);

            registerKeybind();
        }

        public virtual void onEnable () { }
        public virtual void onDisable() { }

        public override void onKeyDown()
        {
            this.toggle();
        }

        public override void onKeyUp()
        {
            
        }

        public List<ToggleSetting> toggleSettings = new List<ToggleSetting>();
        public List<SliderSetting> sliderSettings = new List<SliderSetting>();

        public void RegToggleSettings(string text, bool value) //Same
        {
            toggleSettings.Add(new ToggleSetting(text, value));
        }

        public unsafe void RegisterIntSliderSetting(string text, int min, int* value, int max)
        {
            int minc = min;
            int maxc = max;
            sliderSettings.Add(new SliderSetting(text, new SettingsValue(&minc), new SettingsValue(value), new SettingsValue(&maxc)));
        }

        public unsafe void RegisterFloatSliderSetting(string text, float min, float* value, float max)
        {
            float minc = min;
            float maxc = max;
            sliderSettings.Add(new SliderSetting(text, new SettingsValue(&minc), new SettingsValue(value), new SettingsValue(&maxc)));
        }
    }
    public abstract class TickingModule : Module
    {
        public abstract void onTick();

        public TickingModule(Keys keybind, string cat)
            : base(keybind, cat)
        {
            CategoryHandler.registry.categories.Find(e => e.name.Contains(cat)).tickingModules.Add(this);
        }

        public TickingModule(string cat)
            : base(cat)
        {
            CategoryHandler.registry.categories.Find(e => e.name.Contains(cat)).tickingModules.Add(this);
        }
    }

    public abstract class SparinglyTickingModule : Module
    {
        public abstract void onSpareTick();

        public SparinglyTickingModule(Keys keybind, string cat)
            : base(keybind, cat)
        {
            CategoryHandler.registry.categories.Find(e => e.name.Contains(cat)).sparinglyTickingModules.Add(this);
        }

        public SparinglyTickingModule(string cat)
            : base(cat)
        {
            CategoryHandler.registry.categories.Find(e => e.name.Contains(cat)).sparinglyTickingModules.Add(this);
        }
    }
}
