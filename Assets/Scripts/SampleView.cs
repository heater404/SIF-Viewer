using Loxodon.Framework.Binding;
using Loxodon.Framework.Binding.Builder;
using Loxodon.Framework.Binding.Contexts;
using Loxodon.Framework.Contexts;
using Loxodon.Framework.Views;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SampleView : UIView
{
    public InputField input;
    public Slider slider;

    protected override void Awake()
    {
        //获得应用上下文
        ApplicationContext context = Context.GetApplicationContext();
        //启动数据绑定服务
        BindingServiceBundle bindingService = new BindingServiceBundle(context.GetContainer());
        bindingService.Start();
    }

    protected override void Start()
    {
        //绑定UI控件到视图模型
        BindingSet<SampleView, SampleViewModel> bindingSet;
        bindingSet = this.CreateBindingSet<SampleView, SampleViewModel>();

        //将VM中的值绑定到UI控件上，注意这里是单向绑定vm=>v
        bindingSet.Bind(this.input).For(v => v.text).To(vm => vm.SliderValue).OneWay();
        bindingSet.Bind(this.slider).For(v => v.value).To(vm => vm.SliderValue).OneWay();
        
        //然后将控件的事件绑定到vm的方法中，注意To要使用泛型，方法的参数与事件方法参数一致。
        //当然也可以绑定到命令上（后文再介绍）
        bindingSet.Bind(this.slider).For(v => v.onValueChanged).To<float>(vm => vm.OnSliderValueChanged);
        bindingSet.Bind(this.input).For(v => v.onEndEdit).To<string>(vm=>vm.OnInputFieldChanged);

        //这一句也不能忘了
        bindingSet.Build();

        //获得数据绑定上下文
        //IBindingContext bindingContext = this.BindingContext();
        //bindingContext.DataContext = new SampleViewModel { SliderValue = 98 };
        this.SetDataContext(new SampleViewModel { SliderValue = 98 });
    }
}
