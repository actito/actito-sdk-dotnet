using System.Windows.Input;
using CommunityToolkit.Maui.Views;
using Microsoft.Maui.Controls.Shapes;
using ActitoSdk.Inbox.Core.Models;
using Sample.ViewModels;
using LayoutAlignment = Microsoft.Maui.Primitives.LayoutAlignment;

namespace Sample.Pages.Inbox;

public partial class InboxPage : ContentPage
{
    public ICommand ItemTappedCommand { get; }
    
    public InboxPage()
    {
        InitializeComponent();
        
        ItemTappedCommand = new Command<ActitoInboxItem>(OnItemTapped);
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        
        var viewModel = (InboxViewModel)BindingContext;
        viewModel.Cleanup();
    }

    private void OnItemTapped(ActitoInboxItem item)
    {
        var viewModel = (InboxViewModel)BindingContext;
        var popup = new Popup();

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
                            popup.Close();
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
                            popup.Close();
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
                            popup.Close();
                        }),
                    },
                    new Button
                    {
                        Text = "Close",
                        TextColor = Colors.Black,
                        BackgroundColor = Colors.White,
                        Command = new Command(() => popup.Close()),
                    },
                }
            }
        };

        popup.HorizontalOptions = LayoutAlignment.Fill;
        popup.VerticalOptions = LayoutAlignment.End;
        popup.Color = Colors.Transparent;

        this.ShowPopup(popup);
    }
}
