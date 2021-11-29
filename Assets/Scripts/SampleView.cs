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
        //���Ӧ��������
        ApplicationContext context = Context.GetApplicationContext();
        //�������ݰ󶨷���
        BindingServiceBundle bindingService = new BindingServiceBundle(context.GetContainer());
        bindingService.Start();
    }

    protected override void Start()
    {
        //��UI�ؼ�����ͼģ��
        BindingSet<SampleView, SampleViewModel> bindingSet;
        bindingSet = this.CreateBindingSet<SampleView, SampleViewModel>();

        //��VM�е�ֵ�󶨵�UI�ؼ��ϣ�ע�������ǵ����vm=>v
        bindingSet.Bind(this.input).For(v => v.text).To(vm => vm.SliderValue).OneWay();
        bindingSet.Bind(this.slider).For(v => v.value).To(vm => vm.SliderValue).OneWay();
        
        //Ȼ�󽫿ؼ����¼��󶨵�vm�ķ����У�ע��ToҪʹ�÷��ͣ������Ĳ������¼���������һ�¡�
        //��ȻҲ���԰󶨵������ϣ������ٽ��ܣ�
        bindingSet.Bind(this.slider).For(v => v.onValueChanged).To<float>(vm => vm.OnSliderValueChanged);
        bindingSet.Bind(this.input).For(v => v.onEndEdit).To<string>(vm=>vm.OnInputFieldChanged);

        //��һ��Ҳ��������
        bindingSet.Build();

        //������ݰ�������
        //IBindingContext bindingContext = this.BindingContext();
        //bindingContext.DataContext = new SampleViewModel { SliderValue = 98 };
        this.SetDataContext(new SampleViewModel { SliderValue = 98 });
    }
}
