using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using ScoopManager;
using ScoopManager.Views;
using DevTools.Modules.Scoop;

namespace ScoopManager;

public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);
        var names = new[] { "AppBackground", "SidebarBackground", "PanelBackground", "TextBrush", "MutedBrush", "LineBrush", "AccentBrush", "OnAccentBrush", "SelectionBrush", "SuccessBrush" };
        var colors = new[] { "#15171C", "#111318", "#1D2028", "#EDEEF5", "#A1A7B8", "#30343E", "#AAA1FF", "#191529", "#2D2845", "#97D9BD" };
        for (var i = 0; i < names.Length; i++) Resources[names[i]] = new SolidColorBrush((Color)ColorConverter.ConvertFromString(colors[i]));
        AppModuleContext context;
        try { context = new AppModuleContext(); }
        catch (Exception error) { NoticeWindow.Show("Data folder unavailable", error.Message); Shutdown(1); return; }
        var page = new ScoopModule().CreatePage(context);
        var grid = new Grid { Margin = new Thickness(24) };
        grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
        grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
        var view = new ScoopWorkspaceView { DataContext = page };
        grid.Children.Add(view);
        var activity = new TextBox { IsReadOnly = true, Height = 90, Margin = new Thickness(0, 12, 0, 0), TextWrapping = TextWrapping.Wrap, VerticalScrollBarVisibility = ScrollBarVisibility.Auto };
        var cancel = new Button { Content = "Cancel operation", HorizontalAlignment = HorizontalAlignment.Right, Margin = new Thickness(0, 8, 0, 0), IsEnabled = false };
        cancel.Click += (_, _) => context.Cancel();
        var footer = new StackPanel(); footer.Children.Add(activity); footer.Children.Add(cancel); Grid.SetRow(footer, 1); grid.Children.Add(footer);
        context.Logged += (_, line) => Dispatcher.InvokeAsync(() => { activity.AppendText(line + Environment.NewLine); activity.ScrollToEnd(); });
        context.OperationChanged += () => Dispatcher.InvokeAsync(() => cancel.IsEnabled = context.IsOperationRunning);
        var window = new ThemedWindow { Title = "ScoopNest", WindowStartupLocation = WindowStartupLocation.CenterScreen, Width = 1200, Height = 820, MinWidth = 900, MinHeight = 650, Content = grid, Background = (Brush)Resources["AppBackground"], Foreground = (Brush)Resources["TextBrush"] };
        window.Closing += (_, args) => { if (context.IsOperationRunning) { NoticeWindow.Show("Operation running", "Cancel the operation and wait for it to finish before closing."); args.Cancel = true; } };
        MainWindow = window;
        window.Show();
    }
}

