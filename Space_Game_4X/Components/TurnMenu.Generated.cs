//Code for TurnMenu (Container)
using GumRuntime;
using MonoGameGum.GueDeriving;
using Gum.Converters;
using Gum.DataTypes;
using Gum.Managers;
using Gum.Wireframe;

using RenderingLibrary.Graphics;

using System.Linq;

namespace Space_Game_4X.Components;
partial class TurnMenu : MonoGameGum.Forms.Controls.FrameworkElement
{
    [System.Runtime.CompilerServices.ModuleInitializer]
    public static void RegisterRuntimeType()
    {
        var template = new MonoGameGum.Forms.VisualTemplate((vm, createForms) =>
        {
            var visual = new MonoGameGum.GueDeriving.ContainerRuntime();
            var element = ObjectFinder.Self.GetElementSave("TurnMenu");
            element.SetGraphicalUiElement(visual, RenderingLibrary.SystemManagers.Default);
            if(createForms) visual.FormsControlAsObject = new TurnMenu(visual);
            return visual;
        });
        MonoGameGum.Forms.Controls.FrameworkElement.DefaultFormsTemplates[typeof(TurnMenu)] = template;
        ElementSaveExtensions.RegisterGueInstantiation("TurnMenu", () => 
        {
            var gue = template.CreateContent(null, true) as InteractiveGue;
            return gue;
        });
    }
    public TextRuntime TextOfButton { get; protected set; }
    public SpriteRuntime End_Turn_Button { get; protected set; }
    public SpriteRuntime Turn_Counter { get; protected set; }
    public TextRuntime CounterNum { get; protected set; }

    public string CounterNumText
    {
        get => CounterNum.Text;
        set => CounterNum.Text = value;
    }

    public TurnMenu(InteractiveGue visual) : base(visual) { }
    public TurnMenu()
    {



    }
    protected override void ReactToVisualChanged()
    {
        base.ReactToVisualChanged();
        TextOfButton = this.Visual?.GetGraphicalUiElementByName("TextOfButton") as TextRuntime;
        End_Turn_Button = this.Visual?.GetGraphicalUiElementByName("End Turn Button") as SpriteRuntime;
        Turn_Counter = this.Visual?.GetGraphicalUiElementByName("Turn Counter") as SpriteRuntime;
        CounterNum = this.Visual?.GetGraphicalUiElementByName("CounterNum") as TextRuntime;
        CustomInitialize();
    }
    //Not assigning variables because Object Instantiation Type is set to By Name rather than Fully In Code
    partial void CustomInitialize();
}
