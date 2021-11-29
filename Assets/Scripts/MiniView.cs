using Loxodon.Framework.Binding;
using Loxodon.Framework.Binding.Builder;
using Loxodon.Framework.Binding.Contexts;
using Loxodon.Framework.Contexts;
using Loxodon.Framework.Views;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Toggle))]
public class MiniView : UIView
{
    public RawImage image;
    public Text title;
    public Toggle toggle;
    protected override void Start()
    {
        //绑定UI控件到视图模型
        BindingSet<MiniView, MiniViewViewModel> bindingSet;
        bindingSet = this.CreateBindingSet<MiniView, MiniViewViewModel>();

        bindingSet.Bind(this.image).For(v => v.texture).To(vm => vm.Texture).OneTime();
        bindingSet.Bind(this.title).For(v => v.text).To(vm => vm.Title).OneTime();
        bindingSet.Bind(this.toggle).For(v => v.isOn).To(vm => vm.IsOn).TwoWay();

        bindingSet.Build();
    }
}
