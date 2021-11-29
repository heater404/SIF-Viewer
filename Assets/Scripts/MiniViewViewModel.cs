using Loxodon.Framework.ViewModels;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniViewViewModel : ViewModelBase
{
    public MiniViewViewModel(string title, bool isOn)
    {
        this.Title = title;
        this.IsOn = isOn;
    }

    public MiniViewViewModel(string title)
    {
        this.Title = title;
    }
    private Texture texture;
    public Texture Texture
    {
        get { return texture; }
        private set { Set<Texture>(ref texture, value, "Texture"); }
    }

    private string title;
    public string Title
    {
        get { return title; }
        set 
        { 
            Set<string>(ref title, value, "Title");
            this.Texture = Resources.Load<RenderTexture>($"RenderTexture\\{this.title}");
        }
    }

    private bool isOn;
    public bool IsOn
    {
        get { return isOn; }
        set { Set<bool>(ref isOn, value, "IsOn"); }
    }
}
