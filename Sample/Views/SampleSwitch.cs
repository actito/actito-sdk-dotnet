namespace Sample.Views;

public class SampleSwitch : Switch
{
    private bool _isCodeToggled;

    public event EventHandler<ToggledEventArgs>? UserToggled;

    public static readonly BindableProperty IsCodeToggledProperty =
        BindableProperty.Create(
            nameof(IsCodeToggled),
            typeof(bool),
            typeof(SampleSwitch),
            defaultValue: false,
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: (bindable, oldValue, newValue) =>
                ((SampleSwitch)bindable).OnIsCodeToggledChanged((bool)newValue));

    public bool IsCodeToggled
    {
        get => (bool)GetValue(IsCodeToggledProperty);
        set => SetValue(IsCodeToggledProperty, value);
    }

    private void OnIsCodeToggledChanged(bool newValue)
    {
        if (newValue == IsToggled) return;

        _isCodeToggled = true;
        IsToggled = newValue;
    }

    protected override void OnPropertyChanged(string? propertyName = null)
    {
        base.OnPropertyChanged(propertyName);

        if (propertyName != nameof(IsToggled)) return;

        IsCodeToggled = IsToggled;

        if (!_isCodeToggled)
        {
            UserToggled?.Invoke(this, new ToggledEventArgs(IsToggled));

            return;
        }

        _isCodeToggled = false;
    }
}
