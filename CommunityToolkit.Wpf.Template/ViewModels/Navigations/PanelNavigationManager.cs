using Autofac;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Wpf.Template.Messages;
using CommunityToolkit.Wpf.Template.ViewModels.Base;
using System;

namespace CommunityToolkit.Wpf.Template.ViewModels.Navigations;
/****************************************************************************
   Purpose      : 
       현재 활성화된 ViewModel 패널을 관리하고, 메시지를 통해 다른 ViewModel로 전환하거나
       패널을 닫는 작업을 처리하는 내비게이션 매니저입니다.

       CommunityToolkit.MVVM의 Messenger와 ObservableRecipient를 활용하여,
       NavigationMessage 및 PanelClosedMessage를 수신하고 ViewModel을 동적으로 전환합니다.
       
       ShellViewModel이나 기타 ViewModel에서 메시지를 보냄으로써 느슨하게 패널 전환이 가능하며,
       MVVM 구조에서 ViewModel 간의 강한 결합 없이 UI 전환을 관리할 수 있습니다.

   Created By   : $username$                                                 
   Created On   : $time$                                                   
   Department   : $department$                                                   
   Company      : $company$                                       
   Email        : $email$                                            
****************************************************************************/

public partial class PanelNavigationManager : ObservableRecipient,
                                              IRecipient<NavigationMessage>,
                                              IRecipient<PanelClosedMessage>
{
    #region - Ctors -
    /// <summary>
    /// 생성자 - Messenger에 자동 등록되도록 IsActive 설정
    /// </summary>
    public PanelNavigationManager()
    {
        IsActive = true; // Messenger 자동 등록
    }
    #endregion

    #region - Implementation of Interface -

    /// <summary>
    /// NavigationMessage 수신 시 ViewModel을 전환합니다.
    /// 기존 ViewModel은 비활성화하고, 새 ViewModel을 활성화합니다.
    /// </summary>
    public async void Receive(NavigationMessage message)
    {
        try
        {
            if (App.Container == null)
                throw new NullReferenceException("DI Container가 초기화되지 않았습니다.");

            // DI를 통해 대상 ViewModel 생성
            var viewModel = App.Container.Resolve(message.TargetViewModel) as ViewModelBase;
            if (viewModel != null)
            {
                // 기존 ViewModel 비활성화
                if (CurrentViewModel != null)
                {
                    await CurrentViewModel.DeactivateAsync(true);
                }

                // 새 ViewModel 활성화 및 설정
                CurrentViewModel = viewModel;
                await CurrentViewModel.Activate();
            }
        }
        catch (Exception)
        {
            // 예외 발생 시 상위에서 처리 가능하도록 throw
            throw;
        }
    }

    /// <summary>
    /// PanelClosedMessage 수신 시 현재 ViewModel을 비활성화하고 제거합니다.
    /// </summary>
    public async void Receive(PanelClosedMessage message)
    {
        if (CurrentViewModel != null)
        {
            await CurrentViewModel.DeactivateAsync(true);
            CurrentViewModel = null;
        }
    }

    #endregion

    #region - Overrides -
    // ObservableRecipient의 override가 필요한 경우 정의
    #endregion

    #region - Binding Methods -
    // 뷰에서 바인딩할 수 있는 메서드 정의 (없음)
    #endregion

    #region - Processes -
    // 프로세스 관련 내부 처리 로직 정의 (없음)
    #endregion

    #region - IHandles -
    // 인터페이스 메시지 처리 외 추가 핸들러 정의 필요 시
    #endregion

    #region - Properties -
    // 복합 또는 계산 속성 정의 시 이 영역 사용
    #endregion

    #region - Attributes -

    /// <summary>
    /// 현재 활성화된 ViewModel 인스턴스
    /// </summary>
    [ObservableProperty]
    private ViewModelBase? _currentViewModel;

    #endregion
}
