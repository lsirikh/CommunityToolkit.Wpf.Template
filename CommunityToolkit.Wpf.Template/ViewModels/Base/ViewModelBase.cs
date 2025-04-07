using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using Autofac;
using System;

namespace CommunityToolkit.Wpf.Template.ViewModels.Base;
/****************************************************************************
   Purpose      : 
       모든 ViewModel의 기반이 되는 추상 클래스입니다.
       CommunityToolkit.MVVM의 ObservableObject를 상속하며,
       Caliburn.Micro의 Screen과 유사하게 Activate, DeactivateAsync,
       Dispose 등의 수명주기 관리 기능을 제공합니다.

       Messenger(IMessenger) 및 CancellationTokenSource를 기본으로 포함하여,
       메시징 기반 구조와 비동기 취소 제어가 가능하도록 설계되어 있습니다.

   Created By   : $username$                                                 
   Created On   : $time$                                                   
   Department   : $department$                                                   
   Company      : $company$                                       
   Email        : $email$                                            
****************************************************************************/

public abstract partial class ViewModelBase : ObservableObject
{
    /// <summary>
    /// CommunityToolkit의 메시징 시스템 사용을 위한 Messenger 인스턴스
    /// </summary>
    [ObservableProperty]
    protected IMessenger? _messenger;

    /// <summary>
    /// 비동기 취소 토큰 소스 (예: RelayCommand 취소 처리용)
    /// </summary>
    [ObservableProperty]
    protected CancellationTokenSource _cancellationTokenSource;

    /// <summary>
    /// 기본 생성자 - App의 DI 컨테이너에서 Messenger를 Resolve합니다.
    /// </summary>
    protected ViewModelBase()
    {
        Messenger = App.Container?.Resolve<IMessenger>();
        CancellationTokenSource = new CancellationTokenSource();
    }

    /// <summary>
    /// DI를 통해 Messenger를 주입받는 생성자
    /// </summary>
    protected ViewModelBase(IMessenger messenger)
    {
        Messenger = messenger;
        CancellationTokenSource = new CancellationTokenSource();
    }

    /// <summary>
    /// ViewModel이 활성화될 때 호출됩니다. (Caliburn.Micro의 OnActivateAsync와 유사)
    /// 기본 구현은 비어 있으며, 하위 클래스에서 오버라이드하여 사용합니다.
    /// </summary>
    public virtual async Task Activate()
    {
        await Task.CompletedTask;
    }

    /// <summary>
    /// ViewModel이 비활성화될 때 호출됩니다. (Caliburn.Micro의 OnDeactivateAsync와 유사)
    /// close가 true인 경우 CancellationTokenSource를 초기화합니다.
    /// </summary>
    public virtual async Task DeactivateAsync(bool close)
    {
        if (close)
        {
            CancellationTokenSource.Cancel();
            CancellationTokenSource.Dispose();
            CancellationTokenSource = new CancellationTokenSource();
        }

        await Task.CompletedTask;
    }

    /// <summary>
    /// ViewModel에서 사용한 리소스를 정리합니다.
    /// </summary>
    public virtual void Dispose()
    {
        CancellationTokenSource?.Cancel();
        CancellationTokenSource?.Dispose();
    }
}
