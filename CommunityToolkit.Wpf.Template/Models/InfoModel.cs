using System;

namespace CommunityToolkit.Wpf.Template.Models;
/****************************************************************************
   Purpose      : 
       애플리케이션 내에서 사용되는 정보 패널 또는 정보 표시 UI를 위한
       데이터 모델 클래스입니다. 기본 제목과 설명 정보를 포함하며,
       ViewModel과의 바인딩을 통해 화면에 표시됩니다.

       예: InfoViewModel → InfoView 바인딩 시 사용
       
   Created By   : $username$                                                 
   Created On   : $time$                                                   
   Department   : $department$                                                   
   Company      : $company$                                       
   Email        : $email$                                            
****************************************************************************/
public class InfoModel
{
    /// <summary>
    /// 정보 제목 (예: 섹션 이름, 카드 제목 등)
    /// </summary>
    public string Title { get; set; } = "기본 정보 제목";

    /// <summary>
    /// 정보에 대한 설명 텍스트 (예: 소개 문구, 요약 등)
    /// </summary>
    public string Description { get; set; } = "기본 설명입니다.";
}
