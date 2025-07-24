using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using ScanditDemoApp.Message;
using ScanditDemoApp.Service;

namespace ScanditDemoApp.ViewModel;

public abstract partial class BaseViewModel : ObservableObject
{
    public event EventHandler? Start;
    public event EventHandler? Sleep;
    public event EventHandler? Resume;

    public BaseViewModel(IAlertService alertService)
    {
        _alertService = alertService;
        this.RegisterMessages();
    }

    private string _title = string.Empty;
    public virtual string Title
    {
        get
        {
            return _title;
        }
        set
        {
            // TODO: add debug when pointed at dev environment
            SetProperty(ref _title, value);
        }
    }

    private readonly IAlertService _alertService;
    public IAlertService AlertService => _alertService;

    public virtual void OnStart()
    {
        this.Start?.Invoke(this, EventArgs.Empty);
    }

    public virtual void OnSleep()
    {
        this.Sleep?.Invoke(this, EventArgs.Empty);
    }

    public virtual void OnResume()
    {
        this.Resume?.Invoke(this, EventArgs.Empty);
    }

    private void RegisterMessages()
    {
        WeakReferenceMessenger.Default.Register<StartMessage>(this, async (r, m) =>
        {
            this.OnStart();
        });
        WeakReferenceMessenger.Default.Register<SleepMessage>(this, async (r, m) =>
        {
            this.OnSleep();
        });
        WeakReferenceMessenger.Default.Register<ResumeMessage>(this, async (r, m) =>
        {
            this.OnResume();
        });
    }
}