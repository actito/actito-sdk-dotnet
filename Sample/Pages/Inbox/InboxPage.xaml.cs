using System.Windows.Input;
using CommunityToolkit.Maui.Views;
using Microsoft.Maui.Controls.Shapes;
using ActitoSdk.Inbox.Core.Models;
using CommunityToolkit.Maui.Extensions;
using Sample.ViewModels;

namespace Sample.Pages.Inbox;

public partial class InboxPage : ContentPage
{
    public ICommand ItemTappedCommand { get; }

    public InboxPage()
    {
        InitializeComponent();

        ItemTappedCommand = new Command<ActitoInboxItem>(OnItemTapped);

        var viewModel = (InboxViewModel)BindingContext;
        Loaded += (_, _) => viewModel.SetupListeners();
        Unloaded += (_, _) => viewModel.CleanListeners();
    }

    private void OnItemTapped(ActitoInboxItem item)
    {
        var viewModel = (InboxViewModel)BindingContext;
        var popup = new Popup
        {
            WidthRequest = Width,
            Margin = 0,
            Padding = 0,
            HorizontalOptions = LayoutOptions.Fill,
            VerticalOptions = LayoutOptions.End,
            BackgroundColor = Colors.Transparent
        };

        popup.Content = new Border
        {
            StrokeShape = new RoundRectangle { CornerRadius = new CornerRadius(20, 20, 0, 0) },
            BackgroundColor = Colors.White,
            HorizontalOptions = LayoutOptions.Fill,
            Content = new VerticalStackLayout
            {
                Children =
                {
                    new Button
                    {
                        Text = "Open",
                        TextColor = Colors.Black,
                        BackgroundColor = Colors.White,
                        Command = new Command(() =>
                        {
                            viewModel.Open(item);
                            popup.CloseAsync();
                        }),
                    },
                    new Button
                    {
                        Text = "Mark As Read",
                        TextColor = Colors.Black,
                        BackgroundColor = Colors.White,
                        Command = new Command(() =>
                        {
                            viewModel.MarkAsRead(item);
                            popup.CloseAsync();
                        }),
                    },
                    new Button
                    {
                        Text = "Remove",
                        TextColor = Colors.Black,
                        BackgroundColor = Colors.White,
                        Command = new Command( () =>
                        {
                            viewModel.Remove(item);
                            popup.CloseAsync();
                        }),
                    },
                    new Button
                    {
                        Text = "Close",
                        TextColor = Colors.Black,
                        BackgroundColor = Colors.White,
                        Command = new Command(() => popup.CloseAsync())
                    },
                }
            }
        };

        this.ShowPopupAsync(popup);
    }
}
