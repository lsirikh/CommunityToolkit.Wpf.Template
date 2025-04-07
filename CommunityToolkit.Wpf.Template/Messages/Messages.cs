using System;

namespace CommunityToolkit.Wpf.Template.Messages;
/****************************************************************************
   Purpose      : 
       애플리케이션에서 사용하는 다양한 메시지 클래스를 정의합니다.
       이 클래스들은 CommunityToolkit.MVVM의 Messenger를 통해 ViewModel 간의 
       비동기/비침투적(Decoupled) 통신을 가능하게 합니다.

       - NavigationMessage: ViewModel 및 View 전환 요청
       - StatusChangedMessage: 상태 메시지 전달 (예: 상태바, 로깅)
       - PanelClosedMessage: 패널(ViewModel) 종료 알림
       - CancellationRequestedMessage: 작업 취소 요청 알림

   Created By   : $username$                                                 
   Created On   : $time$                                                   
   Department   : $department$                                                    
   Company      : $company$                                       
   Email        : $email$                                           
****************************************************************************/

/// <summary>
/// 다른 ViewModel 또는 ShellViewModel에 특정 View/ViewModel 전환을 요청하는 메시지
/// </summary>
public class NavigationMessage
{
    /// <summary>
    /// 이동할 대상 View 타입
    /// </summary>
    public Type TargetView { get; }

    /// <summary>
    /// 이동할 대상 ViewModel 타입
    /// </summary>
    public Type TargetViewModel { get; }

    public NavigationMessage(Type targetView, Type targetViewModel)
    {
        TargetView = targetView;
        TargetViewModel = targetViewModel;
    }
}

/// <summary>
/// 상태 메시지 변경 알림 (예: 상태 표시줄 메시지, 로깅 등)
/// </summary>
public class StatusChangedMessage
{
    /// <summary>
    /// 상태 텍스트 (예: "로딩 중...", "완료", "오류 발생" 등)
    /// </summary>
    public string Status { get; }

    /// <summary>
    /// 오류 여부 플래그 (기본값: false)
    /// </summary>
    public bool IsError { get; }

    public StatusChangedMessage(string status, bool isError = false)
    {
        Status = status;
        IsError = isError;
    }
}

/// <summary>
/// 특정 패널(ViewModel)이 닫혔음을 알리는 메시지
/// </summary>
public class PanelClosedMessage
{
    /// <summary>
    /// 닫힌 패널의 ViewModel 타입
    /// </summary>
    public Type ClosedPanel { get; }

    public PanelClosedMessage(Type closedPanel)
    {
        ClosedPanel = closedPanel;
    }
}

/// <summary>
/// 취소 명령이 요청되었음을 알리는 메시지 (예: 비동기 작업 중단)
/// </summary>
public class CancellationRequestedMessage { }
