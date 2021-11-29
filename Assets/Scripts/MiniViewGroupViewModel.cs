using Loxodon.Framework.Observables;
using Loxodon.Framework.ViewModels;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniViewGroupViewModel : ViewModelBase
{
    private ObservableList<MiniViewViewModel> miniViewViewModels = new ObservableList<MiniViewViewModel>();
    public ObservableList<MiniViewViewModel> MiniViewViewModels
    {
        get { return miniViewViewModels; }
    }

    public void AddMiniView(MiniViewViewModel model)
    {
        this.miniViewViewModels.Add(model);
    }

    public void Select(int index)
    {
        if (index < miniViewViewModels.Count)
        {
            miniViewViewModels[index].IsOn = true;
        }
    }

    public void Select(string title)
    {
        foreach (var item in miniViewViewModels)
        {
            if (title == item.Title)
                item.IsOn = true;
        }
    }
}
