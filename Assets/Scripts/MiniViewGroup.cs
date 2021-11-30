using Loxodon.Framework.Binding;
using Loxodon.Framework.Binding.Builder;
using Loxodon.Framework.Binding.Contexts;
using Loxodon.Framework.Contexts;
using Loxodon.Framework.Observables;
using Loxodon.Framework.Views;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;



[RequireComponent(typeof(ToggleGroup))]
public class MiniViewGroup : UIView
{
    public MiniView prefab;

    private ObservableList<MiniViewViewModel> miniViewViewModels = new ObservableList<MiniViewViewModel>();
    public ObservableList<MiniViewViewModel> MiniViewViewModels
    {
        get { return miniViewViewModels; }
        set
        {
            if (this.miniViewViewModels == value)
                return;

            if (this.miniViewViewModels != null)
                this.miniViewViewModels.CollectionChanged -= OnCollectionChanged;

            this.miniViewViewModels = value;
            this.OnItemsChanged();

            if (this.miniViewViewModels != null)
                this.miniViewViewModels.CollectionChanged += OnCollectionChanged;
        }
    }

    private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
    {
        if (NotifyCollectionChangedAction.Add == e.Action)
        {
            AddMiniView((MiniViewViewModel)e.NewItems[0]);
        }
    }

    private void AddMiniView(MiniViewViewModel model)
    {
        MiniView miniview = Instantiate(prefab, this.transform);
        miniview.GetComponent<Toggle>().group = this.GetComponent<ToggleGroup>();
        miniview.SetDataContext(model);
    }

    protected virtual void OnItemsChanged()
    {
        for (int i = 0; i < this.miniViewViewModels.Count; i++)
        {
            AddMiniView(MiniViewViewModels[i]);
        }
    }

    protected override void Awake()
    {
        //获得全局上下文
        ApplicationContext context = Context.GetApplicationContext();
        //初始化数据绑定服务
        BindingServiceBundle bindingService = new BindingServiceBundle(context.GetContainer());
        bindingService.Start();
    }

    protected override void Start()
    {
        MiniViewGroupViewModel viewModel = new MiniViewGroupViewModel();
        IBindingContext bindingContext = this.BindingContext();
        bindingContext.DataContext = viewModel;

        BindingSet<MiniViewGroup, MiniViewGroupViewModel> bindingSet = this.CreateBindingSet<MiniViewGroup, MiniViewGroupViewModel>();
        bindingSet.Bind().For(v => v.MiniViewViewModels).To(vm => vm.MiniViewViewModels).OneWay();

        bindingSet.Build();

        viewModel.AddMiniView(new MiniViewViewModel("Depth"));
        viewModel.AddMiniView(new MiniViewViewModel("Gray"));
        viewModel.AddMiniView(new MiniViewViewModel("Bg"));

        viewModel.Select("Bg");
    }
}

