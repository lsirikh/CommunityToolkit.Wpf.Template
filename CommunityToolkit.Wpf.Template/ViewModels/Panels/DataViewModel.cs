using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Wpf.Template.Messages;
using CommunityToolkit.Wpf.Template.ViewModels.Base;
using System;
using System.Reflection.Metadata;

namespace CommunityToolkit.Wpf.Template.ViewModels.Panels;
/****************************************************************************
   Purpose      : 
       데이터 관련 처리와 사용자 명령을 제어하는 ViewModel입니다.
       명령 실행 조건(CanExecute), 상태 메시지 전송, IsBusy 상태 관리,
       RelayCommand를 통한 취소 가능한 비동기 명령을 포함합니다.

   Created By   : $username$                                                 
   Created On   : $time$                                                   
   Department   : $department$                                                   
   Company      : $company$                                       
   Email        : $email$                                            
****************************************************************************/
public partial class DataViewModel : ViewModelBase
{
    #region - Ctors -
    /// <summary>
    /// Messenger 주입
    /// </summary>
    public DataViewModel(IMessenger messenger) : base(messenger)
    {
    }
    #endregion

    #region - Implementation of Interface -
    #endregion

    #region - Overrides -
    #endregion

    #region - Binding Methods -
    #endregion

    #region - Processes -

    /// <summary>
    /// 명령 실행 가능 조건: Content가 "애플리케이션"일 때만 실행 가능
    /// </summary>
    private bool CanClick() => Content == "애플리케이션";

    /// <summary>
    /// 비동기 명령 실행: 상태 메시지 전송 및 Content 갱신
    /// </summary>
    [RelayCommand(IncludeCancelCommand = true, CanExecute = nameof(CanClick))]
    private async Task OnClick(CancellationToken token)
    {
        try
        {
            Messenger?.Send(new StatusChangedMessage("데이터 로딩 중...", true));
            IsBusy = true;

            await Task.Delay(3_000, token); // 작업 시뮬레이션

            Content = "테스트 데이터 업데이트";
            Messenger?.Send(new StatusChangedMessage("데이터 로딩 완료", true));
        }
        catch (OperationCanceledException)
        {
            Messenger?.Send(new StatusChangedMessage("데이터 로딩 취소", true));
        }
        catch (Exception)
        {
            throw;
        }
        finally
        {
            IsBusy = false;
        }
    }
    #endregion

    #region - IHandles -
    #endregion

    #region - Properties -
    #endregion

    #region - Attributes -

    /// <summary>
    /// 제목 텍스트
    /// </summary>
    [ObservableProperty]
    private string? _title = "애플리케이션";

    /// <summary>
    /// 본문 콘텐츠 텍스트. 명령 조건에 영향을 미칩니다.
    /// </summary>
    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(ClickCommand))]
    private string? _content = "애플리케이션";

    /// <summary>
    /// 로딩 또는 작업 중 상태
    /// </summary>
    [ObservableProperty]
    private bool _isBusy;

    #endregion
}
