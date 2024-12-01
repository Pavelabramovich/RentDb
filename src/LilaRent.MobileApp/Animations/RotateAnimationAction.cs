using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LilaRent.MobileApp.Animations;


public class RotateAnimationAction : TriggerAction<VisualElement>
{
    public double Rotation { get; init; }
    public uint Length { get; init; } = 250;
    public Easing Easing { get; init; } = Easing.CubicOut;

    protected override void Invoke(IView sender)
    {
        sender.RotateTo(Rotation, Length, Easing);
    }
}
