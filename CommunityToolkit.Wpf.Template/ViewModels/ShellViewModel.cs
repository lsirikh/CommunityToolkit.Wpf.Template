using Autofac;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Wpf.Template.Models;
using CommunityToolkit.Wpf.Template.ViewModels.Base;
using CommunityToolkit.Wpf.Template.Messages;
using CommunityToolkit.Wpf.Template.ViewModels.Navigations;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Wpf.Template.Views.Panels;
using CommunityToolkit.Wpf.Template.ViewModels.Panels;
using System.Windows.Input;
using System.Windows;

namespace CommunityToolkit.Wpf.Template.ViewModels;
/****************************************************************************
   Purpose      : 
       애플리케이션의 메인 Shell ViewModel로, 시작 시 최초로 로딩되는 뷰모델입니다.
       전체 패널 전환, 상태 메시지 처리, 비동기 로딩, 메시징 기반 뷰모델 전환 등을 담당합니다.
       CommunityToolkit.MVVM의 ObservableObject와 RelayCommand, Messenger 기능을 활용하며,
       ViewModel 간 전환은 Autofac DI 컨테이너를 통해 처리됩니다.
                                                                           
   Created By   : $username$                                                 
   Created On   : $time$                                                   
   Department   : $department$                                                   
   Company      : $company$                                       
   Email        : $email$                                         
****************************************************************************/
public partial class ShellViewModel : ObservableObject
{
    private readonly SetupModel _setupModel;

    // 메시지 송수신을 위한 Messenger 인스턴스 (DI 주입)
    [ObservableProperty]
    protected IMessenger? _messenger;

    // 현재 패널의 ViewModel을 전환하는 내비게이션 매니저
    [ObservableProperty]
    private PanelNavigationManager _panelNavigator;

    // 현재 화면에 표시되는 ViewModel
    [ObservableProperty]
    private ViewModelBase? _currentViewModel;

    // 상태 메시지 (예: 로딩 중, 완료 등)
    [ObservableProperty]
    private string? _statusMessage;

    // 로딩 또는 처리 중 여부 (IsBusy Spinner 등 표시용)
    [ObservableProperty]
    private bool _isBusy;

    // 취소 명령 저장용 (비동기 명령의 CancelCommand 바인딩)
    [ObservableProperty]
    ICommand? _cancleCommand;

    // 생성자 - Messenger, Navigator, SetupModel 주입
    public ShellViewModel(IMessenger messenger, PanelNavigationManager panelNavigator, SetupModel setupModel)
    {
        _messenger = messenger;
        _panelNavigator = panelNavigator;
        _setupModel = setupModel;

        // 메시지 구독 등록
        RegisterMessages();
    }

    // 소멸자 - 메시지 구독 해제
    ~ShellViewModel()
    {
        Messenger?.UnregisterAll(this);
    }

    /// <summary>
    /// Messenger를 통해 수신할 메시지 등록
    /// </summary>
    private void RegisterMessages()
    {
        // 네비게이션 메시지 수신 등록
        Messenger?.Register<NavigationMessage>(this, async (r, m) =>
        {
            try
            {
                // App의 DI 컨테이너를 통해 대상 ViewModel 인스턴스 생성
                var viewModel = App.Container?.Resolve(m.TargetViewModel) as ViewModelBase;
                if (viewModel != null)
                {
                    // 이전 ViewModel 비활성화 처리
                    if (CurrentViewModel != null)
                        await CurrentViewModel.DeactivateAsync(true);

                    // 현재 ViewModel 교체 및 활성화
                    CurrentViewModel = viewModel;
                    CurrentViewModel?.Activate();
                }
            }
            catch (Exception ex)
            {
                // 예외 시 상태 메시지에 오류 출력
                StatusMessage = $"네비게이션 오류: {ex.Message}";
            }
        });

        // 초기 상태 메시지 출력
        StatusMessage = "애플리케이션이 준비되었습니다.";

        // 상태 변경 메시지 수신 등록
        Messenger?.Register<StatusChangedMessage>(this, (r, m) =>
        {
            StatusMessage = m.Status;
        });
    }

    /// <summary>
    /// 종료 명령 (사용자 확인 후 애플리케이션 종료)
    /// </summary>
    [RelayCommand]
    private void Exit()
    {
        if (MessageBox.Show("애플리케이션을 종료하시겠습니까?", "종료 확인",
            MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
        {
            Application.Current.Shutdown();
        }
    }

    /// <summary>
    /// DataViewModel을 비동기로 로딩하고 화면 전환
    /// </summary>
    [RelayCommand(IncludeCancelCommand = true)]
    private async Task LoadDataViewModelAsync(CancellationToken token)
    {
        try
        {
            CancleCommand = LoadDataViewModelCancelCommand;
            IsBusy = true;
            StatusMessage = "DataViewModel 활성화 중...";

            // 로딩 시뮬레이션 (예: API 호출 대기)
            await Task.Delay(2_000, token);

            // NavigationMessage 전송 → ViewModel 교체 트리거
            Messenger?.Send(new NavigationMessage(typeof(DataView), typeof(DataViewModel)));

            StatusMessage = "DataViewModel 활성화 완료";
        }
        catch (OperationCanceledException)
        {
            StatusMessage = "DataViewModel 활성화 취소";
        }
        catch (Exception ex)
        {
            StatusMessage = $"오류 발생: {ex.Message}";
        }
        finally
        {
            IsBusy = false;
        }
    }

    /// <summary>
    /// InfoViewModel을 비동기로 로딩하고 화면 전환
    /// </summary>
    [RelayCommand(IncludeCancelCommand = true)]
    private async Task LoadInfoViewModelAsync(CancellationToken token)
    {
        try
        {
            CancleCommand = LoadInfoViewModelCancelCommand;
            IsBusy = true;
            StatusMessage = "InfoViewModel 활성화 중...";

            await Task.Delay(2_000, token);

            Messenger?.Send(new NavigationMessage(typeof(InfoView), typeof(InfoViewModel)));

            StatusMessage = "InfoViewModel 활성화 완료";
        }
        catch (OperationCanceledException)
        {
            StatusMessage = "InfoViewModel 활성화 취소";
        }
        catch (Exception ex)
        {
            StatusMessage = $"오류 발생: {ex.Message}";
        }
        finally
        {
            IsBusy = false;
        }
    }
}