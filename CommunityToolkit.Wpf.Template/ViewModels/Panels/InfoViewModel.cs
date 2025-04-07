using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Wpf.Template.Models;
using CommunityToolkit.Wpf.Template.ViewModels.Base;
using System;

namespace CommunityToolkit.Wpf.Template.ViewModels.Panels;
/****************************************************************************
   Purpose      : 
       제품 정보 혹은 기타 설명용 정보를 화면에 표시하기 위한 ViewModel입니다.
       InfoModel을 속성으로 포함하고 있으며, 초기화 시 테스트용 데이터가 설정됩니다.
       ViewModelBase를 상속받아 메시징 및 취소 토큰 기능을 함께 제공합니다.

   Created By   : $username$                                                 
   Created On   : $time$                                                   
   Department   : $department$                                                   
   Company      : $company$                                       
   Email        : $email$                                            
****************************************************************************/
public partial class InfoViewModel : ViewModelBase
{
    #region - Ctors -
    /// <summary>
    /// Messenger 주입 및 Info 초기화
    /// </summary>
    public InfoViewModel(IMessenger messenger) : base(messenger)
    {
        Messenger = messenger;

        // 테스트용 정보 초기화
        Info = new InfoModel
        {
            Title = "제품 정보",
            Description = "이 제품은 최신형 고성능 장비입니다."
        };
    }
    #endregion

    #region - Implementation of Interface -
    #endregion

    #region - Overrides -
    #endregion

    #region - Binding Methods -
    #endregion

    #region - Processes -
    #endregion

    #region - IHandles -
    #endregion

    #region - Properties -
    #endregion

    #region - Attributes -
    /// <summary>
    /// 제품 또는 정보 패널에 표시할 정보 데이터
    /// </summary>
    [ObservableProperty]
    private InfoModel _info = new InfoModel();
    #endregion
}
