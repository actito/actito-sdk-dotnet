using System.Windows.Input;
using ActitoSdk.UserInbox.Core.Models;
using CommunityToolkit.Maui.Views;
using Microsoft.Maui.Controls.Shapes;
using Sample.UserInbox.ViewModels;
using LayoutAlignment = Microsoft.Maui.Primitives.LayoutAlignment;

namespace Sample.UserInbox.Pages.Inbox;

[QueryProperty(nameof(Token), "token")]
public partial class InboxPage : ContentPage
{
    public InboxPage(InboxViewModel viewModel)
    {
        InitializeComponent();

        BindingContext = viewModel;
        ItemTappedCommand = new Command<ActitoUserInboxItem>(OnItemTapped);
    }

    public required string Token { get; set; }
    public ICommand ItemTappedCommand { get; }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        var viewModel = (InboxViewModel)BindingContext;
        viewModel.AccessToken = Token;
        viewModel.SetupListeners();
        viewModel.RefreshCommand.Execute(null);
    }

    protected override void OnDisappearing()
    {
        var viewModel = (InboxViewModel)BindingContext;
        viewModel.CleanListeners();
    }

    private void OnItemTapped(ActitoUserInboxItem item)
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
                        })
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
                        })
                    },
                    new Button
                    {
                        Text = "Remove",
                        TextColor = Colors.Black,
                        BackgroundColor = Colors.White,
                        Command = new Command(() =>
                        {
                            viewModel.Remove(item);
                            popup.Close();
                        })
                    },
                    new Button
                    {
                        Text = "Close",
                        TextColor = Colors.Black,
                        BackgroundColor = Colors.White,
                        Command = new Command(() => popup.Close())
                    }
                }
            }
        };

        popup.HorizontalOptions = LayoutAlignment.Fill;
        popup.VerticalOptions = LayoutAlignment.End;
        popup.Color = Colors.Transparent;

        this.ShowPopup(popup);
    }
}
